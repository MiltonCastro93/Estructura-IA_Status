using UnityEditor.UIElements;
using UnityEngine;

public class CastObjectRayItem : MonoBehaviour
{
    [SerializeField] float distanceFire = 3f;
    Vector3 positionHidden = Vector3.zero;
    Quaternion rotationHidden = Quaternion.identity;

    Vector3 outHidden = Vector3.zero; //nuevo

    IModeHidden modeHidden;

    public bool RayFire() //Logica Aplicada, es true si ofrece un metodo para esconderse
    {
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, transform.forward, out hit, distanceFire))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red, 1f);

            IAction action = hit.collider.GetComponent<IAction>();

            if (action != null)
            {

                action.Ejecuted();

                modeHidden = hit.collider.GetComponent<IModeHidden>();

                positionHidden = modeHidden.PosHidden(); //Paso el vector para esconderse
                rotationHidden = modeHidden.PosHiddenRotation(); //Paso el Quaternion
                outHidden = modeHidden.OutHidden(); //el punto de salida

            }
            else
            {
                Debug.Log("No posee animaciones");
            }

            //IModeHidden modeHidden = hit.collider.GetComponent<IModeHidden>();

            //modeHidden = hit.collider.GetComponent<IModeHidden>();

            //if (modeHidden != null)
            //{

            //    positionHidden = modeHidden.PosHidden(); //Paso el vector para esconderse
            //    rotationHidden = modeHidden.PosHiddenRotation(); //Paso el Quaternion
            //    outHidden = modeHidden.OutHidden();

            //    return true;
            //}
            //else
            //{
            //    Debug.Log("No posee logica");
            //}


            Debug.Log($"No es un Mueble, es un {hit.collider.transform.gameObject.name}");
        }

        return false;
    }

    public Vector3 ModeHiddent() => positionHidden;
    public Quaternion RotModeHidden() => rotationHidden;
    public Vector3 ModoOutHidde() => outHidden; //Salida
    public IModeHidden MuebleCurrent() => modeHidden; //con esto llamare a la Animacion de apertura asi el Jugador puede salir

}
