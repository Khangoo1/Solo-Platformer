using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour

{

    private Inputs Inputs;
    private Vector2 direction;

    private void OnEnable()
    {

        Inputs = new Inputs();
        Inputs.Enable();
        Inputs.Player.Move.performed += OnMovePerformed;

    }

private void OnMovePerformed(InputAction.CallbackContext obj)
{
    direction = obj.ReadValue<Vector2>();
    Debug.Log(direction);
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
        myRigidBody.velocity = direction;
    }
}
