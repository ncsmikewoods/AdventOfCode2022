namespace Day09;

public class Move
{
    public Move(string Direction, int Magnitude)
    {
        this.Direction = Direction;
        this.Magnitude = Magnitude;
    }

    public string Direction { get; set; }
    public int Magnitude { get; set; }
}