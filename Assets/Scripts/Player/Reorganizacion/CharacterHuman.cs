using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
public abstract class CharacterHuman : CharacterInput
{
    protected CharacterController _cc;
    protected enum State { Idle, Walking, Running, Crouch, CrouchWalking, CrouchTilt, Tilt, TiltWalking, TiltRunning, PreHidden, Hidden, OutHidden }
    [SerializeField] protected State CurrentState;

    protected bool IsRunning = false;
    protected bool IsCrouch = false;

    [SerializeField] protected CastObjectRayItem rayItem;
    [SerializeField] protected RayHeightPlayer PiesAltura;

    protected Vector3 OldPosition = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake()
    {
        base.Awake();
        _cc = GetComponent<CharacterController>();

        if (rayItem == null)
        {
            Debug.LogError("Este GO No Posee la logica de deteccion");
            return;
        }

    }


    protected virtual void Update()
    {


    }

    protected virtual void LateUpdate()
    {
        if (CurrentState == State.PreHidden)
        {
            CurrentState = State.Hidden;
            _cc.transform.position = rayItem.ModeHiddent();
            _cc.transform.rotation = rayItem.RotModeHidden();
            return;
        }

        if (CurrentState == State.OutHidden)
        {
            if (!IsCrouch)
            {
                _cc.transform.position = new Vector3(OldPosition.x, 1.50f, OldPosition.z);
            }

            return;
        }


    }


    //Eventos por Touchs
    protected override void OnCrouchPerformed(InputAction.CallbackContext ctx)
    {
        
        if (!PiesAltura.GetForcedCrouch() && CurrentState != State.Hidden)
        {
            base.OnCrouchPerformed(ctx);
            IsCrouch = !IsCrouch;

            switch (CurrentState)
            {
                case State.Idle:
                    CurrentState = State.Crouch;
                    break;
                case State.Walking:
                    CurrentState = State.CrouchWalking;
                    break;
                case State.Crouch:
                    CurrentState = State.Idle;
                    break;
                case State.CrouchWalking:
                    CurrentState = State.Walking;
                    break;
                case State.Tilt:
                    CurrentState = State.CrouchTilt;
                    break;
                case State.CrouchTilt:
                    CurrentState = State.Tilt;
                    break;

            }

        }
        else
        {
            PiesAltura.EjecutaComprobacionAltura();
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

        switch (CurrentState)
        {
            case State.Idle:
                CurrentState = State.Tilt;
                break;
            case State.Walking:
                CurrentState = State.TiltWalking;
                break;
            case State.Running:
                CurrentState = State.TiltRunning;
                break;
            case State.Crouch:
                CurrentState = State.CrouchTilt;
                break;

        }

    }

    protected override void FinishTilts(InputAction.CallbackContext ctx)
    {
        base.FinishTilts(ctx);

        switch (CurrentState)
        {
            case State.Tilt:
                CurrentState = State.Idle;
                break;
            case State.TiltWalking:
                CurrentState = State.Walking;
                break;
            case State.TiltRunning:
                CurrentState = State.Running;
                break;
            case State.CrouchTilt:
                CurrentState = State.Crouch;
                break;
        }

    }

    protected override void OnRun(InputAction.CallbackContext ctx)
    {
        base.OnRun(ctx);
        IsRunning = true;

        switch (CurrentState)
        {
            case State.Walking:
                CurrentState = State.Running;
                break;
            case State.Tilt:
                CurrentState = State.TiltRunning;
                break;
            case State.TiltWalking:
                CurrentState = State.TiltRunning;
                break;

        }

    }

    protected override void FinishRun(InputAction.CallbackContext ctx)
    {
        base.FinishRun(ctx);
        IsRunning = false;

        switch (CurrentState)
        {
            case State.Running:
                CurrentState = State.Walking;
                break;
            case State.TiltRunning:
                CurrentState = State.TiltWalking;
                break;

        }


    }


    protected override void OnWalking(InputAction.CallbackContext ctx)
    {
        base.OnWalking(ctx);

        switch (CurrentState)
        {
            case State.Idle:
                CurrentState = State.Walking;
                break;
            case State.Crouch:
                CurrentState = State.CrouchWalking;
                break;
            case State.Tilt:
                CurrentState = State.TiltWalking;
                break;
            case State.OutHidden:
                CurrentState = State.Walking;
                break;
        }

        //camina agachado y inclinado outlast? CrouchTilt
    }

    protected override void FinishWalking(InputAction.CallbackContext ctx)
    {
        base.FinishWalking(ctx);

        switch (CurrentState)
        {
            case State.Walking:
                CurrentState = State.Idle;
                break;
            case State.CrouchWalking:
                CurrentState = State.Crouch;
                break;
            case State.TiltWalking:
                CurrentState = State.Tilt;
                break;

        }

    }

    protected override void OnFire(InputAction.CallbackContext ctx) //"Castea" el objeto. es un Item o algo Interactable?
    {
        base.OnFire(ctx);
        if (rayItem != null)
        {
            if (rayItem.RayFire())
            {
                if(CurrentState != State.Hidden)
                {
                    CurrentState = State.PreHidden;
                    return;
                }

            }

        }

        if (CurrentState == State.Hidden)
        {
            CurrentState = State.OutHidden;
            return;
        }

    }

}
