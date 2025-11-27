using System.Net;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalking : CharacterHuman
{
    [SerializeField] Transform HolderTransform;

    [SerializeField] private float speedWalking = 5f, speedRunning = 10f;
    [SerializeField] private float gravity = 1f;
    private float speed = 5f, verticalVelocity = 0f;
    private ControllerCamera MyCamera;

    [SerializeField] private Vector3 PreHidden = Vector3.zero;

    protected override void Awake()
    {
        base.Awake();
        if(HolderTransform.GetComponent<ControllerCamera>() == null)
        {
            Debug.LogError("Este GO No Posee el ControllerCamera");
            return;
        }

        MyCamera = HolderTransform.GetComponentInChildren<ControllerCamera>();
    }


    protected override void Update()
    {
        base.Update();
        speed = IsRunning ? speedRunning : speedWalking;

        CheckStatus();

        Vector2 move = inputs.Player.Move.ReadValue<Vector2>();
        PlayerMove(new Vector3(move.x, 0, move.y), gravity);
    }

    //Metodo para mover al personaje, y tambien, aplico la gravedad
    void PlayerMove(Vector3 dir, float gravityMultiplier)
    {
        Vector3 horizontal = OrientacionPlayer(dir) * speed;

        if (_cc.isGrounded)
        {
            if (verticalVelocity < 0f)
                verticalVelocity = -2f;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
        }

        Vector3 final = new Vector3(horizontal.x, verticalVelocity, horizontal.z);

        _cc.Move(final * Time.deltaTime);
    }

    //Camina segun el estado del Personaje, hacia el Forward de la camara o del Personaje 
    Vector3 OrientacionPlayer(Vector3 dir)
    {
        Vector3 camForward = HolderTransform.forward;
        Vector3 camRight = HolderTransform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        return camForward * dir.z + camRight * dir.x;
    }

    //Envio la data quien la necesite: -> TiltDireccion Class
    public Vector2 GetTiltDireccion()
    {
        return TiltOrientacion;
    }

    private void CheckStatus()
    {
        Vector2 look = inputs.Player.Look.ReadValue<Vector2>();
        switch (CurrentState)
        {
            case State.OutHidden:
                {
                    _cc.transform.position = PreHidden;
                    goto case State.Idle;
                }
            case State.Idle:
            case State.Walking:
            case State.Running:
            case State.Crouch:
            case State.CrouchWalking:
                MyCamera.NonTilt(look, this.transform);

                break;
            case State.TiltWalking:
                MyCamera.TiltWalking(look, this.transform, GetTiltDireccion());

                break;
            case State.PreHidden:////PreHidden, Hidden, OutHidden
                {
                    PreHidden = transform.position;
                    goto case State.Hidden;
                }
            case State.Hidden:
                {
                    Invoke("StartHidden", 0.2f);
                    goto case State.CrouchTilt;
                }
            case State.CrouchTilt:
            case State.Tilt:
                MyCamera.TiltCono(look, GetTiltDireccion());

                break;
            case State.TiltRunning:
                MyCamera.BackWardRun(GetTiltDireccion());

                break;
        }
    }

    private void LateUpdate()
    {
        if(CurrentState == State.Hidden && _cc.transform.position != rayItem.ModeHiddent()) {
            _cc.transform.position = rayItem.ModeHiddent();
        }
    }

}
