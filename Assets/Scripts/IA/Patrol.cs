using UnityEngine;

struct PatrolPoint {
    public Vector3 position;
    public float waitTime;
    public PatrolPoint(Vector3 pos, float wait) {
        position = pos;
        waitTime = wait;
    }
}


public class Patrol : IStatusRol
{
    [SerializeField] private PatrolPoint[] patrolPoints;

    public Patrol(int valuePoints) {
        patrolPoints = new PatrolPoint[valuePoints];
        Debug.Log($"Patrol started with {patrolPoints.Length} points.");
    }

    public void MeStart() {
        Debug.Log($"Patrol: {patrolPoints[0].position}.");
    }

    public void TaskUpdate() {
        Debug.Log("Patrol updating.");
    }

    public void TaskFinish() {
        Debug.Log("Patrol finished.");
    }



}
