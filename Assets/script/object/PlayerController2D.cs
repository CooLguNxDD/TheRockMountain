using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class PlayerController2D : NetworkBehaviour
{
    private float movementSpeed = 5f;
    private float jumpForce = 7f;
    private float HP;

    private Rigidbody2D rb;
    private bool isGrounded;

    private bool isSlippyGround;

    private bool isWalking;

    public float SlippingVector;
    private bool isSlipping;

    private bool doubleJumpAble = false;
    private bool doubleJumped = false;

    private SpriteRenderer renderer;

    public Animator animator;

    public event EventHandler loseEventHandler;
    public event EventHandler WinEventHandler;

    public TMP_Text playerTag;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movementSpeed = GetComponent<UnitStates>().movementSpeed;
        jumpForce = GetComponent<UnitStates>().jumpForce;
        HP = GetComponent<UnitStates>().Hp;
        renderer = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        if(IsOwner){
           SetNameServerRpc (gameObject, GameManager.Instance.playerName);
        }
        
        if(!IsOwner){
            return;
        }

        if(gameObject.GetComponent<UnitStates>().Hp <= 0){
            gameObject.GetComponent<UnitStates>().startLoseEvent();
            gameObject.SetActive(false);
        }
        
        HandleMovement();

        
    }
    [ServerRpc(RequireOwnership = false)]
    private void SetNameServerRpc(NetworkObjectReference player, string NewName){
        // playerTag.text = GameManager.Instance.playerName;
        SetNameClientRpc(gameObject, NewName);
    }
    [ClientRpc]
    private void SetNameClientRpc(NetworkObjectReference player, string NewName)
    {
        playerTag.text = NewName;
    }
    void HandleMovement(){
        
        if(Math.Abs(Input.GetAxisRaw("Horizontal")) > 0){
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * movementSpeed , rb.velocity.y);
            animator.SetBool("isWalking", true);

            SlippingVector = Input.GetAxis("Horizontal");
            isWalking = true;
        }
        else if(isWalking){
            isWalking = false;
            animator.SetBool("isWalking", false);
            isSlipping = true;
        }

        if(Input.GetAxisRaw("Horizontal") > 0){
             renderer.flipX = true;
        }
        if(Input.GetAxisRaw("Horizontal") < 0){
             renderer.flipX = false;
        }

        if(isSlippyGround && !isWalking && isSlipping && isGrounded){
            rb.AddForce(new Vector2(SlippingVector * 2.5f, 0f), ForceMode2D.Impulse);
            Debug.Log("slipping");
            isWalking = false;
            isSlipping = false;
        }
        // Jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Debug.Log("jumping");
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
            
        }
        if(!isGrounded && !doubleJumpAble && Input.GetButtonDown("Jump") == false && !doubleJumped){
            doubleJumpAble = true;
        }

        if (Input.GetButtonDown("Jump") && doubleJumpAble && !doubleJumped)
        {
            Debug.Log("jumping");
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(new Vector2(0f, jumpForce / 1.1f), ForceMode2D.Impulse);
            isGrounded = false;
            doubleJumpAble = false;
            doubleJumped = true;
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out ITerrain ground)){
            isGrounded = true;
            doubleJumpAble = false;
            doubleJumped = false;
            if (ground.getGroundType() == ITerrain.GroundType.Ice){
                isSlippyGround = true;
            }
            if (ground.getGroundType() == ITerrain.GroundType.Normal){
                isSlippyGround = false;
            }
            if (ground.getGroundType() == ITerrain.GroundType.win){
                GetComponent<UnitStates>().startWinEvent();
            }
        } 
    }
}
