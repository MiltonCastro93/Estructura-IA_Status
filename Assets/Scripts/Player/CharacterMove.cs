using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    InputSystem_Actions inputs;

    private Rigidbody rb;
    [SerializeField] private float speed = 5f;

    private void Awake() {
        inputs = new InputSystem_Actions();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void FixedUpdate() {
        Vector2 move = inputs.Player.Move.ReadValue<Vector2>();
        Vector3 getInputs = new Vector3(move.x, 0, move.y);

        Debug.Log($"Movimiento: {getInputs}");

        rb.MovePosition(transform.position + getInputs * speed * Time.fixedDeltaTime);
    }


    private void OnEnable() {
        inputs.Player.Enable();
    }

    private void OnDisable() {
        inputs.Player.Disable();
    }

}
