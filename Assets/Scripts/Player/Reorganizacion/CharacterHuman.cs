using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
public abstract class CharacterHuman : CharacterInput
{
    protected CharacterController _cc;
    protected enum State { Idle, Walking, Running, Crouch, CrouchWalking, Hidden, Tilt, TiltWalking, TiltRunning }
    [SerializeField] protected State CurrentState;

    [SerializeField] private float speedWalking = 5f, speedRunning = 10f;
    [SerializeField] protected float gravity = 1f;
    protected float speed = 0f, verticalVelocity = 0f;
    protected Vector3 OldPosition = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake()
    {
        base.Awake();
        _cc = GetComponent<CharacterController>();
    }

    //Regula los estados del Personaje
    protected virtual void Update()
    {
        if(_cc.velocity.sqrMagnitude != 0f && CurrentState != State.Crouch)
        {
            if(_cc.velocity.sqrMagnitude >= 60f)
            {

                CurrentState = State.Running;
                return;
            }

            CurrentState = State.Walking;
            return;
        }

        if(_cc.velocity.sqrMagnitude != 0f && CurrentState == State.Crouch)
        {
            CurrentState = State.CrouchWalking;
            return;
        }


        CurrentState = State.Idle;
    }

    //Modifico el metodo de agacharse
    protected override void OnCrouchPerformed(InputAction.CallbackContext ctx)
    {
        base.OnCrouchPerformed(ctx);
        CurrentState = State.Crouch;

    }

}
