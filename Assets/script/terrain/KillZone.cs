using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killZone : MonoBehaviour, ITerrain
{
    private bool isAttacking = false;
    private float AttackTimer = 3f;

    public ITerrain.GroundType groundType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out IUnitStates unitStates)){
            
            unitStates.setHp(unitStates.getHp() - 999);
            Debug.Log(unitStates.getHp());
            
            collision.gameObject.GetComponent<Rigidbody2D>()
            .AddForce(new Vector2(2f, 2f), ForceMode2D.Impulse);
        } 
    }

    public ITerrain.GroundType getGroundType(){
        return groundType;
    }

    
}
