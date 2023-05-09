class GamePlayingAlgorithms
{
    public int Minimax(int[][] board, int depth, bool maxmizingPlayer){
        if(depth == 0 || IsTerminalNode(board)){
            return Evaluate(board);
        }
        if(maxmizingPlayer){
            int bestValue = int.MinValue;
            foreach(int[][] child in GetChildren(board)){
                int value = Minimax(child, depth - 1, false);
                bestValue = Math.Max(bestValue, value);
            }
            return bestValue;
        }
        else{
            int bestValue = int.MaxValue;
            foreach(int[][] child in GetChildren(board)){
                int value = Minimax(child, depth - 1, true);
                bestValue = Math.Min(bestValue, value);
            }
            return bestValue;
        }

    }

    public int AlphaBetaPruning(int[][] board, int depth, int alpha, int beta, bool maxmizingPlayer){
        if(depth == 0 || IsTerminalNode(board)){
            return Evaluate(board);
        }
        if(maxmizingPlayer){
            int bestValue = int.MinValue;
            foreach(int[][] child in GetChildren(board)){
                int value = AlphaBetaPruning(child, depth - 1, alpha, beta, false);
                bestValue = Math.Max(bestValue, value);
                alpha = Math.Max(alpha, bestValue);
                if(beta <= alpha){
                    break;
                }
            }
            return bestValue;
        }
        else{
            int bestValue = int.MaxValue;
            foreach(int[][] child in GetChildren(board)){
                int value = AlphaBetaPruning(child, depth - 1, alpha, beta, true);
                bestValue = Math.Min(bestValue, value);
                beta = Math.Min(beta, bestValue);
                if(beta <= alpha){
                    break;
                }
            }
            return bestValue;
        }
    }

    public int AlphaBetaPruningWithIt(int[][] board, int depth, int alpha, int beta, bool maxmizingPlayer){
        if(depth == 0 || IsTerminalNode(board)){
            return Evaluate(board);
        }
        if(maxmizingPlayer){
            int bestValue = int.MinValue;
            foreach(int[][] child in GetChildren(board)){
                int value = AlphaBetaPruningWithIt(child, depth - 1, alpha, beta, false);
                bestValue = Math.Max(bestValue, value);
                alpha = Math.Max(alpha, bestValue);
                if(beta <= alpha){
                    break;
                }
            }
            return bestValue;
        }
        else{
            int bestValue = int.MaxValue;
            foreach(int[][] child in GetChildren(board)){
                int value = AlphaBetaPruningWithIt(child, depth - 1, alpha, beta, true);
                bestValue = Math.Min(bestValue, value);
                beta = Math.Min(beta, bestValue);
                if(beta <= alpha){
                    break;
                }
            }
            return bestValue;
        }
    }
    public static void Main()
    {
        // Minimax Algorithm
        // Alpha-Beta Pruning
        // Alpha-Beta Pruning with iterative deepening
    }
}