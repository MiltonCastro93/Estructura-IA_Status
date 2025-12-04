using UnityEngine;

public abstract class BaseEscondite : MonoBehaviour
{
    protected Animator anim;

    protected Quaternion baseRotation;
    protected Vector3 InHidden = Vector3.zero;

    [SerializeField] protected Transform REFposthidden, REFouthidden;
    [SerializeField] protected float RotMax = 45f;

    protected bool playerIN = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
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
