// Hello world

# Summary

At least 1 human player, 1 npc.
Goal: All humans vote on which story is funnier/better, tie => Rock Paper Scissors game with npc
Need: Madlib formats (find API of madlibs stories?), lists of adjectives/nouns/etc (Dictionary API?),
Examples:
API-Ninjas (Random Word API): Offers a robust API where you can specify type=noun, type=adjective, type=verb, or type=adverb.
Random Word Form: A popular, free API (https://random-word-form.herokuapp.com/random/noun or /random/adjective) specifically designed to return single words by part of speech.
Random Word API (herokuapp): A simple API designed for fetching random words, often used to create combinations.
Faker (Word API): Part of the Faker library, this provides a method to generate a random adjective, noun, or verb. [1, 2, 3, 4, 5]

Create a method that that takes a MadlibBlank object and looks at its type property. Then grabs a .json file matching that type from the 'words' folder
and outputs "Enter a <TYPE> ex: (top 3 examples from file)"

# Example Game

1. Build human players. Caveman = bot
2. Best out of three rounds.
3. All players presented the same madlib/questions
4. All inputs are verified against dictionary (except Proper Nouns)
5. Caveman selects their words from dictionary API.
6. Madlibs are constructed and both are presented to users.
7. All human users blind vote. Results withheld until end of game.
8. At end of game, a winner is decided by who has the most votes.
   If there is a tie, they play rock paper scissors.
9. Caveman will play randomly and human gets to write in input.
   First one to win.

# Last time

1. Built stubs for methods getting user selsected templates
2. AddedMAdLibResponse List to Player object to contain answers
3. Loop through blanks and ask each user to fill in response

# To Do

1. Create a method that that takes a MadlibBlank object and looks at its type property. Then grabs a .json file matching that type from the 'words' folder and outputs "Enter a <TYPE> ex: (top 3 examples from file)"
2. Todo: GetMadLibGenre(), create enum models for different template types
3. Todo: GetExamplesForBlankType()
4. Brainrot boogabot's word list and add dumb slang to its options
5. Print all stories for each player, and loop through available players - booga bot and ask to vote. Each human player can vote. If they vote for themselves, shame them publicly.
