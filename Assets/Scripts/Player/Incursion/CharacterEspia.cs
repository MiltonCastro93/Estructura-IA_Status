using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.WSA;

public class CharacterEspia : CharacterPlayer
{
    [SerializeField] private bool IsHidding = false;

    protected override void Update()
    {
        if (!IsHidding)
        {
            base.Update();

            isTilt = inputs.Player.Tilt.ReadValue<Vector2>() == Vector2.zero ? false : true;
        }


    }





    public InputSystem_Actions GetInputs() => inputs;
    public void SetStatus(bool status){
        IsHidding = status;
    }

    public bool GetStatus() => IsHidding;

}
