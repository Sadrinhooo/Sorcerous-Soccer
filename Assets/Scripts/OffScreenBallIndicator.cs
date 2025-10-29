using UnityEngine;

public class OffScreenBallIndicator : MonoBehaviour
{
    [SerializeField] private GameObject ballIndicator, ballIndicatorCenter;

    private GameObject ball;

    private float indicatorOffset = 30;

    private float indicatorScaleMultiplier = 6f;

    private Vector2 baseIndicatorScale;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ball = PlayerControls.ball;
        ballIndicator.SetActive(false);
        baseIndicatorScale = ballIndicatorCenter.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
         IndicateBallPosition();
    }

    public void IndicateBallPosition()
    {
        if (!IsOnScreen())
        {
            ballIndicator.SetActive(true);
            Vector3 ballScreenSpacePos = Camera.main.WorldToScreenPoint(ball.transform.position);
            ballScreenSpacePos = new Vector3(Mathf.Clamp(ballScreenSpacePos.x, 0 + indicatorOffset, Screen.width - indicatorOffset), Mathf.Clamp(ballScreenSpacePos.y, 0 + indicatorOffset, Screen.height - indicatorOffset), 10);
            ballIndicator.transform.position = Camera.main.ScreenToWorldPoint(ballScreenSpacePos);
        }
        else
        {
            ballIndicator.SetActive(false);
        }

        Vector2 ballVelocity = ball.GetComponent<Rigidbody2D>().linearVelocity;
        ballVelocity.Normalize();
        float indicatorDegree = Mathf.Atan2(ballVelocity.y, ballVelocity.x) * Mathf.Rad2Deg;
        ballIndicator.transform.rotation = Quaternion.Euler(0, 0, indicatorDegree);

        //Change ball scale depending on ball distance from screen
        float ballToScreenDistance = Vector2.Distance(ball.transform.position, GetScreenCenter());
        ballIndicatorCenter.transform.localScale = baseIndicatorScale * (indicatorScaleMultiplier / ballToScreenDistance);
        
        Vector2 ballToScreenVector = GetScreenCenter() - (Vector2) ball.transform.localPosition;
        float currentZ = ballIndicator.transform.eulerAngles.z;
        if (Vector2.Dot(ballToScreenVector.normalized, ballVelocity.normalized) > 0)
        {
            ballIndicator.transform.rotation = Quaternion.Euler(0, 0, currentZ + 180);
        }
    }

    public bool IsOnScreen()
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(ball.transform.position);
        return viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1 &&viewportPos.z > 0;
    }

    public Vector2 GetScreenCenter()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2));
    }
}
