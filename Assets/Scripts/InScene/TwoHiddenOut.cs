using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class TwoHiddenOut : OneHiddenOut, ISpecialHidden
{
    float currentValue = 0f;
    [SerializeField] private Transform REFLeftOuthidden;

    [SerializeField] protected float RotMax = 45f;
    protected Quaternion baseRotation;


    protected override void Awake()
    {
        base.Awake();

        baseRotation = RefPostHidden.localRotation;
    }

    //otra interface, para que me permita rotar la camara mientras estoy escondido en un angulo maximo
    public void RotOutHidden(Vector2 valueX)//> sera llamado desde el player
    {
        // sumo el input horizontal
        currentValue += valueX.x;

        // clampo el offset en torno a la rotación base
        currentValue = Mathf.Clamp(currentValue, -RotMax, RotMax);

        // aplico la rotación base + offset
        //REFposthidden.localRotation = baseRotation * Quaternion.Euler(0f, currentValue, 0f);
    }

    public void ResetRotation()
    {
        currentValue = 0;
        RefPostHidden.localRotation = baseRotation;
    }

    public Vector3 CallOutHidden()
    {
        if (currentValue > 0f)
        {
            Debug.Log("Izquierda");
            return REFLeftOuthidden.position;
        }
        Debug.Log("Derecha");
        return RefOutHidden.position;
    }
}
