using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public interface JoystickState
{
    void HandleInput(Vector2 input);
}
public enum PlayerControllers
{
    Player1, Player2
}

public class PlayerControls : MonoBehaviour
{
    public class MovementState : JoystickState
    {
        public void HandleInput(Vector2 input)
        {
            
        }
    }

    public class AimState : JoystickState
    {
        public void HandleInput(Vector2 input)
        {

        }
    }

    [SerializeField] private Transform foot;
    [SerializeField] private Transform leftRaycastPoint, rightRaycastPoint;
    [SerializeField] private float ballInReachThreshhold;
    [SerializeField] private float ballToFeetSpeed;
    [SerializeField] private PlayerControllers playerController;
    [SerializeField] private float horizontalMoveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float shotPower;
    [SerializeField] private LayerMask whatIsGround;

    private Rigidbody2D rb2D;
    private Vector3 facingRight = new Vector3(1f, 1.5f, 1f);
    private Vector3 facingLeft = new Vector3(-1f, 1.5f, 1f);
    private bool isFacingRight = true;
    private GameObject ball;
    private Vector2 joystickDirection;
    private Vector2 shotDirection;
    private float rayDistance = 0.3f;
    private bool isAiming = false;
    private float velocityDampingSpeed = 4f;

    //StateMachine
    private JoystickState currentState;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        ball = GameObject.FindGameObjectWithTag("Ball");
    }

    void Update()
    {
        joystickDirection.x = Input.GetAxis("Horizontal_" + playerController.ToString());
        joystickDirection.y = Input.GetAxis("Vertical_" + playerController.ToString());

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
        }

        if (Input.GetButton("Aim_" + playerController.ToString()) && Vector2.Distance(transform.position, ball.transform.position) < ballInReachThreshhold)
        {
            if (ball.GetComponent<BallBehaviour>().CurrentBallHolder == null)
            {
                Aim();
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

        ball.transform.position = Vector2.Lerp(ball.transform.position,foot.transform.position, Time.deltaTime * ballToFeetSpeed);
        ball.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

        shotDirection = new Vector2(-joystickDirection.x, joystickDirection.y) * shotPower;

        if (joystickDirection.magnitude > 0.1f)
        {
            isAiming = true;
            Debug.Log("Aiming direction: " + joystickDirection + " Shot direction: " + shotDirection);
        }
        else
        {
            isAiming = false;
            Debug.Log("Aiming direction: " + joystickDirection + " Shot direction: " + shotDirection);
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

        return (leftHit.collider != null && leftHit.collider.CompareTag("Ground")) ||
               (rightHit.collider != null && rightHit.collider.CompareTag("Ground"));
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
        //Fix later with Debug.DrawLine???*
    }

    //Try setting states from the delegated classes in JoystickState script???
    public void SetState(JoystickState newState)
    {
        currentState = newState;
    }
}


