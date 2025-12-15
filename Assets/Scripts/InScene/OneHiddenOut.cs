using UnityEngine;

public class OneHiddenOut : BaseEscondite, IAction, IModeHidden
{
    protected Animator anim;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
    }

    public void InAccion()//Metodo llamado por la animacion del mismo mueble
    {//Animacion del Mueble verifica y avisa Player que animacion debe reproducir segun su estado y tipo de escondite
        if (player)
        {
            if (!playerIN)
            {
                player.PreAccion(typeEscondite);
                playerIN = true;
            }
            else
            {
                player.PreAccion(typeEscondite);
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
    public Vector3 PosOutHidden() => RefOutHidden.position; //Posicion pre establecida para salir

}
