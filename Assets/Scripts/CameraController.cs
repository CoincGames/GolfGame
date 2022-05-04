using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;
    [SerializeField]
    private DirectionIndicator directionArrowController;
    [SerializeField]
    private Vector3 cameraOffset;

    [SerializeField]
    [Range(.1f, 5f)]
    private float degreestoRotatePerFrame;

    Vector3 camInitPos;
    Quaternion camInitRot;

    private void Start()
    {
        camInitPos = transform.position;
        camInitRot = transform.rotation;
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
        float rotationDegree = isPositive ? degreestoRotatePerFrame : -degreestoRotatePerFrame;
        cameraOffset = Quaternion.Euler(0, rotationDegree, 0) * cameraOffset;
        directionArrowController.rotate(rotationDegree);
        updateLocation();
        Vector3 ballPos = ball.transform.position;
        Vector3 cameraLookVector = new Vector3(ballPos.x, ballPos.y + .435f, ballPos.z);
        transform.LookAt(cameraLookVector);
    }

    void updateLocation()
    {
        transform.position = cameraOffset + ball.transform.position;
    }
}
