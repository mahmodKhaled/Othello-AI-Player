class Heuristics{
    public class Heuristics
{
    public static int CoinParity(GameState state)
    {
        int score = 0;
        int currentPlayerCoins = state.DiscCount[state.CurrentPlayer];
        int opponentCoins = state.DiscCount[state.CurrentPlayer.Opponent()];

        score = (100 * (currentPlayerCoins - opponentCoins)) / (currentPlayerCoins + opponentCoins);

        return score;
    }

    public static int Mobility(GameState state)
    {
        int score = 0;
        int currentPlayerMoves = state.LegalMoves.Count;
        int opponentMoves = 0;

        Player opponent = state.CurrentPlayer.Opponent();
        if (state.LegalMoves.ContainsKey(opponent))
            opponentMoves = state.LegalMoves[opponent].Count;

        score = (100 * (currentPlayerMoves - opponentMoves)) / (currentPlayerMoves + opponentMoves);

        return score;
    }

    public static int Corner(GameState state)
    {
        int score = 0;
        int currentPlayerCorners = 0;
        int opponentCorners = 0;

        // Define the corner positions
        Position[] cornerPositions = { new Position(0, 0), new Position(0, 7), new Position(7, 0), new Position(7, 7) };

        foreach (var position in cornerPositions)
        {
            if (state.Board[position.Row, position.Col] == state.CurrentPlayer)
                currentPlayerCorners++;
            else if (state.Board[position.Row, position.Col] == state.CurrentPlayer.Opponent())
                opponentCorners++;
        }

        score = (100 * (currentPlayerCorners - opponentCorners)) / (currentPlayerCorners + opponentCorners);

        return score;
    }

    public static int Stability(GameState state)
    {
        int score = 0;
        int currentPlayerStability = 0;
        int opponentStability = 0;

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
                currentPlayerStability++;
            else if (state.Board[position.Row, position.Col] == state.CurrentPlayer.Opponent())
                opponentStability++;
        }

        score = (100 * (currentPlayerStability - opponentStability)) / (currentPlayerStability + opponentStability);

        return score;
    }

    public static void Main(){
        // Coin Parity
        // Mobility
        // Corner
        // Stability

    }
}