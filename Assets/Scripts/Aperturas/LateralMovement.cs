using UnityEngine;
using UnityEngine.InputSystem;

public class LateralMovement : MonoBehaviour, IAberturas
{
    private Animator _anim;
    [SerializeField] private bool IsOpen;

    private void Awake() {
        _anim = GetComponent<Animator>();
    }


    public void Interaccion()
    {
        IsOpen = !IsOpen;
        _anim.SetBool("Open", IsOpen);

    }





}
