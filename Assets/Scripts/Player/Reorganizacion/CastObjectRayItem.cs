using UnityEditor.UIElements;
using UnityEngine;

public class CastObjectRayItem : MonoBehaviour
{
    [SerializeField] float distanceFire = 3f;
    Vector3 positionHidden = Vector3.zero;
    Quaternion rotationHidden = Quaternion.identity;

    public bool RayFire() //Logica Aplicada, es true si ofrece una interaccion como (Ventanas, Puertas, Muebles, items)
    {
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, transform.forward, out hit, distanceFire))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red, 1f);

            Ifurniture mueble = hit.collider.gameObject.GetComponent<Ifurniture>();

            if (mueble != null)
            {
                positionHidden = mueble.EjecutedPos(); //Se Obtiene el lugar donde se esconde el player
                rotationHidden = mueble.EjecutedRot(); //Se obtiene la rotacion deseada para que el jugador vea
                return true;
            }


            Debug.Log($"No es un Mueble, es un {hit.collider.transform.gameObject.name}");
        }

        return false;
    }

    public Vector3 ModeHiddent()
    {
        return positionHidden;
    }

    public Quaternion RotModeHidden()
    {

        return rotationHidden;
    }

}
