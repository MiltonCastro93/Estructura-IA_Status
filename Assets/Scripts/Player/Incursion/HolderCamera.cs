using UnityEngine;

public class HolderCamera : MonoBehaviour
{
    public float sensX = 100f, sensY = 100f;

    InputSystem_Actions inputs;
    private float pitchY = 0f, pitchX = 0f, TurnHolderY = 0f;
    private CharacterEspia _characterEspia;
    public bool turing = true;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _characterEspia = GetComponentInParent<CharacterEspia>();
        inputs = _characterEspia.GetInputs();
    }

    // Update is called once per frame
    void Update()
    {
        if (_characterEspia.IsTilt())
        {

            if (_characterEspia.IsRunning())
            {
                turing = true;
                TiltDirecctionRun(inputs.Player.Tilt.ReadValue<Vector2>()); //Fija la camara cuando el personaje esta corriendo
                ResetControllerROT();
                return;
            }
            turing = false;
            TurnHolder(inputs.Player.Look.ReadValue<Vector2>()); //Rotacion con limites de 45° en la camara cuando el personaje esta quieto
        }
        else
        {
            turing = true;
            ResetControllerROT();
        }

        if (turing) { 
            Turn(inputs.Player.Look.ReadValue<Vector2>()); //Rotacion del personaje y la camara

        }

    }


    void Turn(Vector2 Action) //Rotacion del personaje y la camara
    {
        // Rotacion Horizontal del Character
        transform.parent.Rotate(Vector3.up * Action.x * sensX * Time.deltaTime);

        // Rotación vertical de la Camara
        pitchY -= Action.y * sensY * Time.deltaTime;
        pitchY = Mathf.Clamp(pitchY, -85f, 85f);

        transform.localRotation = Quaternion.Euler(pitchY, 0f, 0f); //> BUG: al rotar el personaje, la camara se voltea
    }

    void TurnHolder(Vector2 Action) //Rotacion con limites de 45° en la camara cuando el personaje esta quieto
    {
        TurnHolderY -= Action.y * sensY * Time.deltaTime;
        TurnHolderY = Mathf.Clamp(TurnHolderY, -45f, 45f);

        pitchX += Action.x * sensX * Time.deltaTime;
        pitchX = Mathf.Clamp(pitchX, -45f, 45f);

        // Rotación de la Camara
        transform.localRotation = Quaternion.Euler(TurnHolderY, pitchX, 0f);

    }

    void TiltDirecctionRun(Vector2 direccion) //Fija la camara cuando el personaje esta corriendo
    {
        transform.localRotation = Quaternion.Euler(0f, 140 * direccion.x, 0f);
    }

    void ResetControllerROT()
    {
        pitchX = 0f;
        TurnHolderY = 0f;
    }

}
