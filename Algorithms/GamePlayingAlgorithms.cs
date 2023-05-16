using System;

public class GamePlayingAlgorithms
{
    private GameState state;
    private Heuristics heuristics;
    private int coinParityWeight;
    private int mobilityWeight;
    private int cornerWeight;
    private int stabilityWeight;

    public GamePlayingAlgorithms()
    {
        state = new GameState();
        heuristics = new Heuristics();
        coinParityWeight = 1;
        mobilityWeight = 2;
        cornerWeight = 3;
        stabilityWeight = 4;
    }
    public static MoveInfo Minimax(GameState state, int depth)
    {
        if (depth <= 0 || state.GameOver)
        {
            return Evaluate(state);
        }

        MoveInfo bestMove = null;
        int bestScore = int.MinValue;

        foreach (var move in state.LegalMoves.Keys)
        {
            GameState nextState = CloneState(state);
            if (nextState.MakeMove(move, out MoveInfo moveInfo))
            {
                MoveInfo opponentMove = Minimax(nextState, depth - 1);
                int score = -opponentMove.Score;

                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = moveInfo;
                    bestMove.Score = score;
                }
            }
        }

        return bestMove;
    }

    public static MoveInfo AlphaBetaPruning(GameState state, int depth)
    {
        return AlphaBetaPruning(state, depth, int.MinValue, int.MaxValue, true);
    }

    private static MoveInfo AlphaBetaPruning(GameState state, int depth, int alpha, int beta, bool maximizingPlayer)
    {
        if (depth <= 0 || state.GameOver)
        {
            return Evaluate(state);
        }

        MoveInfo bestMove = null;

        if (maximizingPlayer)
        {
            int bestScore = int.MinValue;

            foreach (var move in state.LegalMoves.Keys)
            {
                GameState nextState = CloneState(state);
                if (nextState.MakeMove(move, out MoveInfo moveInfo))
                {
                    MoveInfo opponentMove = AlphaBetaPruning(nextState, depth - 1, alpha, beta, false);
                    int score = -opponentMove.Score;

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = moveInfo;
                        bestMove.Score = score;
                    }

                    alpha = Math.Max(alpha, bestScore);
                    if (alpha >= beta)
                    {
                        break;
                    }
                }
            }
        }
        else
        {
            int bestScore = int.MaxValue;

            foreach (var move in state.LegalMoves.Keys)
            {
                GameState nextState = CloneState(state);
                if (nextState.MakeMove(move, out MoveInfo moveInfo))
                {
                    MoveInfo opponentMove = AlphaBetaPruning(nextState, depth - 1, alpha, beta, true);
                    int score = -opponentMove.Score;

                    if (score < bestScore)
                    {
                        bestScore = score;
                        bestMove = moveInfo;
                        bestMove.Score = score;
                    }

                    beta = Math.Min(beta, bestScore);
                    if (beta <= alpha)
                    {
                        break;
                    }
                }
            }
        }

        return bestMove;
    }

    public static MoveInfo AlphaBetaPruningWithIterativeDeepening(GameState state, int maxDepth)
    {
        MoveInfo bestMove = null;

        for (int depth = 1; depth <= maxDepth; depth++)
        {
            MoveInfo currentMove = AlphaBetaPruning(state, depth);
            if (currentMove == null)
                break;

            bestMove = currentMove;
        }

        return bestMove;
    }

    private static MoveInfo Evaluate(GameState state)
    {

        int coinParityScore = heuristics.CoinParity(state);
        int mobilityScore = heuristics.Mobility(state);
        int cornerScore = heuristics.Corner(state);
        int stabilityScore = heuristics.Stability(state);

        int weightedScore = this.coinParityWeight * coinParityScore +
                            this.mobilityWeight * mobilityScore +
                            this.cornerWeight * cornerScore +
                            this.stabilityWeight * stabilityScore;

        return new MoveInfo { Score = weightedScore };
    }


    private static GameState CloneState(GameState state)
    {
        // Create a deep copy of the GameState object to avoid modifying the original state.
        GameState clone = new GameState();

        clone.Board = (Player[,])state.Board.Clone();
        clone.DiscCount = new Dictionary<Player, int>(state.DiscCount);
        clone.CurrentPlayer = state.CurrentPlayer;
        clone.GameOver = state.GameOver;
        clone.Winner = state.Winner;
        clone.LegalMoves = new Dictionary<Position, List<Position>>(state.LegalMoves);
        return clone;
    }

    public static void Main(){
        // MiniMax
        // AlphaBetaPruning
        // AlphaBetaPruningWithIterativeDeepening

    }
}
        