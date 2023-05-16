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
    public static int CoinParity(GameState state)
    {
        this.currentPlayerCoins = state.DiscCount[state.CurrentPlayer];
        this.opponentCoins = state.DiscCount[state.CurrentPlayer.Opponent()];

        this.coin_parity_score = (100 * (this.currentPlayerCoins - this.opponentCoins)) / (this.currentPlayerCoins + this.opponentCoins);

        return this.coin_parity_score;
    }

    public static int Mobility(GameState state)
    {
        this.currentPlayerMoves = state.LegalMoves.Count;

        Player opponent = state.CurrentPlayer.Opponent();
        if (state.LegalMoves.ContainsKey(opponent))
            this.opponentMoves = state.LegalMoves[opponent].Count;

        this.mobility_score = (100 * (this.currentPlayerMoves - this.opponentMoves)) / (this.currentPlayerMoves + this.opponentMoves);

        return this.mobility_score;
    }

    public static int Corner(GameState state)
    {
        // Define the corner positions
        Position[] cornerPositions = { new Position(0, 0), new Position(0, 7), new Position(7, 0), new Position(7, 7) };

        foreach (var position in cornerPositions)
        {
            if (state.Board[position.Row, position.Col] == state.CurrentPlayer)
                this.currentPlayerCorners++;
            else if (state.Board[position.Row, position.Col] == state.CurrentPlayer.Opponent())
                this.opponentCorners++;
        }

        this.corner_score = (100 * (this.currentPlayerCorners - this.opponentCorners)) / (this.currentPlayerCorners + this.opponentCorners);

        return this.corner_score;
    }

    public static int Stability(GameState state)
    {

        // Define the stable positions
        Position[] stablePositions = {
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

        foreach (var position in stablePositions)
        {
            if (state.Board[position.Row, position.Col] == state.CurrentPlayer)
                this.currentPlayerStability++;
            else if (state.Board[position.Row, position.Col] == state.CurrentPlayer.Opponent())
                this.opponentStability++;
        }

        this.stability_score = (100 * (this.currentPlayerStability - this.opponentStability)) / (this.currentPlayerStability + this.opponentStability);

        return this.stability_score;
    }

    public static void Main(){
        // Coin Parity
        // Mobility
        // Corner
        // Stability

    }
}