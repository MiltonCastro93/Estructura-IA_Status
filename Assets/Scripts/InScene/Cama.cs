using UnityEngine;

public class Cama : MonoBehaviour, Ifurniture
{
    [SerializeField] private Transform PostHidden;

    public Vector3 EjecutedPos() => transform.TransformDirection(PostHidden.position);//Pasarle la rotacion tambien, sino genera molestias para ver


}
