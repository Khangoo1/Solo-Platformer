using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour

{
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpForce;

    private Inputs Inputs;
    private Vector2 direction;
    private Rigidbody2D myRigidBody;

    private bool isOnGround = false;

    private void OnEnable()
    {

        Inputs = new Inputs();
        Inputs.Enable();
        Inputs.Player.Move.performed += OnMovePerformed;
        Inputs.Player.Move.canceled += OnMoveCanceled;
        Inputs.Player.Jump.performed += OnJumpPerformed;

    }


private void OnJumpPerformed(InputAction.CallbackContext obj)
{
    if (isOnGround)
    {
        myRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var myRigidBody = GetComponent<Rigidbody2D>();
        direction.y = 0;
        //myRigidBody.MovePosition(direction);
        //myRigidBody.velocity = direction;
        if (myRigidBody.velocity.sqrMagnitude < maxSpeed)
            myRigidBody.AddForce(direction * speed);
    }
}
