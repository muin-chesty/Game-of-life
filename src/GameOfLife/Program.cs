
public static class Program
{
    private const int GENERATIONS = 10;

    public static int Main()
    {
/*         HashSet<(long X, long Y)> live = new();
        
        live.Add((0,1));
        live.Add((1, 2));
        live.Add((2, 0));

        for(int gen = 1; gen <= GENERATIONS; gen++)
        {
            live = Step(live);

            Console.WriteLine($"{live.Count}");
        } */
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

    /// <summary>
    /// Computes next generation from the current live cells
    /// </summary>
    private static HashSet<(long X, long Y)> Step(HashSet<(long X, long Y)> liveCells)
    {
        Dictionary<(long X, long Y), int> liveNeighbourCountByCell = new();

        foreach(var liveCell in liveCells)
        {
            List<(long X, long Y)> neighbours = GetNeighbours(liveCell.X, liveCell.Y);
            foreach(var neighbour in neighbours)
            {
                bool containsNeighbour = liveNeighbourCountByCell.ContainsKey(neighbour);
                if(containsNeighbour)
                {
                    liveNeighbourCountByCell[neighbour] ++;
                }
                else
                {
                    liveNeighbourCountByCell.Add(neighbour, 1);
                }
            }
        }

        var nextGeneration = new HashSet<(long X, long Y)>();

        foreach( var (cell, liveNeighbours) in liveNeighbourCountByCell)
        {
            // Rule: A cell is alive next if: count is 3, OR count is 2 and it is alive now
            if((liveNeighbours == 3) || (liveNeighbours == 2 && liveCells.Contains(cell)))
            {
                nextGeneration.Add(cell);
            }
        }
        return nextGeneration;
    }
}
