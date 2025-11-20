using UnityEngine;

public class CharacterFPS : CharacterPlayer
{
    [SerializeField] private float sensX = 100f;
    [SerializeField] private float sensY = 100f;

    private float pitch;

    protected override void Update()
    {
        base.Update();
        Turn(inputs.Player.Look.ReadValue<Vector2>());
    }

    protected override void Turn(Vector2 Action)
    {
        // Rotacion Horizontal del Character
        transform.Rotate(Vector3.up * Action.x * sensX * Time.deltaTime);

        // Rotación vertical de la Camara
        pitch -= Action.y * sensY * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -85f, 85f);

        camTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

}
