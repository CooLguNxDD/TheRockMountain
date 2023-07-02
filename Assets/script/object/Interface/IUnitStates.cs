using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitStates
{
    public enum UnitType{
        Player,
        Enemy
    }

    public float getHp();
    public void setHp(float newHp);
    public IUnitStates.UnitType GetUnitType();
    public void playerHurtedVisual();

    
}
