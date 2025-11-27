using UnityEditor.UIElements;
using UnityEngine;

public class CastObjectRayItem : MonoBehaviour
{
    [SerializeField] float distanceFire = 3f;
    [SerializeField] Vector3 positionHidden = Vector3.zero;

    public bool RayFire()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, transform.forward, out hit, distanceFire))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red, 1f);

            Ifurniture mueble = hit.collider.gameObject.GetComponent<Ifurniture>();

            if (mueble != null)
            {
                positionHidden = mueble.EjecutedPos();
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

}
