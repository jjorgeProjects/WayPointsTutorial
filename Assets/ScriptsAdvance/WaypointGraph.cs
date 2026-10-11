
using System.Collections.Generic;
using UnityEngine;

public class WaypointGraph : MonoBehaviour
{
    [SerializeField]
    private List<WaypointNode> nodes = new();

    public List<WaypointNode> Nodes => nodes;

    public WaypointNode GetClosestNode(Vector3 position)
    {
        
        WaypointNode closest = null;
        float minDistance = Mathf.Infinity;

        foreach (WaypointNode node in nodes)
        {
            if (node == null)
                continue;

            float distance = Vector3.SqrMagnitude(
                node.Position - position
            );

            if (distance < minDistance)
            {
                minDistance = distance;
                closest = node;
            }
        }

        return closest;
    }

    [ContextMenu("Find Child Nodes")]
    public void FindChildNodes()
    {
        nodes = new List<WaypointNode>(
            GetComponentsInChildren<WaypointNode>()
        );
    }
}
