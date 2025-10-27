using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    [SerializeField] private int defaultCameraFOV;
    [SerializeField] private int cameraZoomMarginal = 15; //The distance between players before camera starts zooming out
    [SerializeField] private float cameraZoomMultiplier;
    [SerializeField] private float cameraSpeedMultiplier;
    private int cameraFOV;

    [SerializeField] private Transform player1Transform;
    [SerializeField] private Transform player2Transform;

    private float playerDistance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraFOV = defaultCameraFOV;
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, cameraFOV, cameraSpeedMultiplier * Time.deltaTime);
        playerDistance = Vector2.Distance(player1Transform.position, player2Transform.position);
        transform.position = Vector3.Lerp(transform.position, GetCameraPosition(), cameraSpeedMultiplier * Time.deltaTime);
        

        if (playerDistance > cameraZoomMarginal)
        {
            cameraFOV = (int)(cameraZoomMultiplier * playerDistance);
        }
        else
        {
            cameraFOV = defaultCameraFOV;
        }

    }

    public Vector3 GetCameraPosition()
    {
        float positionMedian = playerDistance / 2;
        float Xposition = (Mathf.Min(player1Transform.position.x, player2Transform.position.x) + positionMedian);
        return new Vector3(Xposition, transform.position.y, -10);
    }
}
