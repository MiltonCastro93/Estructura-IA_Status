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
    ISpecialHidden specialHidden;

    public bool RayFire() //Logica Aplicada, es true si ofrece un metodo para esconderse
    {
        RaycastHit hit;

        if (!Physics.Raycast(transform.position, transform.forward, out hit, distanceFire)) return false;
        Debug.DrawLine(transform.position, hit.point, Color.red, 1f);

        accionMueble = null; //para obtener las dos acciones necesarias para Open() y Close()
        specialHidden = null;

        IModeHidden hidden = null;

        var components = hit.collider.GetComponents<MonoBehaviour>();
        foreach(var c in components) 
        {
            if(c is IAction action)
            {
                accionMueble = action;
            }
            if(c is IModeHidden modeHidden)
            {
                hidden = modeHidden;
            }
            if(c is ISpecialHidden special)
            {
                specialHidden = special;
            }
        }
        if(accionMueble == null)
        {
            return false;
        }

        //Acción siempre existe
        accionMueble.Ejecuted();
        //Hidden es opcional
        if(hidden != null)
        {
            positionHidden = hidden.PosHidden();//Paso el vector para esconderse
            rotationHidden = hidden.PosHiddenRotation();//Paso el Quaternion
            outHidden = hidden.PosOutHidden();//Punto de salida static
            return true;
        }
        return false;
    }

    public Vector3 ModeHiddent() => positionHidden;
    public Quaternion RotModeHidden() => rotationHidden;
    public Vector3 ModoOutHidden() => outHidden; //Salida
    public IAction AccionMueble() => accionMueble;
    public ISpecialHidden SpecialHidden() => specialHidden;

}
