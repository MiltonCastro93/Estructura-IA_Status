using UnityEngine;
using UnityEngine.Windows;

public class ControllerCamera : MonoBehaviour
{
    public float sensX = 100f, sensY = 100f;
    private float pitchGlobalY = 0f, pitchX = 0f, pitchY = 0f, MaxTilt = 45f;

    private Vector3 CamCenter = Vector3.zero;

    private void Start()
    {
        CamCenter = transform.localPosition;
    }

    public void NonTilt(Vector2 Action, Transform Person) //Rotacion del personaje y la camara
    {
        // Rotacion Horizontal del Character
        Person.Rotate(Vector3.up * Action.x * sensX * Time.deltaTime);

        // Rotación vertical de la Camara
        pitchGlobalY -= Action.y * sensY * Time.deltaTime;
        pitchGlobalY = Mathf.Clamp(pitchGlobalY, -45f, 45f);

        if(transform.localPosition != CamCenter) {
            transform.localPosition = CamCenter;
        }

        transform.localRotation = Quaternion.Euler(pitchGlobalY, 0f, 0f);
    }

    public void TiltWalking(Vector2 Action, Transform Person, Vector2 DireccionZ)
    {
        Person.Rotate(Vector3.up * Action.x * sensX * Time.deltaTime);

        pitchGlobalY -= Action.y * sensY * Time.deltaTime;
        pitchGlobalY = Mathf.Clamp(pitchGlobalY, -45f, 45f);

        transform.localPosition = new Vector3(0.5f * DireccionZ.x, CamCenter.y, CamCenter.z);
        transform.localRotation = Quaternion.Euler(pitchGlobalY, 0f, MaxTilt * -DireccionZ.x);
    }

    public void TiltCono(Vector2 Action, Vector2 DireccionZ) //Rotacion con limites de 45° en la camara cuando el personaje esta quieto
    {
        pitchY -= Action.y * sensY * Time.deltaTime;
        pitchY = Mathf.Clamp(pitchY, -45f, 45f);

        pitchX += Action.x * sensX * Time.deltaTime;
        pitchX = Mathf.Clamp(pitchX, -45f, 45f);

        // Rotación de la Camara
        transform.localPosition = new Vector3(0.5f * DireccionZ.x, CamCenter.y, CamCenter.z); //<- le agrego inclinacion
        transform.localRotation = Quaternion.Euler(pitchY, pitchX, MaxTilt * -DireccionZ.x);

    }

    public void BackWardRun(Vector2 DireccionZ) //Fija la camara cuando el personaje esta corriendo
    {
        transform.localRotation = Quaternion.Euler(0f, 140 * DireccionZ.x, 0f);
    }

}
