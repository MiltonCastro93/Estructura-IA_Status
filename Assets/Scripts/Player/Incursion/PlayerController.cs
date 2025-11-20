using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : CharacterMove
{
    [SerializeField] private float sensX = 100f;
    [SerializeField] private float sensY = 100f;

    private float pitch;

    protected override void Update()
    {
        base.Update();
        Vector2 look = inputs.Player.Look.ReadValue<Vector2>();

        // Rotacion Horizontal del Character
        transform.Rotate(Vector3.up * look.x * sensX * Time.deltaTime);

        // Rotación vertical de la Camara
        pitch -= look.y * sensY * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -85f, 85f);

        camTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }


}
