using UnityEngine;

public class Ducto : MonoBehaviour
{
    [SerializeField] private RayHeightPlayer player;// interface, para que el ia pueda usar el ducto

    private void OnTriggerEnter(Collider other)
    {
        if(player == null)
        {
            player = other.GetComponentInChildren<RayHeightPlayer>();
            player.SetForcedCrouch();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(player != null)
        {
            player = null;
        }

    }


}
