using System.Net;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerWalking : CharacterHuman
{
    [SerializeField] Transform HolderTransform;
    private ControllerCamera MyCamera;

    [SerializeField] private float speedWalking = 5f, speedRunning = 10f;
    [SerializeField] private float gravity = 1f;
    private float speed = 5f, verticalVelocity = 0f;
    protected Vector2 TiltOrientacion = Vector2.zero;

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
        CheckStatus();

        speed = IsRunning ? speedRunning : speedWalking;

        if (CurrentState == State.Hidden)
        {
            return;
        }

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
    Vector3 OrientacionPlayer(Vector3 dir)//usar un bool para cambiar la direccion forward
    {
        Vector3 camForward = IsRunning ? _cc.transform.forward : HolderTransform.forward;
        Vector3 camRight = IsRunning ? _cc.transform.right : HolderTransform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        return camForward * dir.z + camRight * dir.x;
    }

    private void CheckStatus()
    {
        Vector2 look = inputs.Player.Look.ReadValue<Vector2>();
        switch (CurrentState)
        {
            case State.OutHidden:
                {
                    if (CurrentMueble != null)
                    {
                        transform.position = OldPosition;
                        CurrentMueble.ResetRotation();
                        CurrentMueble = null;
                    }

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
                MyCamera.TiltWalking(look, this.transform, TiltOrientacion);

                break;
            case State.PreHidden:
                {
                    IsCrouch = false;
                    transform.localScale = Vector3.one; //reestablesco la scale del GO player. posiblemente lo saque
                    goto case State.Hidden;
                }
            case State.Hidden:
                {
                    if(CurrentMueble != null)
                    {
                        CurrentMueble.RotOutHidden(look); //roto al pivote para tener un lugar para salir del escondite
                        OldPosition = CurrentMueble.OutHidden(); //mientras estoy escondido, actualizo el vector3 de salida
                    }

                    goto case State.CrouchTilt;
                }
            case State.CrouchTilt:
            case State.Tilt:
                MyCamera.TiltCono(look, TiltOrientacion);

                break;
            case State.TiltRunning:
                MyCamera.BackWardRun(TiltOrientacion);

                break;
        }
    }

    protected override void OnTilts(InputAction.CallbackContext ctx)
    {
        base.OnTilts(ctx);
        TiltOrientacion = inputs.Player.Tilt.ReadValue<Vector2>();
    }

    protected override void FinishTilts(InputAction.CallbackContext ctx)
    {
        base.FinishTilts(ctx);
        TiltOrientacion = Vector2.zero;
    }

    protected override void OnCrouchPerformed(InputAction.CallbackContext ctx)
    {
        base.OnCrouchPerformed(ctx);
        transform.localScale = IsCrouch ? new Vector3(0.2f, 0.2f, 0.2f) : Vector3.one; //reproducira una animacion para que se agache
    }

}
