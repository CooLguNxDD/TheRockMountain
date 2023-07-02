using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HeartAnimation : MonoBehaviour
{
    public UnitStates unitStates;
    // Start is called before the first frame update
    void Start()
    {
        unitStates.unitHurt += performHeartBrokenVisual;
    }

    public void performHeartBrokenVisual(object sender, EventArgs args){
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
