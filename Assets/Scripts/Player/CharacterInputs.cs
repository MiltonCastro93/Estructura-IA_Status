using UnityEngine;

public abstract class CharacterInputs : MonoBehaviour
{
    protected InputSystem_Actions inputs;

    private void Awake()
    {
        inputs = new InputSystem_Actions();
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }


    protected virtual void OnEnable()
    {
        inputs.Enable();
    }

    protected virtual void OnDisable()
    {
        inputs.Disable();
    }

}
