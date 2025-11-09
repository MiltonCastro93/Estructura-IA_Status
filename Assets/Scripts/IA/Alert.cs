using UnityEngine;

public class Alert : IStatusRol
{
    private GameObject player;

    public Alert(GameObject _player) {
        player = _player;
    }

    public void MeStart()
    {
        Debug.Log("Alert started.");
    }

    public void TaskUpdate()
    {
        Debug.Log("Alert updating.");
    }

    public void TaskFinish()
    {
        Debug.Log("Alert finished.");
    }

}
