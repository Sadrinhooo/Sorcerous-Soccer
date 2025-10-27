using UnityEngine;

public class OffScreenBallIndicator : MonoBehaviour
{
    [SerializeField] private GameObject ballIndicator;

    private GameObject ball;

    private Vector3 screenBarPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ball = PlayerControls.ball;
    }

    // Update is called once per frame
    void Update()
    {
         IndicateBallPosition();
    }

    public void IndicateBallPosition()
    {
        
    }

    public bool IsOnScreen()
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(ball.transform.position);
        return viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1 &&viewportPos.z > 0;
    }
}
