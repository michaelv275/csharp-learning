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

1. Moved logic to new CreateStories function to fill in the template with the blanks
2. Created new function VoteForBestStory to allow players to vote for their favorite stories and calculate score.
3. Created new function DetermineWinner to look at the scores and see who had the most points and who tied.

# To Do

1. Fix bug when selecting story template. Noticed when going fast. Maybe when entering a wrong possibilty first?
2. Set caveman responses automatically
3. TODO: Cavemanify the prompts from last session
4. Play rock paper scissors when tied for first
5. Reorganize logic clean methods. Maybe move methods from Program.cs to other locations
6. Clean up text output so it's all clear and no weird formatting
