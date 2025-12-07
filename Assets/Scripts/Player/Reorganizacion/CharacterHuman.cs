using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
public abstract class CharacterHuman : CharacterInput
{
    protected CharacterController _cc;
    protected Animator _anim;

    protected enum State { Idle, Walking, Running, Crouch, CrouchWalking, CrouchTilt, Tilt, TiltWalking, TiltRunning, PreHidden, Hidden, OutHidden }
    [SerializeField] protected State CurrentState;

    protected bool IsRunning = false;//Para controlar si esta corriendo esto funciona para detecter si estoy en Running y TiltRunning
    protected bool IsCrouch = false;

    [SerializeField] protected RayHeightPlayer PiesAltura;
    [SerializeField] protected CastObjectRayItem rayItem;//Lee los objetos interactuables con clic
    protected IAction GetCurrentMueble; 

    protected Vector3 OldPosition = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake()
    {
        base.Awake();
        _cc = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();

        if (rayItem == null)
        {
            Debug.LogError("Este GO No Posee la logica de deteccion");
            return;
        }

        
    }

    //Eventos por Touchs
    protected override void OnCrouch(InputAction.CallbackContext ctx)
    {
        
        if (!PiesAltura.GetForcedCrouch() && CurrentState != State.Hidden)
        {
            base.OnCrouch(ctx);
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
                GetCurrentMueble = rayItem.AccionMueble(); //Obtengo la interfaz del mueble, asi puedo abrir y cerrar
            }

        }

        if (CurrentState == State.Hidden)//aplicar animacion
        {
            GetCurrentMueble.Ejecuted();//Si estoy escondido, se Abre mueble
            return;
        }

    }

    //Animar y desacoplar acciones
    public virtual void PreAccion(string TriggerType) //Mueble (InAccion()) -> Personaje Cambia entre estados segundo la accion actual -> Proxima
    {
        if(CurrentState == State.Hidden)
        {
            CurrentState = State.OutHidden;
        }
        else
        {
            CurrentState = State.PreHidden;
        }

        _anim.SetTrigger(TriggerType);
        _cc.enabled = false;
    }

    protected virtual void OnHiddenAnimation(string TriggerType) //Personaje -> Mueble "Cambia a modo hidden y reproducir la animacion del cierre del mueble"
    {
        CurrentState = State.Hidden;
        _anim.ResetTrigger(TriggerType);

        GetCurrentMueble.Reverses();

        transform.position = rayItem.ModeHiddent();
        transform.rotation = rayItem.RotModeHidden();

        _cc.enabled = true;
    }

    protected virtual void OutHiddenAnimation(string TriggerType) //Personaje ->Mueble "Cambia a modo idle y reproducir la animacion del Cierre del mueble"
    {
        _anim.ResetTrigger(TriggerType);
        GetCurrentMueble.Reverses();

        transform.position = rayItem.ModoOutHidden(); //posicion de salida del escondite
        _cc.enabled = true;

        CurrentState = State.Idle;
    }



    private void OnAnimatorMove()
    {
        if(!_cc.enabled)
        {
            transform.position += _anim.deltaPosition;
            transform.rotation *= _anim.deltaRotation;
        }

    }


}
