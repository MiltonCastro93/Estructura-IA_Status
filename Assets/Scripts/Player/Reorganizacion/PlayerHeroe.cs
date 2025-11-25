using UnityEngine;

public class PlayerHeroe : CharacterHuman
{
    [SerializeField] Transform HolderTransform;


    protected override void Update()
    {
        base.Update();

        Vector2 move = inputs.Player.Move.ReadValue<Vector2>();
        PlayerMove(new Vector3(move.x, 0, move.y), gravity);
    }

    //Metodo para mover al personaje, y tambien, aplico la gravedad
    void PlayerMove(Vector3 dir, float gravityMultiplier)
    {
        Vector3 horizontal = OrientacionPlayer(dir, CurrentState) * speed;

        if (_cc.isGrounded)
        {
            if (verticalVelocity < 0f)
                verticalVelocity = -2f;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
        }

        Vector3 final = new Vector3(horizontal.x, verticalVelocity, horizontal.z);

        _cc.Move(final * Time.deltaTime);
    }

    //Camina segun el estado del Personaje, hacia el Forward de la camara o del Personaje 
    Vector3 OrientacionPlayer(Vector3 dir, State Current)
    {
        Vector3 camForward = Current == State.Tilt ? _cc.transform.forward : HolderTransform.forward;
        Vector3 camRight = Current == State.Tilt ? _cc.transform.right : HolderTransform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        return camForward * dir.z + camRight * dir.x;
    }
}
