using UnityEngine;

public class RayHeightPlayer : MonoBehaviour
{
    private bool ForcedCrouch = false;
    private bool ActivateComprobation = false;
    [SerializeField] LayerMask layerMask;

    private void LateUpdate()
    {
        if (ForcedCrouch) //Faltaria comprobar si no tiene techo tambien me pueda parar
        {
            Ray origen = new Ray(transform.position, Vector3.up);

            RaycastHit hit;

            if (Physics.Raycast(origen, out hit, 10f, layerMask))
            {
                if (ActivateComprobation)
                {
                    if (hit.distance <= 1f)
                    {
                        Debug.Log("Aun estas dentro");
                        return;
                    }
                    else
                    {
                        ForcedCrouch = false;
                        Debug.Log("Te podes parar, y saltar");
                    }
                    ActivateComprobation = false;
                }

                Debug.DrawLine(origen.origin, hit.point, Color.yellow);
            }




        }

    }


    public void EjecutaComprobacionAltura()
    {
        ActivateComprobation = true;
    }

    public void SetForcedCrouch()
    {
        ForcedCrouch = true;
    }

    public bool GetForcedCrouch() => ForcedCrouch;



}
