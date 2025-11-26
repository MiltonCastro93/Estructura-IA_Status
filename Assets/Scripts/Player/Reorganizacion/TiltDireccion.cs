using UnityEngine;

//Clase provisoria para chequear la inclinacion
public class TiltDireccion : MonoBehaviour
{
    [SerializeField] float MaxTilt = 45f;
    PlayerWalking playerWalking;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerWalking = GetComponentInParent<PlayerWalking>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, MaxTilt * -playerWalking.GetTiltDireccion().x);

    }
}
