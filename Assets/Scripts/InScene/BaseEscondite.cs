using UnityEngine;

public abstract class BaseEscondite : MonoBehaviour
{

    protected Vector3 InHidden = Vector3.zero;

    [SerializeField] protected Transform RefPostHidden, RefOutHidden; //Entrada y Salida del escondite


    //Aplicar logica de animacion
    [SerializeField] protected string TypeTriggerPrehidden = "DownHidden"; //Trigger para la animacion del Player (Entrada)
    [SerializeField] protected string TypeTriggerOuthidden = "OutDownHidden"; //Trigger para la animacion del Player (Salida)
    [SerializeField] protected PlayerWalking player;
    protected bool playerIN = false;

    //Tipo de Escondite
    protected enum TypeEscondite { Bajo, Medio, Alto}
    [SerializeField] protected TypeEscondite typeEscondite;

    protected virtual void Awake()
    {
        InHidden = RefPostHidden.position;
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
        Gizmos.DrawLine(InHidden, InHidden + (RefPostHidden.forward * 2f));

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(RefOutHidden.position, new Vector3(1f, 2f, 1f));

    }

}
