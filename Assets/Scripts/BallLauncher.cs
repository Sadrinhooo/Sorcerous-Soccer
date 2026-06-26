using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    private Transform LauncherTransform;
    [SerializeField] float shootForce;
    GameObject ball;
    Rigidbody2D ballRigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LauncherTransform = GetComponent<Transform>();
        ball = GameObject.FindGameObjectWithTag("Ball");
        ballRigidbody = ball.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("ResetBall"))
        {
            ballRigidbody.linearVelocity = Vector2.zero;
            ball.transform.position = LauncherTransform.position + (LauncherTransform.up * 2);
            ballRigidbody.AddForce(LauncherTransform.up * shootForce, ForceMode2D.Impulse);
            
        }
    }
}
