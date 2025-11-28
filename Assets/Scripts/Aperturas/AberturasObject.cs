using UnityEngine;
using UnityEngine.InputSystem;

public class AberturasObject : MonoBehaviour, Iinteraction
{
    private Animator _anim;
    [SerializeField] private bool IsOpen;

    private void Awake() {
        _anim = GetComponent<Animator>();
    }


    public void EjecutedInteraction()
    {
        IsOpen = !IsOpen;
        _anim.SetBool("Open", IsOpen);

    }





}
