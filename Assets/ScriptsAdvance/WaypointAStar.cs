
using System.Collections.Generic;
using UnityEngine;

public static class WaypointAStar
{
    public static List<WaypointNode> FindPath(
        WaypointNode start,
        WaypointNode goal)
    {
        if (start == null || goal == null)
            return null;

        List<WaypointNode> openSet = new() { start };
        HashSet<WaypointNode> closedSet = new();

        Dictionary<WaypointNode, WaypointNode> cameFrom = new();
        Dictionary<WaypointNode, float> gScore = new();
        Dictionary<WaypointNode, float> fScore = new();

        gScore[start] = 0f;
        fScore[start] = Heuristic(start, goal);

        while (openSet.Count > 0)
        {
            // Find node with lowest fScore
            WaypointNode current = GetLowestF(
                openSet, fScore
            );

            // Goal reached
            if (current == goal)
                return ReconstructPath(cameFrom, current);

            openSet.Remove(current);
            closedSet.Add(current);

            foreach (WaypointNode neighbor in current.Neighbors)
            {
                if (neighbor == null ||
                    closedSet.Contains(neighbor))
                    continue;

                float tentativeG = gScore[current] +
                    Vector3.Distance(
                        current.Position,
                        neighbor.Position
                    );

                if (!gScore.ContainsKey(neighbor) ||
                    tentativeG < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeG;

                    fScore[neighbor] = tentativeG +
                        Heuristic(neighbor, goal);

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        // No path found
        return null;
    }

    private static float Heuristic(
        WaypointNode a,
        WaypointNode b)
    {
        return Vector3.Distance(a.Position, b.Position);
    }

    private static WaypointNode GetLowestF(
        List<WaypointNode> openSet,
        Dictionary<WaypointNode, float> fScore)
    {
        WaypointNode best = openSet[0];

        foreach (WaypointNode node in openSet)
        {
            if (fScore[node] < fScore[best])
                best = node;
        }

        return best;
    }

    private static List<WaypointNode> ReconstructPath(
        Dictionary<WaypointNode, WaypointNode> cameFrom,
        WaypointNode current)
    {
        List<WaypointNode> path = new() { current };

        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Add(current);
        }

        path.Reverse();
        return path;
    }
}
