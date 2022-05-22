using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 10f)]
    private float mouseSensitivity = 1f;

    [SerializeField]
    private GameObject cameraCenter;
    private Camera cam;

    [SerializeField]
    private float scrollSensitivity = 5f;
    
    [SerializeField]
    private float scrollDampening = 6f;

    [SerializeField]
    private float zoomMin = 0f;

    [SerializeField]
    private float zoomMax = 15f;

    [SerializeField]
    private float zoomDefault = .14f;

    private float zoomDistance;
    
    [SerializeField]
    private float yOffset = 1f;

	// This one is public but no need to input any values for it
    public Vector3 camDist;

    [SerializeField]
    private GameObject golfballGroup;
    private MouseSwing mouseSwing;

    public float collisionSensitivity = -1f;
    // Camera Target
    private GameObject ball;
    private DirectionIndicator directionArrowController;

    private void Awake()
    {
        ball = golfballGroup.GetComponentInChildren<Collider>().gameObject;
        mouseSwing = golfballGroup.GetComponent<MouseSwing>();
        directionArrowController = golfballGroup.GetComponentInChildren<DirectionIndicator>();

        cam = GetComponentInChildren<Camera>();
        camDist = cam.transform.localPosition;
        zoomDistance = zoomDefault;
        camDist.z = zoomDistance;
        transform.LookAt(ball.transform.position);
        setInitialRotation(180);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void setInitialRotation(float y) {
        float cameraRotate = y - cameraCenter.transform.rotation.eulerAngles.y;
        rotate(0, cameraRotate);

        float arrowRotate = y - directionArrowController.transform.rotation.eulerAngles.y - 180;
        directionArrowController.rotate(arrowRotate);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position1 = ball.transform.position;
        cameraCenter.transform.position = new Vector3(position1.x, position1.y + yOffset, position1.z);

        if (!mouseSwing.isTracking && (mouseSwing.launchTime + .1f <= Time.time))
        {
            rotate(-1f * Input.GetAxis("Mouse Y") * mouseSensitivity, Input.GetAxis("Mouse X") * mouseSensitivity);
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            zoom();
        }

        updateLocation();
    }

    void rotate(float x, float y)
    {
        Quaternion rotation = cameraCenter.transform.rotation;
        float initY = rotation.eulerAngles.y;

        float xRot = rotation.eulerAngles.x + x;

        if (xRot >= 180f) xRot -= 360f;
        xRot = Mathf.Clamp(xRot, -10f, 89f);
        if ( xRot <= 0f ) xRot += 360f;

        float yRot = rotation.eulerAngles.y + y;
        rotation = Quaternion.Euler(xRot, yRot, rotation.eulerAngles.z);
        
        directionArrowController.rotate(rotation.eulerAngles.y - initY);
        cameraCenter.transform.rotation = rotation;

    }
    void zoom()
    {
        float scrollAmount = Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;
        scrollAmount *= (zoomDistance * 0.3f);
        zoomDistance += scrollAmount * -1f;
        zoomDistance = Mathf.Clamp(zoomDistance, zoomMin, zoomMax);
    }

    void updateLocation()
    {
        if (camDist.z != zoomDistance * -1f)
        {
	        camDist.z = Mathf.Lerp(camDist.z, -zoomDistance, Time.deltaTime * scrollDampening);
        }
        cam.transform.localPosition = camDist;

        transform.LookAt(ball.transform.position);
    }
}
