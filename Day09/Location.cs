namespace Day09;

public class Location
{
    public Location(int X, int Y)
    {
        this.X = X;
        this.Y = Y;
    }

    public int X { get; set; }
    public int Y { get; set; }

    public bool Overlaps(Location other)
    {
        return X == other.X && Y == other.Y;
    }
}