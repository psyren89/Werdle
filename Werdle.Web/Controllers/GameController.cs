namespace Werdle.Web.Controllers;

using System.Net.Mime;
using Lib.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Responses;
using Responses.Enums;

[Route("Game")]
public class GameController : Controller
{
    private const int MaxGuesses = 6;

    private readonly MemoryCacheEntryOptions oneHourFixedExpiration =
        new() { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1) };

    private readonly IMemoryCache cache;
    private readonly IGuessChecker guessChecker;
    private readonly IWordFormatValidator wordFormatValidator;
    private readonly ICsvWordSourceFactory csvWordSourceFactory;

    public GameController(
        IMemoryCache cache,
        IWordFormatValidator wordFormatValidator,
        ICsvWordSourceFactory csvWordSourceFactory,
        IGuessChecker guessChecker)
    {
        this.cache = cache;
        this.wordFormatValidator = wordFormatValidator;
        this.csvWordSourceFactory = csvWordSourceFactory;
        this.guessChecker = guessChecker;
    }

    [HttpPost("{gameId:guid}")]
    public IActionResult Guess(Guid gameId, [FromForm] string guessWord)
    {
        if (gameId == default || gameId == Guid.Empty)
        {
            return BadRequest("Invalid game ID");
        }

        if (!cache.TryGetValue(gameId, out GameState gameState))
        {
            return NotFound("No game with that ID exists");
        }

        if (gameState.RemainingGuesses == 0)
        {
            return BadRequest("No remaining guesses");
        }

        if (!wordFormatValidator.IsValid(guessWord))
        {
            return BadRequest("Invalid guess");
        }

        var letterFeedback = guessChecker.Check(gameState.Word, guessWord);

        var allCorrect = letterFeedback.All(lf => lf.Validity == LetterValidity.Correct);
        gameState.UseGuess();
        var noMoreGuesses = gameState.RemainingGuesses == 0;

        if (allCorrect || noMoreGuesses)
        {
            var outcome = allCorrect ? GameOutcome.Win : GameOutcome.Loss;
            var gameOver = new GameOver(gameState.Id, gameState.Word, outcome, letterFeedback);

            gameState.EndGame(outcome);

            return Json(gameOver);
        }

        return Json(new GuessFeedback(gameState, letterFeedback));
    }

    [HttpPost("[action]")]
    [Produces(MediaTypeNames.Application.Json)]
    public IActionResult New([FromForm] string gameWord)
    {
        if (string.IsNullOrEmpty(gameWord))
        {
            return BadRequest("Must provide a word for the game");
        }

        var wordSource = csvWordSourceFactory.Create();
        if (!wordSource.IsValid(gameWord))
        {
            return BadRequest("Invalid word");
        }

        var gameId = Guid.NewGuid();
        var newGame = new GameState(gameId, MaxGuesses, gameWord);

        // TODO: replace with distributed cache, DB, etc.
        cache.Set(gameId, newGame, oneHourFixedExpiration);

        return Json(newGame);
    }

    [HttpGet("{gameId:guid}")]
    [Produces(MediaTypeNames.Application.Json)]
    public IActionResult Status(Guid gameId)
    {
        if (gameId == default || gameId == Guid.Empty)
        {
            return BadRequest("Invalid game ID");
        }

        if (!cache.TryGetValue(gameId, out GameState gameState))
        {
            return NotFound("No game with that ID exists");
        }

        return Json(gameState);
    }

    [HttpGet("[action]/{gameWord:alpha}")]
    [Produces(MediaTypeNames.Application.Json)]
    public IActionResult Validate(string gameWord)
    {
        if (string.IsNullOrEmpty(gameWord))
        {
            return BadRequest("Must provide a word to validate");
        }

        var wordSource = csvWordSourceFactory.Create();
        return Json(wordSource.IsValid(gameWord));
    }
}
