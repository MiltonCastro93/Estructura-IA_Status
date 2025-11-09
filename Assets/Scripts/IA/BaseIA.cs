using UnityEngine;

public class BaseIA : MonoBehaviour
{
    public GameObject player;
    private IStatusRol currentStatus;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        currentStatus = new Patrol(5);
    }

    // Update is called once per frame
    void Update() {
        currentStatus?.TaskUpdate();

        if (Input.GetKeyDown(KeyCode.Q)) {
            currentStatus = ChangeStatus(new Patrol(5));
            Debug.Log("Patrol State");
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            currentStatus = ChangeStatus(new Alert(player));
            Debug.Log("Alert State");
        }


    }

    private IStatusRol ChangeStatus(IStatusRol newStatus) {
        currentStatus.TaskFinish();
        newStatus.MeStart();
        return newStatus;
    }

}
