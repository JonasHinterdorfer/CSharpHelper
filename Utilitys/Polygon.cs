using System.Drawing;

namespace Utilitys;

public sealed class Polygon
{
    public List<Point> Corners { get; set; } = new();
    public int AmountOfCorners => Corners.Count;

    public decimal Area
    {
        get
        {
            decimal area = 0M;
            int j = AmountOfCorners - 1;
         
            for (int i = 0; i < AmountOfCorners; i++) {
                area += (Corners[j].X + Corners[i].X) * (Corners[j].Y - Corners[i].Y);
                j = i;
            }
            return area / 2M;
        }
    }

    public bool IsPointInPolygon(Point p)
    {
        // When polygon has less than 3 edge, it is not polygon
        if (AmountOfCorners < 3)
            return false;
 
        // Create a point at infinity, y is same as point p
        Point pt=new Point(9999, p.Y);
        Line exline = new Line(p,pt); 
        int count = 0;
        int i = 0;
        do {
 
            // Forming a line from two consecutive points of
            // poly
             Line side = new Line( Corners[i], Corners[(i + 1) % AmountOfCorners]);
            if (side.Intersect(exline)) {
 
                // If side is intersects exline
                if (GetDirection(side.Start, p, side.End) == 0)
                    return side.IsOnLine(p);
                count++;
            }
            i = (i + 1) % AmountOfCorners;
        } while (i != 0);
 
        // When count is odd
        return (count & 1) == 1;
    }
    
    public static int GetDirection(Point a, Point b, Point c)
    {
        int val = (b.Y - a.Y) * (c.X - b.X)
                  - (b.X - a.X) * (c.Y - b.Y);
 
        if (val == 0)
            return 0;
 
        if (val < 0)
            return 2;
        
        return 1;
    }
}