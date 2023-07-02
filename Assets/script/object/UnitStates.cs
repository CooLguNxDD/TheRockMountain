using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitStates : MonoBehaviour, IUnitStates
{
    // Start is called before the first frame update

    public IUnitStates.UnitType unitType;
    public float Hp;

    public float movementSpeed;
    public float jumpForce;

    public event EventHandler unitHurt;

    public event EventHandler loseEventHandler;
    public event EventHandler WinEventHandler;

    void Update(){
        if(Hp <= 0 && unitType != IUnitStates.UnitType.Player){
            gameObject.SetActive(false);
        }
    }

    public IUnitStates.UnitType GetUnitType(){
        return this.unitType;
    }

    public void playerHurtedVisual(){
        unitHurt?.Invoke(this, EventArgs.Empty);
    }

    public void startLoseEvent(){
        GameManager.Instance.PlayerHeight = transform.position.y;
        loseEventHandler?.Invoke(this, EventArgs.Empty);
        Destroy(this.gameObject);
    }

    public void startWinEvent(){
        GameManager.Instance.PlayerHeight = transform.position.y;
        WinEventHandler?.Invoke(this, EventArgs.Empty);
        Destroy(this.gameObject);
    }

    public float getHp()
    {
        return Hp;
    }

    public void setHp(float newHp)
    {
        playerHurtedVisual();
        this.Hp = newHp;
    }
}
