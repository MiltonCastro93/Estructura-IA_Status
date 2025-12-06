using UnityEngine;

public class OneHiddenOut : BaseEscondite, IAction, IModeHidden
{

    public void Ejecuted() //el RayCast Ejecutara la animacion
    {
        anim.SetBool("Open", true);
        Debug.Log("Abriendo Mueble");
    }

    public void InAccion() //Animacion Mueble -> Animacion Player
    {
        if (player)
        {
            if (!playerIN)
            {
                Debug.Log("Player Dentro del Mueble");
                player.GetComponent<PlayerWalking>().PreAccion(TypeTriggerPrehidden);
                playerIN = true;
            }
            else
            {
                Debug.Log("Player Fuera del Mueble");
                player.GetComponent<PlayerWalking>().PreAccion(TypeTriggerOuthidden);
                playerIN = false;
            }

        }

    }

    public void Reverses() //para que sea llamado para cerrar la puerta
    {
        anim.SetBool("Open", false);
        Debug.Log("Cerrando Mueble");
    }


    public Vector3 PosHidden() => REFposthidden.position; //Posicion pre establecida para esconderse
    public Quaternion PosHiddenRotation() => REFouthidden.rotation; //Rotacion de personaje segun el mueble
    public Vector3 PosOutHidden() => REFouthidden.position; //Posicion pre establecida para salir

}
