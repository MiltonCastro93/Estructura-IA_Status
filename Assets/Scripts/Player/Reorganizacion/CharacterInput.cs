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
        inputs.Player.Attack.performed += OnAttack;

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
        inputs.Player.Attack.performed -= OnAttack;

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

    protected virtual void OnAttack(InputAction.CallbackContext ctx)
    {
        Debug.Log("Interactuar");
        //RaycastHit hit;

        //if (Physics.Raycast(transform.position, transform.forward, out hit, 3f))
        //{
        //    Debug.Log($"Choco contra {hit.collider.transform.gameObject.name}");
        //    Debug.DrawLine(transform.position, hit.point, Color.red, 1f);

        //    if (RelativeMueble != null && hit.collider.CompareTag("Mueble"))
        //    {
        //        // ENTRAR
        //        if (!_characterEspia.GetStatus())
        //        {
        //            // Guardar posición REAL del personaje
        //            OldPosition = transform.parent.position;

        //            _characterEspia.SetStatus(true);

        //            // Mover al punto de ejecución del mueble
        //            transform.parent.position = RelativeMueble.EjecutedPos();
        //        }
        //        // SALIR
        //        else
        //        {
        //            // Volver a la posición anterior
        //            transform.parent.position = OldPosition;
        //            _characterEspia.SetStatus(false);

        //        }
        //    }
        //}

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
