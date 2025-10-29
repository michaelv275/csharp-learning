# Trivia Bot Console App

## Overview

This will be a configurable trivia bot game where users can play solo or with their friends.

## Game Rules

Single player or mutliplayer option.

The game will consist of three rounds with 10 questions per round.

Rounds can be configured based on number of questions, difficulty of questions, as well as the maximum number of hard questions.
The first round will be x category, second round will be x category, and final round will be general knowledge.

Mercy rule: get more than 10 incorrect, you lose.
Single player passing score: 70% correct (roughly 23/30 questions)

## Game expectations

On startup of game, the user will be presented with configuration options for the gameplay.
These configurations will include:

- Number of rounds (limit of 5)
- Number of questions per round (limit of 10)
- Difficulty of questions (easy, medium, hard)
- Categories: choose on a per round basis, can be fully random.
- Players/teams

Configure player/team identity

Retrieve questions list from API

Start game.

## Data Structures

Structure of question data retrieved from Open Trivia Database API

Multiple choice:

```
{
    "type": "multiple",
    "difficulty": "medium",
    "category": "General Knowledge",
    "question": "Sample question",
    "correct_answer": "Correct answer",
    "incorrect_answers": [
        "Answer 1",
        "Answer 2",
        "Answer 3"
    ]
}
```

True/false:

```
{
    "type": "boolean",
    "difficulty": "medium",
    "category": "General Knowledge",
    "question": "Sample question",
    "correct_answer": "False",
    "incorrect_answers": [
        "True"
    ]
}
```

Player/Team:

```
{
    "name": string,
    "score": int,
    "numIncorrectAnswers": int,
}
```

## Data Retrieval

Using an HTTP client, we will hit the [OpenTrivia Database API]("https://opentdb.com/api_config.php") to retrieve our trivia questions.

Sample request will look like:
`https://opentdb.com/api.php?amount=10&category=28&difficulty=hard`
