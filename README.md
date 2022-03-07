# Werdle

tl;dr: [here](#getting-started).

Totally not Wordle. Please don't sue me, New York Times.

An API for managing and playing word decoding games.

Four endpoints are available:

- `/Game/New` (begins a new game)
- `/Game/<game id>` (view the current status of a game)
- `/Game/<game id>/Guess` (submit a guess for a game)
- `/Game/Validate` (validates a word for use in a game)

A typical game cycle is as follows:

- Create a new game with a valid word, taking note of the returned game ID
- Make guesses against the game by providing the game ID and a word
- At any time, users can see the status of a game (and remaining guesses)
- Once the word has been correctly deduced or no guesses remain, the game is over

Attempting to submit guesses against a completed game will return `BadRequest`.

#### Games expire an hour from the time they are created, regardless of whether they are complete.

## Client Information

Nicholas Little (nlittle@fmgl.com.au)

## Getting Started

1. Open `Werdle.sln` in your preferred IDE and `Rebuild the Solution`
1. Run the application

Alternatively, via command line:

1. `cd Werdle.Web`
1. `dotnet build`
1. `dotnet run`

Open the Swagger API browser in a web browser at `http<s>://localhost:<port>/swagger`

## Running tests

Run as usual through your IDE of choice.

Alternatively, via command line:

1. `cd Werdle.Web.Tests`
1. `dotnet build`
1. `dotnet test`
