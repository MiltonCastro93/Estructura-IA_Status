using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
public abstract class CharacterHuman : CharacterInput
{
    protected CharacterController _cc;
    protected enum State { Idle, Walking, Running, Crouch, CrouchWalking, Hidden, Tilt, TiltWalking, TiltRunning }
    [SerializeField] protected State CurrentState;
    Vector2 TiltOrientacion = Vector2.zero;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake()
    {
        base.Awake();
        _cc = GetComponent<CharacterController>();
    }

    //Regula los estados del Personaje
    protected override void Update()
    {
        base.Update();
        

    }


    //Modifico el metodo de agacharse
    protected override void OnCrouchPerformed(InputAction.CallbackContext ctx)
    {
        base.OnCrouchPerformed(ctx);
        if(_cc.velocity.sqrMagnitude <= 30f)
        {
            CurrentState = CurrentState != State.Crouch ? State.Crouch : State.Idle;
        }


    }

    //modifico el metodo de correr
    protected override void OnRun(InputAction.CallbackContext ctx)
    {
        if (CurrentState == State.Tilt)
        {
            CurrentState = State.TiltRunning;
            return;
        }

        CurrentState = State.Running;
    }

    protected override void FinishRun(InputAction.CallbackContext ctx)
    {
        base.FinishRun(ctx);
        CurrentState = State.Idle;
    }


    protected override void Tilts()
    {
        base.Tilts();
        TiltOrientacion = inputs.Player.Tilt.ReadValue<Vector2>();
        if (CurrentState == State.TiltRunning)
        {
            return;
        }
        CurrentState = State.Tilt;

    }

}
