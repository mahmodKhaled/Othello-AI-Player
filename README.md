# Othello-AI-Player

Welcome to the OthelloAI project! In this project, we aim to explore the fascinating world of game playing using search algorithms and heuristics specifically designed for the game of Othello. Othello, also known as Reversi, is a two-player strategy game played on an 8x8 board, where each player controls pieces that are either black or white. The objective of the game is to have the majority of your colored pieces on the board when the game ends.

## Project Overview

The main objective of this project is to develop an intelligent Othello AI player capable of making strategic decisions and playing at a high level. By utilizing search algorithms and heuristics, our AI will analyze the game state, search through possible moves, and determine the best move to make at any given moment. This will allow the AI to exhibit strong gameplay and compete against human players or other AI opponents.

## Main Features

### Seach Algorithms

We will employ several search algorithms to explore and evaluate the game tree effectively:

- **Minimax Algorithm**: A fundamental search algorithm that examines all possible moves from a given position and selects the move that leads to the best outcome for the current player.

- **Alpha-Beta Pruning**: An enhancement to the minimax algorithm that prunes branches of the game tree that are guaranteed to be suboptimal. This reduces the number of nodes that need to be searched, resulting in improved performance.

- **Alpha-Beta Pruning with Iterative Deepening**: This algorithm combines the benefits of alpha-beta pruning with iterative deepening. It gradually increases the depth of the search tree until timing constraints are violated, allowing for a more precise evaluation of different move sequences and optimizing the AI's decision-making process.

### Heuristics

Heuristics play a crucial role in evaluating the game state and estimating the desirability of different moves. We will employ a combination of heuristics to capture important aspects of the game and guide the AI's decision-making process. Some of the heuristics we will utilize include:

- **Mobility**: This heuristic evaluates the number of legal moves available to each player. A player with more mobility typically has a higher advantage as they have more options to choose from.

- **Coin Parity**: The coin parity heuristic calculates the difference between the number of pieces of the AI's color and the opponent's color. Having more pieces on the board usually indicates a favorable position.

- **Corners Captured**: Corners are strategically valuable positions in Othello. The heuristic rewards the AI for occupying corners since they are difficult to capture and provide stability throughout the game.

- **Stability**: Stability refers to pieces that cannot be flipped by the opponent. The AI will prioritize maintaining stable positions on the board as they are less vulnerable to capture and offer long-term advantages.

These heuristics will be combined and weighted to create an overall evaluation function that quantifies the desirability of different game states and moves. We will continuously refine and optimize these heuristics throughout the development process to improve the AI's gameplay and decision-making capabilities.

### Gameplay Modes

Our AI player will accommodate different gameplay modes, including:

- **Human vs. AI**: Engage in a thrilling battle against our AI opponent. Test your skills and strategic thinking in challenging gameplay.

- **AI vs. AI**: Sit back and watch as our intelligent AI players go head-to-head in a battle of wits. Observe their decision-making processes and witness exciting gameplay unfold.

- **Human vs. Human**: Challenge your friends or family members to a competitive Othello match. Take turns making strategic moves and outsmart your opponent on the virtual game board.

For AI gameplay, we offer three difficulty levels:

- **Easy**: Suitable for beginners or players seeking a more relaxed experience. The AI will make simpler and more forgiving moves, providing an opportunity to learn and improve your skills.

- **Medium**: Ideal for players looking for a balanced challenge. The AI will employ strategic thinking and make competitive moves to test your abilities.

- **Hard**: Geared towards experienced players seeking a formidable opponent. The AI will utilize advanced strategies and make optimal moves, providing a tough and engaging gameplay experience.

With these varied gameplay modes and difficulty levels, the OthelloAI project offers a customizable and enjoyable experience for players of all skill levels.

### Graphical User Interface(GUI)

- We will create a user-friendly GUI that enables human players to interact with the Othello AI. The GUI will feature a game board display, move input functionality for human players, game status information (score, current turn, game over), and game options to select different modes and difficulty levels.

## Getting Started

To begin your journey with the OthelloAI project, you have two options to get started:

### Option 1: Play the Game Exe

1. Clone the repository to your local machine.
2. Navigate to the "Game Exe" folder within the repository.
3. Run the provided executable file to launch the Othello game.
4. Enjoy playing Othello against the AI directly without any additional setup or configuration.

### Option 2: Integrate the Project with Unity

1. Clone the repository to your local machine.
2. Locate the C# project files in the repository.
3. Import these C# classes into your Unity project.
4. Continue building the missing components related to object design and visual elements in Unity, leveraging the provided project structure and logic.
5. Run the Unity project to start playing Othello against the AI within the Unity environment.
6. Customize and enhance the game as per your preferences, adding visual effects, animations, and additional features using Unity's powerful tools and capabilities.

Choose the option that best suits your needs and preferences. Whether you prefer to play the game as an executable or integrate the project into Unity for further customization, we hope you have an enjoyable experience playing Othello against our AI player.

## License

This project is licensed under the <ins>**MIT License**</ins>. Feel free to use, modify, and distribute the code according to the terms of the license.
