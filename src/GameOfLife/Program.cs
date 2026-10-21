
public static class Program
{
    private const int GENERATIONS = 10;

    public static int Main()
    {
        var neighbours = GetNeighbours(0, 1);
        Console.WriteLine($"{neighbours.Count}");
        return 0;

    }
    /// <summary>
    /// Find 8 neighbours where possible. On edge and corner finds 5 and 3 respectively.
    /// </summary>
    private static List<(long X, long Y)> GetNeighbours(long cellX, long cellY)
    {
        List<(long X, long Y)> neighbourCoordinates = new();
        // go from x -1 to 1 and same for y.
        for(long dx = -1; dx <= 1; dx++)
        {
            for(long dy = -1; dy <= 1; dy++)
            {
                // don't count the same coordinate as neighbours. otherwise counts become 9 instead 8
                if(dx == 0 && dy == 0)
                {
                    continue;
                }
                bool isAddingAndXAtMaxEdge = cellX == long.MaxValue && dx == 1;
                bool isSubtractingAndXAtMindEdge = cellX == long.MinValue && dx == -1;
                // Skip offsets beyond 64-bit edge; adding/sub would cause crash with over/underflow
                if(isAddingAndXAtMaxEdge || isSubtractingAndXAtMindEdge)
                {
                    continue;
                }
                bool isAddingAndYAtMaxEdge = cellY == long.MaxValue && dy == 1;
                bool isSubtractingAndYAtMindEdge = cellY == long.MinValue && dy == -1;
                // Same logic as X edge. 
                if(isAddingAndYAtMaxEdge || isSubtractingAndYAtMindEdge)
                {
                    continue;
                }
                // Now add dx and dy to look for neighbours
                long neighbourX = cellX + dx;
                long neighbourY = cellY + dy;
                neighbourCoordinates.Add((neighbourX, neighbourY));
            }
        }
        return neighbourCoordinates;
    }
}
