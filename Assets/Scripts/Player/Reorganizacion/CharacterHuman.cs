using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
public abstract class CharacterHuman : CharacterInput
{
    protected CharacterController _cc;
    protected enum State { Idle, Walking, Running, Crouch, CrouchWalking, CrouchTilt, Tilt, TiltWalking, TiltRunning, Hidden }
    [SerializeField] protected State CurrentState;

    protected bool IsRunning = false;

    protected Vector2 TiltOrientacion = Vector2.zero;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake()
    {
        base.Awake();
        _cc = GetComponent<CharacterController>();
    }

    //Regula los estados del Personaje
    protected virtual void Update()
    {
        TiltOrientacion = inputs.Player.Tilt.ReadValue<Vector2>();

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

        if(CurrentState == State.Tilt)
        {
            CurrentState = State.CrouchTilt;
            return;
        }

        if(CurrentState == State.CrouchTilt)
        {
            CurrentState = State.Tilt;
            return;
        }

        //Si estoy Corriendo se deslice
        if (CurrentState == State.Running)
        {
            Debug.Log("Deslizando, termina en Crouch");
        }

        
    }

    //Eventos por Hold
    protected override void OnTilts(InputAction.CallbackContext ctx)
    {
        base.OnTilts(ctx);
        
        if(CurrentState == State.Idle)
        {
            CurrentState = State.Tilt;
            return;
        }

        if(CurrentState == State.Walking)
        {
            CurrentState = State.TiltWalking;
            return;
        }

        if (CurrentState == State.Running) 
        {
            CurrentState = State.TiltRunning;
            return;
        }

        if(CurrentState == State.Crouch)
        {
            CurrentState = State.CrouchTilt;
            return;
        }

    }

    protected override void FinishTilts(InputAction.CallbackContext ctx)
    {
        base.FinishTilts(ctx);
        if(CurrentState == State.Tilt)
        {
            CurrentState = State.Idle;
            return;
        }

        if (CurrentState == State.TiltWalking)
        {
            CurrentState = State.Walking;
            return;
        }

        if(CurrentState == State.TiltRunning)
        {
            CurrentState = State.Running;
            return;
        }

        if(CurrentState == State.CrouchTilt)
        {
            CurrentState = State.Crouch;
            return;
        }

    }

    protected override void OnRun(InputAction.CallbackContext ctx)
    {
        base.OnRun(ctx);
        IsRunning = true;

        if (CurrentState == State.Walking)
        {
            CurrentState = State.Running;
            return;
        }

        if (CurrentState == State.Tilt)
        {
            CurrentState = State.TiltRunning;
            return;
        }

        if (CurrentState == State.TiltWalking)
        {
            CurrentState = State.TiltRunning;
            return;
        }

    }

    protected override void FinishRun(InputAction.CallbackContext ctx)
    {
        base.FinishRun(ctx);
        IsRunning = false;

        if (CurrentState == State.Running) {
            CurrentState = State.Walking;
            return;
        }
        if(CurrentState == State.TiltRunning)
        {
            CurrentState = State.TiltWalking;
            return;
        }


    }


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

        if (CurrentState == State.Tilt)
        {
            CurrentState = State.TiltWalking;
            return;
        }


        //camina agachado y inclinado outlast? CrouchTilt
    }

    protected override void FinishWalking(InputAction.CallbackContext ctx)
    {
        base.FinishWalking(ctx);

        if(CurrentState == State.Walking)
        {
            CurrentState = State.Idle;
            return;
        }

        if(CurrentState == State.CrouchWalking)
        {
            CurrentState = State.Crouch;
            return;
        }

        if(CurrentState == State.TiltWalking)
        {
            CurrentState = State.Tilt;
            return;
        }

    }


}
