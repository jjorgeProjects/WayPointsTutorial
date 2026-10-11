
using System.Collections.Generic;
using UnityEngine;

public class WaypointNode : MonoBehaviour
{
    [SerializeField]
    private List<WaypointNode> neighbors = new();

    public List<WaypointNode> Neighbors => neighbors;

    public Vector3 Position => transform.position;

    private void OnValidate()
        {
            // Remove null references, self-connections and duplicates
            neighbors.RemoveAll(n => n == null || n == this);

            for (int i = neighbors.Count - 1; i >= 0; i--)
            {
                if (neighbors.IndexOf(neighbors[i]) != i)
                    neighbors.RemoveAt(i);
            }

            // Ensure bidirectional connections
            foreach (WaypointNode neighbor in neighbors)
            {
                if (!neighbor.neighbors.Contains(this))
                {
                    neighbor.neighbors.Add(this);
                }
            }
    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position, 0.25f);

        Gizmos.color = Color.yellow;

        foreach (WaypointNode neighbor in neighbors)
        {
            if (neighbor != null)
            {
                Gizmos.DrawLine(
                    transform.position,
                    neighbor.transform.position
                );
            }
        }
    }
    */
    private void OnDrawGizmos()
    {
        // Draw waypoint
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(Position, 0.25f);

        // Draw connections
        Gizmos.color = Color.yellow;

        foreach (WaypointNode neighbor in neighbors)
        {
            if (neighbor != null)
            {
                Gizmos.DrawLine(
                    Position,
                    neighbor.Position
                );
            }
        }
    }
}
