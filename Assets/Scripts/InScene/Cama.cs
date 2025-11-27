using UnityEngine;

public class Cama : MonoBehaviour, Ifurniture
{
    [SerializeField] private Transform PostHidden;
    [SerializeField] private GameObject Player;

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (Player)
    //    {

    //    }


    //}


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        if (!Player)
    //        {
    //            Player = other.gameObject;
    //            Player.GetComponentInChildren<HolderCamera>().SetRelativeMueble(this);
    //        }
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (Player)
    //    {
    //        Player.GetComponentInChildren<HolderCamera>().SetRelativeMueble(null);
    //        Player = null;            
    //    }
    //}



    public Vector3 EjecutedPos() => transform.TransformDirection(PostHidden.position);


}
