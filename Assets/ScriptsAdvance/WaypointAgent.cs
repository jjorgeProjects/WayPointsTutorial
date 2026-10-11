
using System.Collections.Generic;
using UnityEngine;

public class WaypointAgent : MonoBehaviour
{
    [Header("Navigation")]
    [SerializeField] private WaypointGraph graph;
    [SerializeField] private WaypointNode destination;

    [Header("Movement")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float arrivalDistance = 0.2f;

    private List<WaypointNode> path;
    private int currentIndex;

    private void Start()
    {
        CalculatePath();
    }

    public void CalculatePath()
    {
        if (graph == null || destination == null)
            return;

        WaypointNode start = graph.GetClosestNode(
            transform.position
        );

        path = WaypointAStar.FindPath(start, destination);
        currentIndex = 0;

        if (path == null)
        {
            Debug.LogWarning("No path found!");
        }
    }

    private void Update()
    {
        if (path == null || currentIndex >= path.Count)
            return;

        Vector3 target = path[currentIndex].Position;
        Vector3 direction = target - transform.position;

        // Check if current waypoint was reached
        if (direction.magnitude <= arrivalDistance)
        {
            currentIndex++;
            return;
        }

        // Move towards waypoint
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );

        // Rotate agent towards waypoint
        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (path == null)
            return;

        Gizmos.color = Color.green;

        for (int i = 0; i < path.Count - 1; i++)
        {
            if (path[i] != null && path[i + 1] != null)
            {
                Gizmos.DrawLine(
                    path[i].Position,
                    path[i + 1].Position
                );
            }
        }
    }

    public void SetDestination(WaypointNode newDestination)
    {
        destination = newDestination;
        CalculatePath();
    }
}
