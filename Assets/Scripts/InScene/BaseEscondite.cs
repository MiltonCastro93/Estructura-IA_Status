using UnityEngine;

public abstract class BaseEscondite : MonoBehaviour
{
    protected Animator anim;
    protected Vector3 InHidden = Vector3.zero;

    [SerializeField] protected Transform REFposthidden, REFouthidden;


    //Aplicar logica de animacion
    [SerializeField] protected string TypeTriggerPrehidden = "DownHidden";
    [SerializeField] protected string TypeTriggerOuthidden = "OutDownHidden";
    [SerializeField] protected PlayerWalking player;
    protected bool playerIN = false;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        InHidden = REFposthidden.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerWalking>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(player != null)
        {
            player = null;
        }
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
