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
    protected virtual void Update()
    {



    }


    //Eventos por Touchs
    protected override void OnCrouchPerformed(InputAction.CallbackContext ctx)
    {
        base.OnCrouchPerformed(ctx);

        if (CurrentState == State.Idle)
        {
            CurrentState = State.Crouch;
            return;
        }

        if (CurrentState == State.Walking)
        {
            CurrentState = State.CrouchWalking;
            return;
        }

        if (CurrentState == State.Crouch)
        {
            CurrentState = State.Idle;
            return;
        }

        if(CurrentState == State.CrouchWalking)
        {
            CurrentState = State.Walking;
            return;
        }

        //Si estoy Corriendo se deslice
    }

    //Eventos por Hold
    protected override void OnTilts(InputAction.CallbackContext ctx)
    {
        base.OnTilts(ctx);
        TiltOrientacion = inputs.Player.Tilt.ReadValue<Vector2>();
        if (CurrentState == State.TiltRunning)
        {
            return;
        }
        CurrentState = State.Tilt;

    }

    protected override void OnRun(InputAction.CallbackContext ctx)
    {
        base.OnRun(ctx);
        if(CurrentState == State.Walking)
        {
            CurrentState = State.Running;
        }

        if (CurrentState == State.Tilt)
        {
            CurrentState = State.TiltRunning;
        }

    }

    protected override void FinishRun(InputAction.CallbackContext ctx)
    {
        base.FinishRun(ctx);
        if (CurrentState == State.Running) {
            CurrentState = State.Walking;
        }

    }

    //26/11/2025
    protected override void OnWalking(InputAction.CallbackContext ctx)
    {
        base.OnWalking(ctx);
        if (CurrentState == State.Idle)
        {
            CurrentState = State.Walking;
            return;
        }

        if (CurrentState == State.Crouch)
        {
            CurrentState = State.CrouchWalking;
            return;
        }

    }

    protected override void FinishWalking(InputAction.CallbackContext ctx)
    {
        base.FinishWalking(ctx);

        if(CurrentState == State.Walking)
        {
            CurrentState = State.Idle;
        }

        if(CurrentState == State.CrouchWalking)
        {
            CurrentState = State.Crouch;
        }

    }

}
