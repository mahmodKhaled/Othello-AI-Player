class Heuristics{
    public static int CoinParity(Board board){
        int score = 0;
        int player = board.player;
        int opponent = board.opponent;
        int playerCoins = board.countCoins(player);
        int opponentCoins = board.countCoins(opponent);
        score = (100 * (playerCoins - opponentCoins)) / (playerCoins + opponentCoins);
        return score;
    }
    public static int Mobility(Board board){
        int score = 0;
        int player = board.player;
        int opponent = board.opponent;
        int playerMoves = board.GetValidMoves(player).Count;
        int opponentMoves = board.GetValidMoves(opponent).Count;
        score = (100 * (playerMoves - opponentMoves)) / (playerMoves + opponentMoves);
        return score;
    }
    public static int Corner(Board board){
        int score = 0;
        int player = board.player;
        int opponent = board.opponent;
        int playerCorners = board.countCorners(player);
        int opponentCorners = board.countCorners(opponent);
        score = (100 * (playerCorners - opponentCorners)) / (playerCorners + opponentCorners);
        return score;
    }
    public static int Stability(Board board){
        int score = 0;
        int player = board.player;
        int opponent = board.opponent;
        int playerStability = board.countStability(player);
        int opponentStability = board.countStability(opponent);
        score = (100 * (playerStability - opponentStability)) / (playerStability + opponentStability);
        return score;
    }

    public static void Main(){
        // Coin Parity
        // Mobility
        // Corner
        // Stability

    }
}