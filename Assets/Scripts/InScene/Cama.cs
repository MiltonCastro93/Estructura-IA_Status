using UnityEngine;

public class Cama : MonoBehaviour, Ifurniture
{
    [SerializeField] private Transform PostHidden;

    [SerializeField] private Vector3 InHidden = Vector3.zero;

    //Se hace un returns de valores necesarios para esconderse
    public Vector3 EjecutedPos() => transform.TransformDirection(PostHidden.position);//Pasarle la rotacion tambien, sino genera molestias para ver
    public Quaternion EjecutedRot() => PostHidden.rotation;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(InHidden, new Vector3(1f, 2f, 1f));

        Gizmos.color = Color.red;
        Gizmos.DrawLine(InHidden, InHidden + (PostHidden.forward * 2f));

    }


}
