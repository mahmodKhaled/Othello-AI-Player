public class Heuristics
{
    private GameState state;
    private int currentPlayerCoins;
    private int opponentCoins;
    private int coin_parity_score;
    private int currentPlayerMoves;
    private int opponentMoves;
    private int mobility_score;
    private int currentPlayerCorners;
    private int opponentCorners;
    private int corner_score;
    private int currentPlayerStability;
    private int opponentStability;
    private int stability_score;

    public Heuristics()
    {
        // Initialize the Heuristics object with default values
        this.state = new GameState();
        this.currentPlayerCoins = 0;
        this.opponentCoins = 0;
        this.coin_parity_score = 0;
        this.currentPlayerMoves = 0;
        this.opponentMoves = 0;
        this.mobility_score = 0;
        this.currentPlayerCorners = 0;
        this.opponentCorners = 0;
        this.corner_score = 0;
        this.currentPlayerStability = 0;
        this.opponentStability = 0;
        this.stability_score = 0;
    }
    // Calculate the coin parity score for the given game state
    public int CoinParity(GameState state)
    {
        // Calculate the number of coins for the current player and the opponent
        this.currentPlayerCoins = state.DiscCount[state.CurrentPlayer];
        this.opponentCoins = state.DiscCount[state.CurrentPlayer.Opponent()];

        // Calculate the coin parity score using the formula
        this.coin_parity_score = (100 * (this.currentPlayerCoins - this.opponentCoins)) / (this.currentPlayerCoins + this.opponentCoins);

        return this.coin_parity_score;
    }

    // Calculate the mobility score for the given game state
    public int Mobility(GameState state)
    {
        // Calculate the number of legal moves for the current player
        this.currentPlayerMoves = state.LegalMoves.Count;

        // Calculate the number of legal moves for the opponent
        Player opponent = state.CurrentPlayer.Opponent();
        if (state.LegalMoves.ContainsKey(opponent))
            this.opponentMoves = state.LegalMoves[opponent].Count;

        this.mobility_score = (100 * (this.currentPlayerMoves - this.opponentMoves)) / (this.currentPlayerMoves + this.opponentMoves);

        return this.mobility_score;
    }

    // Calculate the corner score for the given game state
    public int Corner(GameState state)
    {
        // Define the corner positions
        Position[] cornerPositions = { new Position(0, 0), new Position(0, 7), new Position(7, 0), new Position(7, 7) };

        // Calculate the number of corners for the current player and the opponent
        foreach (var position in cornerPositions)
        {
            if (state.Board[position.Row, position.Col] == state.CurrentPlayer)
                this.currentPlayerCorners++;
            else if (state.Board[position.Row, position.Col] == state.CurrentPlayer.Opponent())
                this.opponentCorners++;
        }

        // Calculate the corner score using the formula
        this.corner_score = (100 * (this.currentPlayerCorners - this.opponentCorners)) / (this.currentPlayerCorners + this.opponentCorners);

        return this.corner_score;
    }

    // Calculate the stability score for the given game state
    public int Stability(GameState state)
    {

        // Define the stable positions
        Position[] stablePositions = {
            // List of stable positions
            new Position(1, 1), new Position(1, 6), new Position(6, 1), new Position(6, 6),
            new Position(1, 0), new Position(0, 1), new Position(1, 7), new Position(0, 6),
            new Position(7, 1), new Position(6, 0), new Position(6, 7), new Position(7, 6),
            new Position(0, 0), new Position(0, 7), new Position(7, 0), new Position(7, 7),
            new Position(2, 2), new Position(2, 5), new Position(5, 2), new Position(5, 5),
            new Position(2, 0), new Position(0, 2), new Position(2, 7), new Position(0, 5),
            new Position(7, 2), new Position(5, 0), new Position(5, 7), new Position(7, 5),
            new Position(1, 2), new Position(2, 1), new Position(1, 5), new Position(2, 6),
            new Position(6, 1), new Position(5, 2), new Position(6, 6), new Position(5, 5),
            new Position(1, 3), new Position(3, 1), new Position(1, 4), new Position(3, 6),
            new Position(6, 3), new Position(4, 2), new Position(6, 4), new Position(4, 5),
            new Position(3, 0), new Position(0, 3), new Position(3, 7), new Position(0, 4),
            new Position(7, 3), new Position(4, 0), new Position(7, 4), new Position(4, 7),
            new Position(3, 2), new Position(2, 3), new Position(3, 5), new Position(5, 3),
            new Position(6, 2), new Position(2, 4), new Position(3, 3), new Position(4, 3)
        };

        // Calculate the number of stable positions for the current player and the opponent
        foreach (var position in stablePositions)
        {
            if (state.Board[position.Row, position.Col] == state.CurrentPlayer)
                this.currentPlayerStability++;
            else if (state.Board[position.Row, position.Col] == state.CurrentPlayer.Opponent())
                this.opponentStability++;
        }

        // Calculate the stability score using the formula
        this.stability_score = (100 * (this.currentPlayerStability - this.opponentStability)) / (this.currentPlayerStability + this.opponentStability);

        return this.stability_score;
    }
}