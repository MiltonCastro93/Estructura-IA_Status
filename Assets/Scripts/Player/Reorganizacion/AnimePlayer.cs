using UnityEngine;


[RequireComponent(typeof(Animator))]
public class AnimePlayer : PlayerWalking
{
    protected Animator _anim;

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
    public virtual void PreAccion(string TriggerType) //Mueble (InAccion()) -> Personaje Cambia entre estados segundo la accion actual -> Proxima
    {
        if (CurrentSubState == SubState.None)
        {
            CurrentState = MainState.Action;
            CurrentSubState = SubState.PreHidden;
        }

        if(CurrentSubState == SubState.Hidden)
        {
            CurrentState = MainState.Idle;
            CurrentSubState = SubState.OutHidden;
        }

        _anim.SetTrigger(TriggerType);
        _cc.enabled = false;
    }

    protected virtual void OnHiddenAnimation(string TriggerType) //Personaje -> Mueble "Cambia a modo hidden y reproducir la animacion del cierre del mueble"
    {
        CurrentSubState = SubState.Hidden;

        _anim.ResetTrigger(TriggerType);
        GetCurrentMueble.Reverses();

        transform.position = rayItem.ModeHiddent();
        transform.rotation = rayItem.RotModeHidden();

        _cc.enabled = true;
    }

    protected virtual void OutHiddenAnimation(string TriggerType) //Personaje ->Mueble "Cambia a modo idle y reproducir la animacion del Cierre del mueble"
    {
        CurrentState = MainState.Idle;
        CurrentSubState = SubState.None;

        _anim.ResetTrigger(TriggerType);
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
