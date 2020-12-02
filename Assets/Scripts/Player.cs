using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    // configs
    [Header("Player Movements")]
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] float jumpSpeed = 1f;      
    
    [Header("Death Flop")]          
    [SerializeField] Vector2 deathFlop = new Vector2 (10f , 15f);

    [Header("Colliders")]
    [SerializeField] Collider2D myFeetCollider = null; 
    
    // cached components
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    Collider2D myBodyCollider;

    bool isPlayerAlive = true;


    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerAlive)
        {
            Run();
            Jump();
            FlipPlayerSprite();
            ClimbLadders();
        }
    }
    

    private void Run()
    {       
        float keyInput  = Input.GetAxis("Horizontal");
        myRigidBody.velocity = new Vector2(movementSpeed * keyInput, myRigidBody.velocity.y); 
        myAnimator.SetBool("IsRunning", true);    
    }


    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                Vector2 jumpVelocity = new Vector2 (0f, jumpSpeed);
                myRigidBody.velocity += jumpVelocity;
            }
        }
    }


    private void FlipPlayerSprite()
    {
        bool isPlayerMoving = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        if(isPlayerMoving)
        {
            transform.localScale = new Vector3 (Mathf.Sign(myRigidBody.velocity.x), 1, 1);
        }
        else
        {
            myAnimator.SetBool("IsRunning", false);    
        }
    }


    private void ClimbLadders()
    {
        if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladders")))
        {   
            float keyInput  = Input.GetAxis("Vertical");
            if(Mathf.Abs(keyInput) > Mathf.Epsilon)
            {
                myRigidBody.gravityScale = 0;
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x , climbSpeed * keyInput); 
                myAnimator.SetBool("IsClimbing", true);   
            }
            else
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, myRigidBody.velocity.y);
            }
        }
        else
            {
                myAnimator.SetBool("IsClimbing", false);
                myRigidBody.gravityScale = 1;
            }
    }



    private void OnTriggerEnter2D(Collider2D otherCollider) 
    {
        if( myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazard")))
        {   
            otherCollider.enabled = false;  // turn off the collider for whatever collided into player so they cant collide once already dead
            StartCoroutine(HandleDeath()); 
        }
    }

    IEnumerator HandleDeath()
    {
        isPlayerAlive = false;                          // player loses controls
        myRigidBody.velocity = deathFlop;
        while(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))     // waits for player to start the deathflop to get off the gorund
            {   
                yield return null;
            }

        while(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))    // waits for player to reach the ground again before disabling the physics
            {   
                yield return null;
            }
        myAnimator.SetBool("isDead", true);             // activate animation to drop player

        FindObjectOfType<GameSession>().HandlePlayerDeath();

    }
    
}
