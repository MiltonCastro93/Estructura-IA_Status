using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
public abstract class CharacterHuman : CharacterInput
{
    protected CharacterController _cc;

    protected enum MainState { Idle, Walking, Running, Crouch, CrouchWalking, CrouchTilt, Tilt, TiltWalking, TiltRunning, Action }
    [SerializeField] protected MainState CurrentState;

    protected enum SubState { None, PreHidden, Hidden, OutHidden }
    [SerializeField] protected SubState CurrentSubState = SubState.None;

    protected bool IsRunning = false;//Para controlar si esta corriendo esto funciona para detecter si estoy en Running y TiltRunning
    protected bool IsCrouch = false;

    [SerializeField] protected RayHeightPlayer PiesAltura;
    [SerializeField] protected CastObjectRayItem rayItem;//Lee los objetos interactuables con clic
    protected IAction GetCurrentMueble; 
    protected ISpecialHidden GetSpecialHidden;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake()
    {
        base.Awake();
        _cc = GetComponent<CharacterController>();

    }

    //Eventos por Touchs
    protected override void OnCrouch(InputAction.CallbackContext ctx)
    {        
        if (!PiesAltura.GetForcedCrouch() && CurrentState != MainState.Action)
        {
            base.OnCrouch(ctx);
            IsCrouch = !IsCrouch;

            switch (CurrentState)
            {
                case MainState.Idle:
                    CurrentState = MainState.Crouch;
                    break;
                case MainState.Running://efecto de inercia al agacharse corriendo?
                case MainState.Walking:
                    CurrentState = MainState.CrouchWalking;
                    break;
                case MainState.Crouch:
                    CurrentState = MainState.Idle;
                    break;
                case MainState.CrouchWalking:
                    CurrentState = MainState.Walking;
                    break;
                case MainState.Tilt:
                    CurrentState = MainState.CrouchTilt;
                    break;
                case MainState.CrouchTilt:
                    CurrentState = MainState.Tilt;
                    break;

            }

        }
        else
        {
            PiesAltura.EjecutaComprobacionAltura();
        }

    }

    //Eventos por Hold
    protected override void OnTilts(InputAction.CallbackContext ctx)
    {
        base.OnTilts(ctx);

        switch (CurrentState)
        {
            case MainState.Idle:
                CurrentState = MainState.Tilt;
                break;
            case MainState.Walking:
                CurrentState = MainState.TiltWalking;
                break;
            case MainState.Running:
                CurrentState = MainState.TiltRunning;
                break;
            case MainState.Crouch:
                CurrentState = MainState.CrouchTilt;
                break;

        }

    }

    protected override void FinishTilts(InputAction.CallbackContext ctx)
    {
        base.FinishTilts(ctx);

        switch (CurrentState)
        {
            case MainState.Tilt:
                CurrentState = MainState.Idle;
                break;
            case MainState.TiltWalking:
                CurrentState = MainState.Walking;
                break;
            case MainState.TiltRunning:
                CurrentState = MainState.Running;
                break;
            case MainState.CrouchTilt:
                CurrentState = MainState.Crouch;
                break;

        }

    }

    protected override void OnRun(InputAction.CallbackContext ctx)
    {
        base.OnRun(ctx);
        IsRunning = true;

        switch (CurrentState)
        {
            case MainState.Walking:
                CurrentState = MainState.Running;
                break;
            case MainState.Tilt:
                CurrentState = MainState.TiltRunning;
                break;
            case MainState.TiltWalking:
                CurrentState = MainState.TiltRunning;
                break;

        }

    }

    protected override void FinishRun(InputAction.CallbackContext ctx)
    {
        base.FinishRun(ctx);
        IsRunning = false;

        switch (CurrentState)
        {
            case MainState.Running:
                CurrentState = MainState.Walking;
                break;
            case MainState.TiltRunning:
                CurrentState = MainState.TiltWalking;
                break;

        }


    }


    protected override void OnWalking(InputAction.CallbackContext ctx)
    {
        base.OnWalking(ctx);

        switch (CurrentState)
        {
            case MainState.Idle:
                CurrentState = MainState.Walking;
                break;
            case MainState.Crouch:
                CurrentState = MainState.CrouchWalking;
                break;
            case MainState.Tilt:
                CurrentState = MainState.TiltWalking;
                break;
            case MainState.Action:
                if(CurrentSubState == SubState.None)
                {
                    CurrentState = MainState.Walking;
                }
                break;
        }

        //camina agachado y inclinado outlast? CrouchTilt
    }

    protected override void FinishWalking(InputAction.CallbackContext ctx)
    {
        base.FinishWalking(ctx);

        switch (CurrentState)
        {
            case MainState.Walking:
                CurrentState = MainState.Idle;
                break;
            case MainState.CrouchWalking:
                CurrentState = MainState.Crouch;
                break;
            case MainState.TiltWalking:
                CurrentState = MainState.Tilt;
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
                GetSpecialHidden = rayItem.SpecialHidden(); //Obtengo la interfaz del mueble especial, asi puedo establecer el punto de salida
            }

        }

        if (CurrentState == MainState.Action)//aplicar animacion
        {
            GetCurrentMueble.Ejecuted();//Si estoy escondido, se Abre mueble
            return;
        }

    }

}
