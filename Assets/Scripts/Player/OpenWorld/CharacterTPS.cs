using UnityEngine;

public class CharacterTPS : CharacterPlayer
{

    protected override void Update()
    {
        base.Update();
        Turn(inputs.Player.Move.ReadValue<Vector2>());
    }

    protected override void Turn(Vector2 Action)
    {
        Vector3 camForward = HolderTransform.forward;
        Vector3 camRight = HolderTransform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 getTurn = camForward * Action.y + camRight * Action.x;
        if (getTurn != Vector3.zero) {
            transform.rotation = Quaternion.LookRotation(getTurn);
        }

    }

}
