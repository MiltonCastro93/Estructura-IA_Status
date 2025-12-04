using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class EsconditeFast : BaseEscondite, IModeHidden
{
    float currentValue = 0f;

    public Vector3 OutHidden() => REFouthidden.position;
    public Vector3 PosHidden() => REFposthidden.position;
    public Quaternion PosHiddenRotation() => REFouthidden.rotation;

    public void RotOutHidden(Vector2 valueX)//> sera llamado desde el player
    {
        // sumo el input horizontal
        currentValue += valueX.x;

        // clampo el offset en torno a la rotación base
        currentValue = Mathf.Clamp(currentValue, -RotMax, RotMax);

        // aplico la rotación base + offset
        REFposthidden.localRotation = baseRotation * Quaternion.Euler(0f, currentValue, 0f);
    }

    public void ResetRotation()
    {
        currentValue = 0;
        REFposthidden.localRotation = baseRotation;
    }


}
