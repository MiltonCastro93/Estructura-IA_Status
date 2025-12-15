using UnityEngine;

public class OneDinamicHidden : OneHiddenOut, ISpecialHidden
{
    float currentValue = 0f;
    [SerializeField] protected float RotMax = 45f;
    protected Quaternion baseRotation;

    protected override void Awake()
    {
        base.Awake();

        baseRotation = RefPostHidden.localRotation;
    }

    public Vector3 CallOutHidden() => RefOutHidden.position;

    public void ResetRotation()
    {
        currentValue = 0;
        RefPostHidden.localRotation = baseRotation;
    }

    public void RotOutHidden(Vector2 valueX)
    {
        // sumo el input horizontal
        currentValue += valueX.x;

        // clampo el offset en torno a la rotación base
        currentValue = Mathf.Clamp(currentValue, -RotMax, RotMax);

        //aplico la rotación base + offset
        RefPostHidden.localRotation = baseRotation * Quaternion.Euler(0f, currentValue, 0f);
    }

}
