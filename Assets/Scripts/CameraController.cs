using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 10f)]
    private float mouseSensitivity = 1f;

    [SerializeField]
    private MouseSwing mouseSwing;

    public GameObject cameraCenter;
    public Camera cam;
    public float scrollSensitivity = 5f;
    public float scrollDampening = 6f;
    public float zoomMin = 0f;
    public float zoomMax = 15f;
    public float zoomDefault = .14f;
    public float zoomDistance;
    
    public float yOffset = .435f;
    private RaycastHit _camHit;
	// This one is public but no need to input any values for it
    public Vector3 camDist;

    [SerializeField]
    private GameObject golfballGroup;

    public float collisionSensitivity = 1f;
    // Camera Target
    private GameObject ball;
    private DirectionIndicator directionArrowController;

    private void Awake()
    {
        ball = golfballGroup.GetComponentInChildren<Collider>().gameObject;
        camDist = cam.transform.localPosition;
        zoomDistance = zoomDefault;
        camDist.z = zoomDistance;
        transform.LookAt(ball.transform.position);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        var position1 = ball.transform.position;
        cameraCenter.transform.position = new Vector3(position1.x, position1.y + yOffset, position1.z);

        if (!mouseSwing.isTracking && (mouseSwing.launchTime + .1f <= Time.time))
        {
            rotate();
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            zoom();
        }

        updateLocation();
    }

    void rotate()
    {
        var rotation = cameraCenter.transform.rotation;

        float xRot = rotation.eulerAngles.x - Input.GetAxis("Mouse Y") * mouseSensitivity;
        xRot = Mathf.Clamp(xRot, 0f, 89f);

        float yRot = rotation.eulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;

        rotation = Quaternion.Euler(xRot, yRot, rotation.eulerAngles.z);
        cameraCenter.transform.rotation = rotation;

    }
    void zoom()
    {
        var scrollAmount = Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;
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
        var tempTransform = cam.transform;
        tempTransform.localPosition = camDist;

        // Check and handle Collision
        GameObject obj = new GameObject();
        obj.transform.SetParent(tempTransform.parent);
        var position = cam.transform.localPosition;
        obj.transform.localPosition = new Vector3(position.x, position.y, position.z - collisionSensitivity);
		/*
		Linecast is an alternative to Raycast, using it to cast a ray between the CameraCenter 
		and a point directly behind the camera (to smooth things, that's why there's an "obj"
		GameObject, that is directly behind cam)
		*/
        
        if(Physics.Linecast(cameraCenter.transform.position, obj.transform.position, out _camHit))
        {
			//This gets executed if there's any collider in the way
			var transform1 = cam.transform;
			transform1.position = _camHit.point;
            var localPosition = transform1.localPosition;
            localPosition = new Vector3(localPosition.x, localPosition.y, localPosition.z + collisionSensitivity);
            transform1.localPosition = localPosition;
        }
		// Clean up
        Destroy(obj);
        
        transform.LookAt(ball.transform.position);
    }
}
