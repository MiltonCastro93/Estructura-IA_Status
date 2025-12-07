using UnityEngine;

public class OneHiddenOut : BaseEscondite, IAction, IModeHidden
{
    protected Animator anim;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
    }

    public void InAccion() //Animacion Mueble -> Animacion Player
    {
        if (player)
        {
            if (!playerIN)
            {
                player.GetComponent<PlayerWalking>().PreAccion(TypeTriggerPrehidden);
                playerIN = true;
            }
            else
            {
                player.GetComponent<PlayerWalking>().PreAccion(TypeTriggerOuthidden);
                playerIN = false;
            }

        }

    }

    //Implementacion de IAction
    public void Ejecuted() //el RayCast Ejecutara la animacion
    {
        if(player == null) return;
        anim.SetBool("Open", true);
    }

    //Implementacion de IAction
    public void Reverses() //para que sea llamado para cerrar la puerta
    {
        anim.SetBool("Open", false);
    }

    //Implementacion de IModeHidden
    public Vector3 PosHidden() => RefPostHidden.position; //Posicion pre establecida para esconderse
    public Quaternion PosHiddenRotation() => RefOutHidden.rotation; //Rotacion de personaje segun el mueble
    public virtual Vector3 PosOutHidden() => RefOutHidden.position; //Posicion pre establecida para salir

}
