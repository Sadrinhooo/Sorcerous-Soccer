using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
public enum PlayerControllers
{
    Player1, Player2
}

public class PlayerControls : MonoBehaviour{

    [SerializeField] private Transform foot;
    [SerializeField] private Transform leftRaycastPoint, rightRaycastPoint;
    [SerializeField] private float ballInReachThreshhold;
    [SerializeField] private float ballToFeetSpeed;
    [SerializeField] private PlayerControllers playerController;
    [SerializeField] private float horizontalMoveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float shotPower;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private string[] validGroundTags = { "Ground", "Player", "Ball" };

    private Rigidbody2D rb2D;
    private Vector3 facingRight = new Vector3(1f, 1.5f, 1f);
    private Vector3 facingLeft = new Vector3(-1f, 1.5f, 1f);
    private bool isFacingRight = true;
    public static GameObject ball;
    private Vector2 joystickDirection;
    private Vector2 shotDirection;
    private float rayDistance = 0.3f;
    private bool isAiming = false;
    private float velocityDampingSpeed = 4f;
    private float ballVelocity;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.gravityScale = 1.5f;
        ball = GameObject.FindGameObjectWithTag("Ball");
    }

    void Update()
    {
        joystickDirection.x = Input.GetAxis("Horizontal_" + playerController.ToString());
        joystickDirection.y = Input.GetAxis("Vertical_" + playerController.ToString());
        ballVelocity = ball.GetComponent<Rigidbody2D>().linearVelocity.magnitude;

        if (Input.GetButtonUp("Aim_" + playerController.ToString()))
        {
            if (ball.GetComponent<BallBehaviour>().CurrentBallHolder == gameObject)
            {
                ball.GetComponent<BallBehaviour>().CurrentBallHolder = null;
            }

            if (isAiming)
            {
                Shoot(shotDirection);
            }

            rb2D.gravityScale = 1.5f;
        }

        if (Input.GetButton("Aim_" + playerController.ToString()) && Vector2.Distance(transform.position, ball.transform.position) < ballInReachThreshhold)
        {
            if (ball.GetComponent<BallBehaviour>().CurrentBallHolder == null)
            {
                Aim();
                rb2D.gravityScale = 1f;
            }
        }
        else
        {
            isAiming = false;
            MovePlayer();
        }

        if (Input.GetButtonDown("Jump_" + playerController.ToString()) && IsGrounded())
        {
            Jump();
        }

        FlipPlayer();
    }

    private void Aim()
    {
        rb2D.linearVelocity = Vector2.Lerp(rb2D.linearVelocity, Vector2.zero, Time.deltaTime * velocityDampingSpeed);

        Vector2 ballFromBodyVector = new Vector2(joystickDirection.x * -1, joystickDirection.y * -1);
        float ballRotation = Mathf.Acos(ballFromBodyVector.x) * Mathf.Rad2Deg;

        ball.transform.position = Vector2.Lerp(ball.transform.position, foot.transform.position, Time.deltaTime * ballToFeetSpeed);
        ball.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

        shotDirection = new Vector2(-joystickDirection.x, joystickDirection.y) * shotPower;

        if (joystickDirection.magnitude > 0.1f)
        {
            isAiming = true;
        }
        else
        {
            isAiming = false;
        }

        DrawArrow(joystickDirection);
    }

    private void Jump()
    {
        rb2D.AddForce(new Vector2(0, jumpForce));
    }

    private bool IsGrounded()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftRaycastPoint.position, Vector2.down, rayDistance, whatIsGround);
        RaycastHit2D rightHit = Physics2D.Raycast(rightRaycastPoint.position, Vector2.down, rayDistance, whatIsGround);

        if (leftHit.collider != null)
        {
            foreach (string tag in validGroundTags)
            {
                if (leftHit.collider.CompareTag(tag))
                    return true;
            }
        }

        if (rightHit.collider != null)
        {
            foreach (string tag in validGroundTags)
            {
                if (rightHit.collider.CompareTag(tag))
                    return true;
            }
        }

        return false;
        
    }

    private void FlipPlayer()
    {
        if (isAiming)
        {
            if (joystickDirection.x > 0.01f && isFacingRight)
            {
                isFacingRight = false;            
            }
            else if (joystickDirection.x < -0.01f && !isFacingRight)
            {
                isFacingRight = true; 
            }
        }
        else
        {
            if (joystickDirection.x > 0.01f && !isFacingRight)
            {
                isFacingRight = true;
            }
            else if (joystickDirection.x < -0.01f && isFacingRight)
            {
                isFacingRight = false;
            }
        }

        transform.localScale = isFacingRight ? facingRight : facingLeft;
    }

    private void Shoot(Vector2 direction)
    {
        ball.GetComponent<Rigidbody2D>().AddForce(direction, ForceMode2D.Impulse);
    }

    public void MovePlayer()
    {
        rb2D.linearVelocity = new Vector2(joystickDirection.x * horizontalMoveSpeed, rb2D.linearVelocity.y);
    }

    public void DrawArrow(Vector2 joystickDirection)
    {
        Vector2 ballPosition = ball.transform.position;
        Debug.DrawLine(ballPosition, ballPosition + shotDirection, Color.red);
    }

}


