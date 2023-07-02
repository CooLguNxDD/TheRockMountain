using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour, ITerrain
{

    public ITerrain.GroundType groundType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ITerrain.GroundType getGroundType(){
        return groundType;
    }
}
