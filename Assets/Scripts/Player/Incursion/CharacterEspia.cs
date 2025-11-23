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





    public InputSystem_Actions GetInputs() => inputs;

}
