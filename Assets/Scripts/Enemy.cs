using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] Collider2D platformEdgeCollider;

    Rigidbody2D myRigidBody;
    int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    private void Move()
    {
        myRigidBody.velocity = new Vector2 (movementSpeed * direction, myRigidBody.velocity.y);
    }


    private void OnTriggerExit2D(Collider2D otherCollider) 
    {
        if(!otherCollider.gameObject.GetComponent<Player>() && !otherCollider.gameObject.GetComponentInParent<Player>())     // check if the other collider isnt from the player or the childrens of the player
        {
            FlipEnemy();    
        }
    }

    private void FlipEnemy()
    {
        direction = direction * -1;
        transform.localScale = new Vector3(transform.localScale.x * -1 , 1, 1);
    }


}
