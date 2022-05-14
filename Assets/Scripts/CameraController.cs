using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Vector3 cameraOffset = new Vector3(-1.2f, 1f, 0);

    [SerializeField]
    [Range(0.1f, 10f)]
    private float rotationSensitivity = .5f;

    [SerializeField]
    private GameObject golfballGroup;

    private GameObject ball;
    private DirectionIndicator directionArrowController;

    private float degreestoRotatePerFrame = .5f;

    private void Awake()
    {
        ball = golfballGroup.GetComponentInChildren<Collider>().gameObject;
        directionArrowController = golfballGroup.GetComponentInChildren<DirectionIndicator>();
    }

    // Update is called once per frame
    void Update()
    {
        updateLocation();

        bool qPressed = Input.GetKey("q");
        bool ePressed = Input.GetKey("e");

        if (qPressed)
        {
            rotate(false);
        }
        else if (ePressed)
        {
            rotate(true);
        }
    }

    void rotate(bool isPositive)
    {
        cameraOffset = transform.position - ball.transform.position;
        float rotationDegree = (isPositive ? degreestoRotatePerFrame : -degreestoRotatePerFrame) * rotationSensitivity;
        cameraOffset = Quaternion.Euler(0, rotationDegree, 0) * cameraOffset;
        directionArrowController.rotate(rotationDegree);
        updateLocation();
    }

    void updateLocation()
    {
        transform.position = cameraOffset + ball.transform.position;
        Vector3 ballPos = ball.transform.position;
        Vector3 cameraLookVector = new Vector3(ballPos.x, ballPos.y + .435f, ballPos.z);
        transform.LookAt(cameraLookVector);
    }
}
