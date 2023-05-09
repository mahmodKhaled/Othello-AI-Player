public class Position
{
    // Define two read-only properties for the row and column of the position
    public int Row { get; }
    public int Col { get; }

    // Define a constructor that takes a row and column as input and sets the corresponding properties
    public Position(int row, int col)
    {
        Row = row;
        Col = col;
    }

    // Override the Equals method to compare two Position objects based on their row and column values
    public override bool Equals(object obj)
    {
        // Check if the given object is a Position object
        if (obj is Position other)
        {
            // If it is, return true if the row and column values are equal, otherwise return false
            return Row == other.Row && Col == other.Col;
        }

        // If the given object is not a Position object, return false
        return false;
    }

    // Override the GetHashCode method to calculate a hash code for a Position object based on its row and column values
    public override int GetHashCode()
    {
        // Calculate and return a hash code using an arbitrary formula
        return 8 * Row + Col;
    }
}