using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    protected InputSystem_Actions inputs;
    private CharacterController _cc;

    [SerializeField] protected Transform camTransform;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = 1f;
    private float verticalVelocity = 0f;

    private void Awake() {
        inputs = new InputSystem_Actions();
        _cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    protected virtual void Update() 
    {
        Vector2 move = inputs.Player.Move.ReadValue<Vector2>();

        PlayerMove(new Vector3(move.x, 0, move.y), gravity);

    }

    void PlayerMove(Vector3 dir, float gravityMultiplier)
    {
        Vector3 horizontal = OrientacionPlayer(dir) * speed;

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

    Vector3 OrientacionPlayer(Vector3 dir)
    {
        Vector3 camForward = camTransform.forward;
        Vector3 camRight = camTransform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        return camForward * dir.z + camRight * dir.x;
    }

    private void OnEnable() {
        inputs.Player.Enable();
    }

    private void OnDisable() {
        inputs.Player.Disable();
    }

}
