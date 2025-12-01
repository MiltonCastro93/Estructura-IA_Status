using UnityEngine;

public class OneHiddenOut : BaseEscondite, IAction, IModeHidden
{
    





    public void Ejecuted() //el RayCast Ejecutara la animacion
    {
        anim.SetBool("Open", true);

    }

    public void InAccion() //Animacion Mueble -> Animacion Player
    {
        if(playerWalking && playernear)
        {
            playerWalking.PreHiddenAnimation();
        }

    }

    public Vector3 OutHidden() => REFouthidden.position;
    public Vector3 PosHidden() => REFposthidden.position;



    public Quaternion PosHiddenRotation()
    {
        throw new System.NotImplementedException();
    }

    public void ResetRotation()
    {
        throw new System.NotImplementedException();
    }

    public void RotOutHidden(Vector2 valueX)
    {
        throw new System.NotImplementedException();
    }

}
