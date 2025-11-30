using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseEscondite : MonoBehaviour
{
    protected Quaternion baseRotation;

    [SerializeField] protected Transform REFposthidden, REFouthidden;

    [SerializeField] protected Vector3 InHidden = Vector3.zero;

    [SerializeField] protected float RotMax = 45f;

    private void Awake()
    {
        InHidden = REFposthidden.position;
        baseRotation = REFposthidden.localRotation;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(InHidden, new Vector3(1f, 2f, 1f));

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(InHidden, InHidden + (REFposthidden.forward * 2f));

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(REFouthidden.position, new Vector3(1f, 2f, 1f));

    }

}
