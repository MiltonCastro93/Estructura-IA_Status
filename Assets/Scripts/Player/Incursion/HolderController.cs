using UnityEngine;
using UnityEngine.InputSystem;

public class HolderController : MonoBehaviour
{
    InputSystem_Actions inputs;

    private float pitchY = 0f, pitchX = 0f, sensX = 0f, sensY = 0f;
    private CharacterFPS _characterFPS;

    private void Start() {
        _characterFPS = GetComponentInParent<CharacterFPS>();
    }

    public void GetValues(InputSystem_Actions Inputs, float SensX, float SensY)
    {
        inputs = Inputs;
        sensX = SensX;
        sensY = SensY;
    }

    public void ResetHolder()
    {
        pitchY = 0f;
        pitchX = 0f;
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(inputs != null) TurnHolder(inputs.Player.Look.ReadValue<Vector2>(), sensX, sensY);

    }

    void TurnHolder(Vector2 Action, float sensX, float sensY)
    {
        if (_characterFPS?.isRunningState() == false)
        {
            pitchY -= Action.y * sensY * Time.deltaTime;
            pitchY = Mathf.Clamp(pitchY, -45f, 45f);

            pitchX += Action.x * sensX * Time.deltaTime;
            pitchX = Mathf.Clamp(pitchX, -45f, 45f);


            // Rotación de la Camara
            transform.localRotation = Quaternion.Euler(pitchY, pitchX, 0f);
        }

    }

    public void TiltDirecctionRun(Vector2 direccion)
    {
        transform.localRotation = Quaternion.Euler(0f, 140 * direccion.x, 0f);
    }

}
