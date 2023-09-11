using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public HealthBase healthBase;

    [Header("Speed Setup")]
    public Vector2 friction = new Vector2(.1f,0);

    public float speed;
    public float speedRun;

    public float forceJump;

    [Header("Animation Setup")]

    public float jumpScaleY = 1.5f;
    public float jumpScaleX = .7f;
    public float animationDuration = .3f;

    [Header("Animation Player")]
    public string boolRun = "Run";
    public string triggerDeath = "Death";
    public Animator animator;
    public float playerSwipeDuration = .2f;

    private int direction = 1;


    //private bool _isRunning = false;

    private float _currentSpeed;

    private void Awake()
    {
        if(healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }
    }

    private void OnPlayerKill()
    {
        healthBase.OnKill -= OnPlayerKill;

        animator.SetTrigger(triggerDeath);
    }
    private void Update()
    {
        HandleJump();
        HandleMovement();

    }
    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            _currentSpeed = speedRun;
            animator.speed = 2;
        }
        else
        {
            _currentSpeed = speed;
            animator.speed = 1;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //myRigidBody.MovePosition(myRigidBody.position - velocity * Time.deltaTime);
            myRigidBody.velocity = new Vector2(-_currentSpeed, myRigidBody.velocity.y);
            
            if(myRigidBody.transform.localScale.x != -1)
            {
                myRigidBody.transform.DOScaleX(-1, playerSwipeDuration);
            }

            animator.SetBool(boolRun, true);
            direction = -1;
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //myRigidBody.MovePosition(myRigidBody.position + velocity * Time.deltaTime);
            myRigidBody.velocity = new Vector2(_currentSpeed, myRigidBody.velocity.y);
            if (myRigidBody.transform.localScale.x != 1)
            {
                myRigidBody.transform.DOScaleX(1, playerSwipeDuration);
            }
            animator.SetBool(boolRun, true);
            direction = 1;
        }
        else
        {
            animator.SetBool(boolRun, false);
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

            HandleScaleJump();
            DOTween.Kill(myRigidBody.transform.localScale);

        }
    }

    private void HandleScaleJump()
    {
        float finalXScale = direction == 1 ? jumpScaleX : -jumpScaleX;
        myRigidBody.transform.DOScaleX(finalXScale, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(Ease.Flash);
        myRigidBody.transform.DOScaleY(jumpScaleY, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(Ease.Flash);
     
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}

