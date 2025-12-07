using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class CastObjectRayItem : MonoBehaviour
{
    [SerializeField] float distanceFire = 3f;

    Vector3 positionHidden = Vector3.zero; //Posicion para esconderse
    Quaternion rotationHidden = Quaternion.identity; //Rotacion para esconderse
    Vector3 outHidden = Vector3.zero; //Posicion para salir del escondite

    IAction accionMueble;

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

                IModeHidden GetMueble = hit.collider.GetComponent<IModeHidden>();

                if(GetMueble != null)
                {
                    accionMueble = action; //para obtener las dos acciones necesarias para Open() y Close()

                    positionHidden = GetMueble.PosHidden(); //Paso el vector para esconderse
                    rotationHidden = GetMueble.PosHiddenRotation(); //Paso el Quaternion
                    outHidden = GetMueble.PosOutHidden(); //Punto de salida

                    return true;
                }

            }

        }

        return false;
    }

    public Vector3 ModeHiddent() => positionHidden;
    public Quaternion RotModeHidden() => rotationHidden;
    public Vector3 ModoOutHidden() => outHidden; //Salida
    public IAction AccionMueble() => accionMueble;

}
