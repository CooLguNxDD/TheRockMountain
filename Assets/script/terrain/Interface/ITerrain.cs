using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITerrain{

    public enum GroundType{
        Ice,
        Normal,
        win,
    }

    public GroundType getGroundType();
}

