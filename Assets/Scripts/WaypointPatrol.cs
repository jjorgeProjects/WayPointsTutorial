
using UnityEngine;

public class WaypointPatrol : MonoBehaviour
{
    [Header("Path")]
    [SerializeField] private WaypointPath path;

    [Header("Movement")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float arrivalDistance = 0.2f;
    [SerializeField] private float rotationSpeed = 5f;

    private int currentWaypoint = 0;

    private void Update()
    {
        if (path == null || path.Count == 0)
            return;

        Vector3 target = path.GetPosition(currentWaypoint);

        // Direction towards the waypoint
        Vector3 direction = target - transform.position;

        // Check if the waypoint has been reached
        if (direction.magnitude <= arrivalDistance)
        {
            NextWaypoint();
            return;
        }

        // Move towards the waypoint
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );

        // Rotate towards the movement direction
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

    private void NextWaypoint()
    {
        currentWaypoint =
            (currentWaypoint + 1) % path.Count;
    }


    private int direction = 1;

    private void NextWaypoint2()
    {
        if (path.Count <= 1)
            return;

        if (currentWaypoint == path.Count - 1)
            direction = -1;
        else if (currentWaypoint == 0)
            direction = 1;

        currentWaypoint += direction;
    }

}
