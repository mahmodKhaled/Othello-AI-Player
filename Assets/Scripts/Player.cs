public enum Player
{
    // Define three values for the Player enum: None, Black, and White
    None,
    Black,
    White
}

public static class PlayerExtensions
{
    // Define an extension method for the Player enum to get the opponent of a given player
    public static Player Opponent(this Player player)
    {
        // If the given player is black, return white
        if (player == Player.Black)
        {
            return Player.White;
        }
        // If the given player is white, return black
        else if (player == Player.White)
        {
            return Player.Black;
        }

        // If the given player is none, return none
        return Player.None;
    }
}