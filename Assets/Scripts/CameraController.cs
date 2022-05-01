using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;
    [SerializeField]
    private Vector3 cameraOffset;

    [SerializeField]
    [Range(.1f, 5f)]
    private float degreestoRotatePerFrame;

    // Update is called once per frame
    void Update()
    {
        transform.position = ball.transform.position + cameraOffset;

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
        cameraOffset = Quaternion.Euler(0, isPositive ? degreestoRotatePerFrame : -degreestoRotatePerFrame, 0) * cameraOffset;
        transform.position = cameraOffset + ball.transform.position;
        transform.LookAt(ball.transform.position);
    }
}
