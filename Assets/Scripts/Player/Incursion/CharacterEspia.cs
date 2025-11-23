using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.WSA;

public class CharacterEspia : CharacterPlayer
{


    protected override void Update()
    {
        base.Update();

        isTilt = inputs.Player.Tilt.ReadValue<Vector2>() == Vector2.zero ? false : true;

    }

    protected override void Turn(Vector2 Action)
    {
        Debug.Log("Se esta pensando en borrar");
    }

    public InputSystem_Actions GetInputs()
    {
        return inputs;
    }

}
