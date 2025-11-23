using System.Collections;
using UnityEngine;

public class HolderCamera : MonoBehaviour
{
    public float sensX = 100f, sensY = 100f;

    InputSystem_Actions inputs;
    private float pitchGlobalY = 0f, pitchX = 0f, pitchY = 0f;
    private CharacterEspia _characterEspia;
    private bool allowFreeLook = true;
    
    private Coroutine resetRotCoroutine;//Referencia a la corrutina de reseteo de rotacion


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _characterEspia = GetComponentInParent<CharacterEspia>();
        inputs = _characterEspia.GetInputs();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 look = inputs.Player.Look.ReadValue<Vector2>();
        Vector2 tilt = inputs.Player.Tilt.ReadValue<Vector2>();


        if (_characterEspia.IsTilt())
        {

            if (_characterEspia.IsRunning())
            {
                allowFreeLook = true;
                TiltDirecctionRun(tilt); //Fija la camara cuando el personaje esta corriendo
                return;
            }
            allowFreeLook = false;
            TurnHolder(look); //Rotacion con limites de 45° en la camara cuando el personaje esta quieto
        }
        else
        {
            allowFreeLook = true;
            ResetControllerROT();//hacer un lerp entre la rotacion actual
        }

        if (_characterEspia.IsWalkingForward())
        {
            allowFreeLook = true;

        }

        if (allowFreeLook) { 
            Turn(look); //Rotacion del personaje y la camara

        }

    }


    void Turn(Vector2 Action) //Rotacion del personaje y la camara
    {
        // Rotacion Horizontal del Character
        transform.parent.Rotate(Vector3.up * Action.x * sensX * Time.deltaTime);

        // Rotación vertical de la Camara
        pitchGlobalY -= Action.y * sensY * Time.deltaTime;
        pitchGlobalY = Mathf.Clamp(pitchGlobalY, -45f, 45f);

        transform.localRotation = Quaternion.Euler(pitchGlobalY, 0f, 0f);
    }

    void TurnHolder(Vector2 Action) //Rotacion con limites de 45° en la camara cuando el personaje esta quieto
    {
        pitchY -= Action.y * sensY * Time.deltaTime;
        pitchY = Mathf.Clamp(pitchY, -45f, 45f);

        pitchX += Action.x * sensX * Time.deltaTime;
        pitchX = Mathf.Clamp(pitchX, -45f, 45f);

        // Rotación de la Camara
        transform.localRotation = Quaternion.Euler(pitchY, pitchX, 0f);

    }

    void TiltDirecctionRun(Vector2 direccion) //Fija la camara cuando el personaje esta corriendo
    {
        transform.localRotation = Quaternion.Euler(0f, 140 * direccion.x, 0f);
    }

    void ResetControllerROT()
    {

        if (resetRotCoroutine != null)
            StopCoroutine(resetRotCoroutine);

        resetRotCoroutine = StartCoroutine(ResetRotRoutine());

    }


    IEnumerator ResetRotRoutine()
    {
        Quaternion startRot = transform.localRotation;
        Quaternion targetRot = Quaternion.identity;

        float t = 0f;
        float duration = 0.15f; // se ajusta a gusto

        while (t < duration)
        {
            t += Time.deltaTime;
            float k = t / duration;

            // Suavizado (evita mareos)
            k = Mathf.SmoothStep(0f, 1f, k);

            transform.localRotation = Quaternion.Lerp(startRot, targetRot, k);
            yield return null;
        }

        transform.localRotation = targetRot;
        pitchX = 0f;
        pitchY = 0f;
        pitchGlobalY = 0f;
    }


}
