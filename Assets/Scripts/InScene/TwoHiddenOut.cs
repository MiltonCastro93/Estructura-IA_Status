using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class TwoHiddenOut : BaseEscondite, IAction,IModeHidden, ISpecialHidden
{

    protected Quaternion baseRotation;

    [SerializeField] protected float RotMax = 45f;
    float currentValue = 0f;

    protected override void Awake()
    {
        base.Awake();

        baseRotation = REFposthidden.localRotation;
    }

    public void Ejecuted()
    {
        throw new System.NotImplementedException();
    }

    public void Reverses()
    {
        throw new System.NotImplementedException();
    }


    public Vector3 PosOutHidden() => REFouthidden.position;
    public Vector3 PosHidden() => REFposthidden.position;
    public Quaternion PosHiddenRotation() => REFouthidden.rotation;



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
        REFposthidden.localRotation = baseRotation;
    }


}
