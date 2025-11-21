using UnityEngine;
using UnityEngine.InputSystem;

public abstract class CharacterPlayer : MonoBehaviour
{
    protected InputSystem_Actions inputs;
    private CharacterController _cc;

    [SerializeField] protected Transform camTransform;

    [SerializeField] private float speedWalking = 5f, speedRunning = 10f;
    [SerializeField] private float gravity = 1f;
    private float speed = 0f, verticalVelocity = 0f;

    private bool isRunning = false, isCrouching = false;
    public bool isTilt = false;

    private void Awake() {
        inputs = new InputSystem_Actions();
        _cc = GetComponent<CharacterController>();
        isRunning = false;
        isCrouching = false;

    }

    // Update is called once per frame
    protected virtual void Update() 
    {
        Vector2 move = inputs.Player.Move.ReadValue<Vector2>();
        isRunning = inputs.Player.Sprint.IsPressed();

        if(isRunning && !isCrouching)
        {
            speed = speedRunning;
        }
        else
        {
            speed = speedWalking;
        }

        PlayerMove(new Vector3(move.x, 0, move.y), gravity);
    }

    void PlayerMove(Vector3 dir, float gravityMultiplier)//Movimiento del player y Aplicar gravedad
    {
        Vector3 horizontal = OrientacionPlayer(dir, isTilt) * speed;

        if (_cc.isGrounded)
        {
            if (verticalVelocity < 0f)
                verticalVelocity = -2f;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
        }

        Vector3 final = new Vector3(horizontal.x, verticalVelocity, horizontal.z);

        _cc.Move(final * Time.deltaTime);
    }

    Vector3 OrientacionPlayer(Vector3 dir, bool IsTilt)//Camina en direccion de la camara
    {
        Vector3 camForward = Vector3.zero;
        Vector3 camRight = Vector3.zero;

        if (IsTilt)
        {
            camForward = _cc.transform.forward;
            camRight = _cc.transform.right;
        }
        else
        {
            camForward = camTransform.forward;
            camRight = camTransform.right;
        }

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        return camForward * dir.z + camRight * dir.x;
    }

    abstract protected void Turn(Vector2 Action);//segun el modo de juego, se implementa diferente su rotacion


    private void OnEnable()
    {
        inputs.Enable();
        inputs.Player.Crouch.performed += OnCrouchPerformed;
    }

    private void OnDisable()
    {
        inputs.Player.Crouch.performed -= OnCrouchPerformed;
        inputs.Disable();
    }

    private void OnCrouchPerformed(InputAction.CallbackContext ctx)
    {
        isCrouching = !isCrouching;
    }

}
