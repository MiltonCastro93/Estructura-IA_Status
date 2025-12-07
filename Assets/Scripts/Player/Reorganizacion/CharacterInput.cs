using UnityEngine;
using UnityEngine.InputSystem;

public abstract class CharacterInput : MonoBehaviour
{
    protected InputSystem_Actions inputs;
    protected virtual void Awake()
    {
        inputs = new InputSystem_Actions();
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    //Aniado nuevos eventos aqui!
    protected virtual void OnEnable()
    {
        inputs.Enable();
        inputs.Player.Crouch.performed += OnCrouch;
        inputs.Player.Attack.performed += OnFire;

        inputs.Player.Sprint.performed += OnRun;
        inputs.Player.Sprint.canceled += FinishRun;

        inputs.Player.Move.performed += OnWalking;
        inputs.Player.Move.canceled += FinishWalking;

        inputs.Player.Tilt.performed += OnTilts;
        inputs.Player.Tilt.canceled += FinishTilts;
    }

    protected virtual void OnDisable()
    {
        inputs.Player.Crouch.performed -= OnCrouch;
        inputs.Player.Attack.performed -= OnFire;

        inputs.Player.Sprint.performed -= OnRun;
        inputs.Player.Sprint.canceled -= FinishRun;

        inputs.Player.Move.performed -= OnWalking;
        inputs.Player.Move.canceled -= FinishWalking;

        inputs.Player.Tilt.performed -= OnTilts;
        inputs.Player.Tilt.canceled -= FinishTilts;

        inputs.Disable();
    }

    //Se hacen las Primeras instancias de los eventos por teclados
    protected virtual void OnCrouch(InputAction.CallbackContext ctx){}//Agacharse
    protected virtual void OnFire(InputAction.CallbackContext ctx){}//Interactuar
    protected virtual void OnTilts(InputAction.CallbackContext ctx){}//Inclinarse
    protected virtual void FinishTilts(InputAction.CallbackContext ctx){}//Termina de Inclinarse
    protected virtual void OnRun(InputAction.CallbackContext ctx){}//Correr
    protected virtual void FinishRun(InputAction.CallbackContext ctx){}//Termina de Correr
    protected virtual void OnWalking(InputAction.CallbackContext ctx){}//Caminar
    protected virtual void FinishWalking(InputAction.CallbackContext ctx){}//Termina de Caminar

}
