
using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;

    public int Count => waypoints.Length;

    public Vector3 GetPosition(int index)
    {
        return waypoints[index].position;
    }

    private void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length == 0)
            return;

        Gizmos.color = Color.yellow;

        for (int i = 0; i < waypoints.Length; i++)
        {
            if (waypoints[i] == null)
                continue;

            Gizmos.DrawSphere(waypoints[i].position, 0.25f);

            int next = (i + 1) % waypoints.Length;

            if (waypoints[next] != null)
            {
                Gizmos.DrawLine(
                    waypoints[i].position,
                    waypoints[next].position
                );
            }
        }
    }
}
