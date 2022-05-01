using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class MouseSwing : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float swingTimeGapAllowance;

    [SerializeField]
    private float testInputForce;

    [SerializeField]
    private Slider powerTracker;

    [SerializeField]
    private Color low;
    [SerializeField]
    private Color high;

    bool isTracking = false;
    float timeStarted = 0;
    float trackedMovement = 0;
    bool impactOnFixed = false;

    Vector3 initPos;
    Quaternion initRot;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 500;
        rb.sleepThreshold = .2f;

        initPos = transform.position;
        initRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        drawLine();

        if (Input.GetMouseButtonDown(0))
        {
            timeStarted = Time.time;
            isTracking = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            impactOnFixed = true;
            isTracking = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            reset();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            testImpact();
        }

        if (isTracking)
        {
            getImpactForce();
            trackMouseMovement();
        }
    }

    private void FixedUpdate()
    {
        if (impactOnFixed)
        {
            doImpact();
            impactOnFixed = false;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 camPos = cam.transform.position;
        Vector3 ballPos = transform.position;
        Vector3 pointA = new Vector3(camPos.x, ballPos.y, camPos.z);

        Vector3 tempSize = new Vector3(0.2f, 0.2f, 0.2f);
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(pointA, tempSize);
        Gizmos.color = Color.green;
        Gizmos.DrawCube(ballPos, tempSize);
    }

    private void reset()
    {
        transform.SetPositionAndRotation(initPos, initRot);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void drawLine()
    {
        Vector3 camPos = cam.transform.position;
        Vector3 ballPos = transform.position;
        Vector3 pointA = new Vector3(camPos.x, ballPos.y, camPos.z);

        Vector3 dir = (ballPos - pointA).normalized;

        Debug.DrawLine(ballPos, ballPos + dir * 5, Color.red);
    }

    void trackMouseMovement()
    {
        float mouseMovement = Input.GetAxis("Mouse X");

        if (mouseMovement <= 0)
        {
            return;
        }

        float scaledMouseMovement = Mathf.Abs(mouseMovement * Time.deltaTime);
        trackedMovement += scaledMouseMovement;
        StartCoroutine(removeValue(scaledMouseMovement));
    }

    IEnumerator removeValue(float value)
    {
        yield return new WaitForSeconds(swingTimeGapAllowance);
        trackedMovement -= value;

        if (trackedMovement < 0)
        {
            trackedMovement = 0;
        }
    }

    void testImpact()
    {
        Vector3 camPos = cam.transform.position;
        Vector3 ballPos = transform.position;
        Vector3 pointA = new Vector3(camPos.x, ballPos.y, camPos.z);

        Vector3 dir = (ballPos - pointA).normalized;

        GetComponent<Rigidbody>().AddForce(dir * testInputForce, ForceMode.Impulse);
    }

    float getImpactForce()
    {
        float timeElapsed = Time.time - timeStarted;
        float relevantTimeElapsed = Mathf.Min(timeElapsed, swingTimeGapAllowance);

        if (relevantTimeElapsed <= 0 || trackedMovement <= 0)
        {
            return 0f;
        }

        float distancePerSecond = trackedMovement / relevantTimeElapsed;

        float force = distancePerSecond * 250;

        if (force > powerTracker.maxValue)
        {
            powerTracker.maxValue = force;
        }
        powerTracker.value = force;

        powerTracker.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, powerTracker.normalizedValue);

        return force;
    }

    void doImpact()
    {
        if (trackedMovement <= 0)
        {
            return;
        }

        float force = getImpactForce();

        Vector3 camPos = cam.transform.position;
        Vector3 ballPos = transform.position;
        Vector3 pointA = new Vector3(camPos.x, ballPos.y, camPos.z);

        Vector3 dir = (ballPos - pointA).normalized;

        GetComponent<Rigidbody>().AddForce(dir * force, ForceMode.Impulse);
        trackedMovement = 0;

        print(force);
    }
}