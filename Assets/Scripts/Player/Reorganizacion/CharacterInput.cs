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
        inputs.Player.Crouch.performed += OnCrouchPerformed;
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
        inputs.Player.Crouch.performed -= OnCrouchPerformed;
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
    protected virtual void OnCrouchPerformed(InputAction.CallbackContext ctx)
    {
        Debug.Log("Agachado");
    }

    protected virtual void OnFire(InputAction.CallbackContext ctx)
    {
        Debug.Log("Interactuar");

    }

    protected virtual void OnTilts(InputAction.CallbackContext ctx)
    {
        Debug.Log("Start Inclinacion");
    }
    protected virtual void FinishTilts(InputAction.CallbackContext ctx)
    {
        Debug.Log("Dejo de Inclinar");
    }

    protected virtual void OnRun(InputAction.CallbackContext ctx)
    {
        Debug.Log("Corriendo");
    }

    protected virtual void FinishRun(InputAction.CallbackContext ctx)
    {
        Debug.Log("Dejo de Correr");
    }


    protected virtual void OnWalking(InputAction.CallbackContext ctx)
    {
        Debug.Log("Empezo a Caminar");
    }

    protected virtual void FinishWalking(InputAction.CallbackContext ctx)
    {
        Debug.Log("Dejo de Caminar");
    }

}
