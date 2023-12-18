using System.Drawing;

namespace Utilitys;

public class Line
{
    public Point Start { get;}
    public Point End { get; }

    public Line(Point start, Point end)
    {
        Start = start;
        End = end;
    }
    
    public bool IsOnLine(Point p)
    {
        return (p.X <= Math.Max(Start.X, End.X)
                && p.X >= Math.Min(Start.X, End.X)
                && (p.Y <= Math.Max(Start.Y, End.Y)
                    && p.Y >= Math.Min(Start.Y, End.Y)));
    }

    public bool Intersect(Line line)
    {
        int dir1 = Polygon.GetDirection(Start, End, line.Start);
        int dir2 = Polygon.GetDirection(Start, End, line.End);
        int dir3 = Polygon.GetDirection(line.Start, line.End, Start);
        int dir4 = Polygon.GetDirection(line.Start, line.End, End);
        
        if (dir1 != dir2 && dir3 != dir4)
            return true;
        
        if (dir1 == 0 && IsOnLine(line.Start))
            return true;
        
        if (dir2 == 0 &&IsOnLine(line.Start))
            return true;
        
        if (dir3 == 0 && line.IsOnLine(Start))
            return true;

        if (dir4 == 0 && line.IsOnLine(Start))
            return true;

        return false;
    }
}