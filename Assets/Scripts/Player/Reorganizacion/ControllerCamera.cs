using UnityEngine;
using UnityEngine.Windows;

public class ControllerCamera : MonoBehaviour
{
    public float sensX = 100f, sensY = 100f;
    private float pitchGlobalY = 0f, pitchX = 0f, pitchY = 0f;

    public void Turn(Vector2 Action, Transform Person) //Rotacion del personaje y la camara
    {
        // Rotacion Horizontal del Character
        Person.Rotate(Vector3.up * Action.x * sensX * Time.deltaTime);

        // Rotación vertical de la Camara
        pitchGlobalY -= Action.y * sensY * Time.deltaTime;
        pitchGlobalY = Mathf.Clamp(pitchGlobalY, -45f, 45f);

        transform.localRotation = Quaternion.Euler(pitchGlobalY, 0f, 0f);
    }

    public void TurnHolder(Vector2 Action) //Rotacion con limites de 45° en la camara cuando el personaje esta quieto
    {
        pitchY -= Action.y * sensY * Time.deltaTime;
        pitchY = Mathf.Clamp(pitchY, -45f, 45f);

        pitchX += Action.x * sensX * Time.deltaTime;
        pitchX = Mathf.Clamp(pitchX, -45f, 45f);

        // Rotación de la Camara
        transform.localRotation = Quaternion.Euler(pitchY, pitchX, 0f);

    }

    public void TiltDirecctionRun(Vector2 direccion) //Fija la camara cuando el personaje esta corriendo
    {
        transform.localRotation = Quaternion.Euler(0f, 140 * direccion.x, 0f);
    }

}
