using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidBody;

    [Header("Speed Setup")]
    public Vector2 friction = new Vector2(.1f,0);

    public float speed;
    public float speedRun;

    public float forceJump;

    [Header("Animation Setup")]

    public float jumpScaleY = 1.5f;
    public float jumpScaleX = .7f;
    public float animationDuration = .3f;


    //private bool _isRunning = false;

    private float _currentSpeed;

    private void Update()
    {
        HandleJump();
        HandleMovement();

    }
    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftControl))
            _currentSpeed = speedRun;
        else
            _currentSpeed = speed;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //myRigidBody.MovePosition(myRigidBody.position - velocity * Time.deltaTime);
            myRigidBody.velocity = new Vector2(-_currentSpeed, myRigidBody.velocity.y);
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //myRigidBody.MovePosition(myRigidBody.position + velocity * Time.deltaTime);
            myRigidBody.velocity = new Vector2(_currentSpeed, myRigidBody.velocity.y);
        }
        
        
        if(myRigidBody.velocity.x >0)
        {
            myRigidBody.velocity -= friction;
        }

        else if(myRigidBody.velocity.x < 0)
        {
            myRigidBody.velocity += friction;
        }
     
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidBody.velocity = Vector2.up * forceJump;
            myRigidBody.transform.localScale = Vector2.one;

            DOTween.Kill(myRigidBody.transform);
            HandleScaleJump();
        }
    }

    private void HandleScaleJump()
    {
        myRigidBody.transform.DOScaleY(jumpScaleY, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(Ease.Flash);
        myRigidBody.transform.DOScaleX(jumpScaleX, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(Ease.Flash);
    }
}

