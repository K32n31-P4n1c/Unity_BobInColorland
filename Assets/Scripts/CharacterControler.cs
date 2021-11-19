using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControler : MonoBehaviour
{
    [SerializeField]
    float runSpeed = 5f;

    [SerializeField]
    float jumpspeed = 5f;

    [SerializeField]
    Vector2 deahKick = new Vector2(15f, 15f);

    [SerializeField]
    public GameObject ObjectToSpawn;

    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider2D;
    BoxCollider2D myFeetCollider2D;

    bool isAlive = true;
    int ColorAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        myFeetCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if(!isAlive)
        {
            return;
        }

        Run();
        Jump();
        FlipSprite();
        Paint();
        Die();
    }

    void Run()
    {
        float controlDirection = Input.GetAxis("Horizontal");  // -1 to +1
        Vector2 playerVelocity = new Vector2(controlDirection * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool playerMoving = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerMoving);
    }

    void Jump()
    {
        if(!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myAnimator.SetBool("Jumping", false);
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpspeed);
            myRigidBody.velocity += jumpVelocity;
            myAnimator.SetBool("Jumping", true);
        }
    }

    void Die()
    {
        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Die");
            GetComponent<Rigidbody2D>().velocity = deahKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    void Cast()
    {
        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("PickUps")))
        {
            myAnimator.SetTrigger("Cast");
        }
    }

    void Paint()
    {
        if (FindObjectOfType<GameSession>().color != 0 && Input.GetButtonDown("Fire1"))
        {
                if (transform.localScale.x < 0)
                {
                    Instantiate(ObjectToSpawn, transform.position + new Vector3(-3,0.7f,0), Quaternion.identity);
                }
                else
                {
                    Instantiate(ObjectToSpawn, transform.position + new Vector3(3,0.7f,0), Quaternion.identity);
                }
                FindObjectOfType<GameSession>().SubtractColorAmount(1);
                myAnimator.SetTrigger("Cast");
        }
    }


    void FlipSprite()
    {
        bool playerMoving = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        if (playerMoving)
        {
            // reverse the current scaling of x axis
            transform.localScale = new Vector2 (Mathf.Sign(myRigidBody.velocity.x) / 2, 0.5f);
        }
    }
}
