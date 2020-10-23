using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour

{
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask ground;

    //private Inputs Inputs;
    private Vector2 direction;
    private Rigidbody2D myRigidBody;

    private bool isOnGround = true;
    private Animator myAnimator;
    private SpriteRenderer myRenderer;

    private void OnEnable()
    {

        var Inputs = new Inputs();
        Inputs.Enable();
        Inputs.Player.Move.performed += OnMovePerformed;
        Inputs.Player.Move.canceled += OnMoveCanceled;
        Inputs.Player.Jump.performed += OnJumpPerformed;
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myRenderer = GetComponent<SpriteRenderer>();

    }


private void OnJumpPerformed(InputAction.CallbackContext obj)
{
    if (isOnGround)
    {
        myRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isOnGround = false;
    }
}


private void OnMovePerformed(InputAction.CallbackContext obj)
{
    direction = obj.ReadValue<Vector2>();
    Debug.Log(direction);
}


private void OnMoveCanceled(InputAction.CallbackContext obj)
{
    direction = Vector2.zero;
}


    // Start is called before the first frame update
    void Start()
    {

    //GetComponent<Animator>().SetBool(*IsRunning*, true);

        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //var myRigidBody = GetComponent<Rigidbody2D>();
        //direction.y = 0;
        //myRigidBody.MovePosition(direction);
        //myRigidBody.velocity = direction;
        //why are there so many commented lines
        //wdym i'm afraid of commitment 
        //YOU'RE afraid of commitment
        var PlayerDirection = new Vector2
        {
            x = direction.x,
            y = 0
        };


        if (myRigidBody.velocity.sqrMagnitude < maxSpeed)
        { 
            myRigidBody.AddForce(direction * speed);
        }
        var isRunning = direction.x != 0;
        myAnimator.SetBool("isRunning", isRunning);
        if(direction.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (direction.x > 0)
        {
            myRenderer.flipX = false;
        }

        
            
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var touchGround = ground == (ground | (1 << other.gameObject.layer));
        var touchFromAbove = other.contacts[0].normal == Vector2.up;
        if (touchGround && touchFromAbove)
        //if (other.gameObject.CompareTag("Ground") == true)
        //do i know what i'm doing?
        //does anyone?
        {
            isOnGround = true;
        }

        //The only thing getting destroyed here is my mental health

        if(other.gameObject.tag == "DeathPit")
       {
        Destroy(other.gameObject);
       }
       //just yeet me into the gameObject.DeathPit already
    }


   
}
