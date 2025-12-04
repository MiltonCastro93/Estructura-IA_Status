using UnityEngine;

public class OneHiddenOut : BaseEscondite, IAction
{


    private void ClosedAnimation()
    {
        anim.SetBool("Open", false);
    }

    public void Ejecuted() //el RayCast Ejecutara la animacion
    {
        anim.SetBool("Open", true);

    }

    public void InAccion() //Animacion Mueble -> Animacion Player
    {
        if(playerWalking && playernear)
        {
            if (!playerIN)
            {
                //playerWalking.PreHiddenAnimation();
                playerIN = true;
            }
            else
            {

                playerIN = false;
            }

        }


        ClosedAnimation();
    }


    public Vector3 OutHidden() => REFouthidden.position; //Posicion pre establecida para salir
    public Vector3 PosHidden() => REFposthidden.position; //Posicion pre establecida para esconderse
    public Quaternion PosHiddenRotation() => REFouthidden.rotation; //Rotacion de personaje segun el mueble

}
