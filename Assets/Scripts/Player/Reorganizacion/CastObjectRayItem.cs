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
            Debug.Log($"Choco contra {hit.collider.transform.gameObject.name}");
            Debug.DrawLine(transform.position, hit.point, Color.red, 1f);

            Ifurniture mueble = hit.collider.gameObject.GetComponent<Ifurniture>();

            if (mueble != null)
            {
                positionHidden = GetHiddentPos(mueble);
                return true;
            }
            
        }

        return false;
    }

    private Vector3 GetHiddentPos(Ifurniture obj)
    {
        return obj.EjecutedPos();
    }

    public Vector3 ModeHiddent()
    {
        return positionHidden;
    }

}
