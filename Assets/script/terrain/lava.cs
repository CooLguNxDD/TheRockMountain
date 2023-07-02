using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lava : MonoBehaviour, ITerrain
{
    private bool isAttacking = false;
    private float AttackTimer = 1.5f;

    public ITerrain.GroundType groundType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        AttackTimer -= Time.deltaTime;

        if (isAttacking && AttackTimer < 0f)
            {
                AttackTimer = 1.5f;
                isAttacking = false;
            }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(!isAttacking && collision.gameObject.TryGetComponent(out IUnitStates unitStates)){
            if(unitStates.GetUnitType() == IUnitStates.UnitType.Player){
                isAttacking = true;
                unitStates.setHp(unitStates.getHp() - 1);
                
                Debug.Log(unitStates.getHp());
            }
            collision.gameObject.GetComponent<Rigidbody2D>()
            .AddForce(new Vector2(0f, 10f), ForceMode2D.Impulse);
        } 
    }
    public ITerrain.GroundType getGroundType(){
        return groundType;
    }

    
}
