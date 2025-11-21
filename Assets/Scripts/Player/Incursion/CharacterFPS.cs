using UnityEngine;
using UnityEngine.UIElements;

public class CharacterFPS : CharacterPlayer
{
    [SerializeField] private float sensX = 100f;
    [SerializeField] private float sensY = 100f;

    private float pitch;

    [SerializeField] HolderController holder;

    private void Start()
    {
        holder?.GetValues(inputs, sensX, sensY);

    }

    protected override void Update()
    {
        base.Update();

        if (Tilt(inputs.Player.Tilt.ReadValue<Vector2>())) {            
            Debug.Log("Tilting");

            if (inputs.Player.Move.ReadValue<Vector2>() != Vector2.zero) {
                isTilt = true;

                Debug.Log("Tilting and Moving");
            }
        }
        else
        {
            Turn(inputs.Player.Look.ReadValue<Vector2>());
            isTilt = false;
        }


    }

    protected bool Tilt(Vector2 value)
    {
        //activo/desactivo el componente que sujeta la camara
        if (value == Vector2.zero)
        {
            holder?.ResetHolder();
            return holder.enabled = false;
        }

        return holder.enabled = true;
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
