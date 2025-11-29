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

            if (CurrentState == State.CrouchWalking)
            {
                CurrentState = State.Walking;
                return;
            }

            if (CurrentState == State.Tilt)
            {
                CurrentState = State.CrouchTilt;
                return;
            }

            if (CurrentState == State.CrouchTilt)
            {
                CurrentState = State.Tilt;
                return;
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

        if (CurrentState == State.Tilt)
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

        if(CurrentState == State.OutHidden)
        {
            CurrentState = State.Walking;
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
