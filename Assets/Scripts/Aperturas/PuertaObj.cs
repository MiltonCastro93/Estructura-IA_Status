using UnityEngine;

public class PuertaObj : MonoBehaviour, IAction
{
    private Animator _anim;
    [SerializeField] private bool IsOpen;

    private void Awake() {
        _anim = GetComponent<Animator>();
    }


    public void Ejecuted()
    {
        IsOpen = !IsOpen;
        _anim.SetBool("Open", IsOpen);

    }

    public void Reverses()//posiblemente no se use
    {

    }
}
