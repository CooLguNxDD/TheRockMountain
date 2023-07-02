using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class rockController2D : NetworkBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D rb;

    private bool isGrounded;

    private bool isAttacking = false;

    
    public float movementSpeed = 5f;
    public float jumpForce = 10f;

    private float Hp;

    public float jumpRandomUpperTime;
    public float jumpRandomLowerTime;

    private float AttackTimer = 3f;
    public float jumpTimer;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpTimer = Random.Range(jumpRandomLowerTime, jumpRandomUpperTime);

        movementSpeed = GetComponent<UnitStates>().movementSpeed;
        jumpForce = GetComponent<UnitStates>().jumpForce;
        isAttacking = false;
        Hp = GetComponent<UnitStates>().Hp;
    }

    // Update is called once per frame
    void Update()
    {
        jumpTimer -= Time.deltaTime;
        if (isGrounded && jumpTimer < 0f)
            {
                jumpTimer = Random.Range(jumpRandomLowerTime, jumpRandomUpperTime);
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                isGrounded = false;
            }

        AttackTimer -= Time.deltaTime;

        if (isAttacking && AttackTimer < 0f)
            {
                AttackTimer = 3f;
                isAttacking = false;
            }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out ITerrain ground)){
            isGrounded = true;
        } 
        if(!isAttacking && collision.gameObject.TryGetComponent(out IUnitStates unitStates)){
            if(unitStates.GetUnitType() == IUnitStates.UnitType.Player){
                isAttacking = true;
                unitStates.setHp(unitStates.getHp() - 1);
                Debug.Log(unitStates.getHp());
            }
            collision.gameObject.GetComponent<Rigidbody2D>()
            .AddForce(new Vector2(2f, 2f), ForceMode2D.Impulse);
            

        } 
    }
}
