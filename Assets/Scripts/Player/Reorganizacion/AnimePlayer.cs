using System;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class AnimePlayer : PlayerWalking
{
    protected Animator _anim;
    [SerializeField] int typeHidden = 0; // Tipo de escondite (None = -1, Bajo = 0, Medio = 1, Alto = 2)

    protected override void Awake()
    {
        base.Awake();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

    }


    //Animar y desacoplar acciones
    public virtual void PreAccion(Enum type) //Mueble (InAccion()) -> Personaje Cambia entre estados segundo la accion actual -> Proxima
    {
        typeHidden = Convert.ToInt32(type);

        if (CurrentSubState == SubState.None) //Entrar en escondite
        {
            CurrentState = MainState.Action;
            CurrentSubState = SubState.PreHidden;

            _anim.SetInteger("HiddenType", typeHidden);
            _anim.SetTrigger("StartHidden");
            _anim.SetBool("IsHidden", true);
        }

        if (CurrentSubState == SubState.Hidden) //Salir del escondite
        {
            CurrentState = MainState.Idle;
            CurrentSubState = SubState.OutHidden;

            _anim.SetTrigger("FinishHidden");
            _anim.SetBool("IsHidden", false);
        }

        _cc.enabled = false;
    }

    protected virtual void OnHiddenAnimation() //Personaje -> Mueble "Cambia a modo hidden y reproducir la animacion del cierre del mueble"
    {
        CurrentSubState = SubState.Hidden;

        _anim.ResetTrigger("StartHidden");

        GetCurrentMueble.Reverses();

        transform.position = rayItem.ModeHiddent();
        transform.rotation = rayItem.RotModeHidden();

        _cc.enabled = true;
    }

    protected virtual void OutHiddenAnimation() //Personaje ->Mueble "Cambia a modo idle y reproducir la animacion del Cierre del mueble"
    {
        CurrentState = MainState.Idle;
        CurrentSubState = SubState.None;

        _anim.ResetTrigger("FinishHidden");
        _anim.SetInteger("HiddenType", -1);
        GetCurrentMueble.Reverses();

        transform.position = rayItem.ModoOutHidden(); //posicion de salida del escondite

        _cc.enabled = true;
    }

    private void OnAnimatorMove()
    {
        if (!_cc.enabled)
        {
            transform.position += _anim.deltaPosition;
            transform.rotation *= _anim.deltaRotation;
        }

    }
}
