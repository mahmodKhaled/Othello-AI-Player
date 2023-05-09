using System.Collections.Generic;

public class GameState
{
   public const int Rows=8;
   public const int Cols=8;
   public Player[,] Board {get;}
   public Dictionary<Player,int> DiscCount {get;}
   public Player CurrentPlayer {get;private set;}
   public bool GameOver {get;private set;}
   public Player Winner {get;private set;}
   public Dictionary<Position,List<Position>> LegalMoves {get;private set;}
   public GameState()
{
    // Initialize the game board as a 2D array of Player objects
    Board = new Player[Rows, Cols];

    // Set the initial state of the game board with two white and two black discs in the center
    Board[3, 3] = Player.White;
    Board[3, 4] = Player.Black;
    Board[4, 3] = Player.Black;
    Board[4, 4] = Player.White;

    // Initialize the disc count for each player
    DiscCount = new Dictionary<Player, int>()
    {
        {Player.Black, 2},
        {Player.White, 2}
    };

    // Set the current player to black
    CurrentPlayer = Player.Black;

    // Find and store the legal moves for the current player
    LegalMoves = FindLegalMoves(CurrentPlayer);
}

  public bool MakeMove(Position pos, out MoveInfo moveInfo)
{
    // Check if the move is legal
    if (!LegalMoves.ContainsKey(pos))
    {
        // If the move is not legal, set moveInfo to null and return false
        moveInfo = null;
        return false;
    }

    // Get the current player
    Player movePlayer = CurrentPlayer;

    // Get the list of outflanked positions for the given move
    List<Position> outflanked = LegalMoves[pos];

    // Place the disc on the board
    Board[pos.Row, pos.Col] = movePlayer;

    // Flip the outflanked discs
    FlipDiscs(outflanked);

    // Update the disc counts for each player
    UpdateDiscCounts(movePlayer, outflanked.Count);

    // Pass the turn to the next player
    PassTurn();

    // Set the moveInfo with information about the move
    moveInfo = new MoveInfo { Player = movePlayer, Position = pos, Outflanked = outflanked };

    // Return true to indicate that the move was successful
    return true;
}

  public IEnumerable<Position> OccupiedPositions()
{
    // Iterate over all rows and columns of the game board
    for (int r = 0; r < Rows; r++)
    {
        for (int c = 0; c < Cols; c++)
        {
            // Check if the current position is occupied by a player
            if (Board[r, c] != Player.None)
            {
                // If the position is occupied, return the position using the yield keyword
                yield return new Position(r, c);
            }
        }
    }
}

   private void FlipDiscs(List<Position> positions)
{
    // Iterate over all positions in the list
    foreach (Position pos in positions)
    {
        // Flip the disc at the current position by setting it to the opponent of the current player
        Board[pos.Row, pos.Col] = Board[pos.Row, pos.Col].Opponent();
    }
}

private void UpdateDiscCounts(Player movePlayer, int outflankedCount)
{
    // Increase the disc count for the current player by the number of outflanked discs plus one for the new disc
    DiscCount[movePlayer] += outflankedCount + 1;

    // Decrease the disc count for the opponent by the number of outflanked discs
    DiscCount[movePlayer.Opponent()] -= outflankedCount;
}

private void ChangePlayer()
{
    // Set the current player to the opponent of the current player
    CurrentPlayer = CurrentPlayer.Opponent();

    // Find and store the legal moves for the new current player
    LegalMoves = FindLegalMoves(CurrentPlayer);
}

private Player FindWinner()
{
    // Check if black has more discs than white
    if (DiscCount[Player.Black] > DiscCount[Player.White])
    {
        return Player.Black;
    }

    // Check if white has more discs than black
    if (DiscCount[Player.White] > DiscCount[Player.Black])
    {
        return Player.White;
    }

    // If neither player has more discs, return Player.None to indicate a tie
    return Player.None;
}

    private void PassTurn()
{
    // Change the current player to the opponent
    ChangePlayer();

    // Check if the new current player has any legal moves
    if (LegalMoves.Count > 0)
    {
        // If the new current player has legal moves, return without doing anything else
        return;
    }

    // If the new current player has no legal moves, change the current player back to the original player
    ChangePlayer();

    // Check if the original player also has no legal moves
    if (LegalMoves.Count == 0)
    {
        // If neither player has any legal moves, set the current player to Player.None and set the game over flag to true
        CurrentPlayer = Player.None;
        GameOver = true;

        // Find and store the winner of the game
        Winner = FindWinner();
    }
}

private bool IsInsideBoard(int r, int c)
{
    // Check if the given row and column are within the bounds of the game board
    return r >= 0 && r < Rows && c >= 0 && c < Cols;
}

private List<Position> OutflankedInDir(Position pos, Player player, int rDelta, int cDelta)
{
    // Create a new list to store the outflanked positions
    List<Position> outflanked = new List<Position>();

    // Set the initial row and column to check based on the given position and deltas
    int r = pos.Row + rDelta;
    int c = pos.Col + cDelta;

    // While the current position is inside the board and is not empty
    while (IsInsideBoard(r, c) && Board[r, c] != Player.None)
    {
        // Check if the disc at the current position belongs to the opponent of the given player
        if (Board[r, c] == player.Opponent())
        {
            // If it does, add the current position to the list of outflanked positions
            outflanked.Add(new Position(r, c));

            // Move to the next position in the given direction
            r += rDelta;
            c += cDelta;
        }
        else
        {
            // If we reach a disc that belongs to the given player or an empty position, return the list of outflanked positions
            return outflanked;
        }
    }

    // If we reach this point, it means that no discs were outflanked in this direction. Return an empty list.
    return new List<Position>();
}
   private List<Position> Outflanked(Position pos, Player player)
{
    // Create a new list to store the outflanked positions
    List<Position> outflanked = new List<Position>();

    // Iterate over all directions (8 directions in total)
    for (int rDelta = -1; rDelta <= 1; rDelta++)
    {
        for (int cDelta = -1; cDelta <= 1; cDelta++)
        {
            // Skip the case where both deltas are zero (no direction)
            if (rDelta == 0 && cDelta == 0)
            {
                continue;
            }

            // Find the outflanked positions in the current direction and add them to the list
            outflanked.AddRange(OutflankedInDir(pos, player, rDelta, cDelta));
        }
    }

    // Return the list of outflanked positions
    return outflanked;
}

private bool IsMoveLegal(Player player, Position pos, out List<Position> outflanked)
{
    // Check if the given position is already occupied
    if (Board[pos.Row, pos.Col] != Player.None)
    {
        // If it is, set outflanked to null and return false to indicate that the move is not legal
        outflanked = null;
        return false;
    }

    // Find the outflanked positions for the given move
    outflanked = Outflanked(pos, player);

    // Return true if there is at least one outflanked position, otherwise return false
    return outflanked.Count > 0;
}

private Dictionary<Position, List<Position>> FindLegalMoves(Player player)
{
    // Create a new dictionary to store the legal moves and their corresponding outflanked positions
    Dictionary<Position, List<Position>> legalMoves = new Dictionary<Position, List<Position>>();

    // Iterate over all positions on the game board
    for (int r = 0; r < Rows; r++)
    {
        for (int c = 0; c < Cols; c++)
        {
            // Create a new Position object for the current position
            Position pos = new Position(r, c);

            // Check if the move at the current position is legal for the given player
            if (IsMoveLegal(player, pos, out List<Position> outflanked))
            {
                // If it is, add it to the dictionary of legal moves along with its corresponding outflanked positions
                legalMoves[pos] = outflanked;
            }
        }
    }

    // Return the dictionary of legal moves
    return legalMoves;
}
}
