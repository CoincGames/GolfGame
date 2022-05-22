using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    [Min(1)]
    private int currentHole = 1;

    [SerializeField]
    [Min(1)]
    private float timeBetweenGoalAndNextHole = 2.5f;

    [SerializeField]
    private StartPoint[] startsPoints;

    [SerializeField]
    private CameraController cameraController;

    private void Awake()
    {
        if (setSingleton())
        {
            // Anything for setting up this specific manager, do here
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentHole = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentHole = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentHole = 3;
        }
    }

    public void finishHole(GameObject golfball)
    {
        if (currentHole + 1 <= startsPoints.Length)
        {
            sendToNextStartPoint(golfball);
            return;
        }

        finishCourse();
    }

    public int getCurrentHole()
    {
        return currentHole;
    }

    public void sendToPosition(Vector3 position, GameObject golfball)
    {
        golfball.transform.position = position;
        stopRigidbodyVelocities(golfball);

    }

    public void sendToCurrentStartPoint(GameObject golfball)
    {
        Transform startTransform = getStartPoint(currentHole).getTransform();
        sendToPosition(getStartPoint(currentHole).getTransform().position, golfball);
        cameraController.setInitialRotation(startTransform.rotation.eulerAngles.y);
    }

    private void sendToNextStartPoint(GameObject golfball)
    {   
        Transform startTransform = getStartPoint(++currentHole).getTransform();
        sendToPosition(startTransform.position, golfball);
        cameraController.setInitialRotation(startTransform.rotation.eulerAngles.y);
        golfball.GetComponentInParent<MouseSwing>().setResetPosition(startTransform.position);
    }

    private void setGolfballTransform(GameObject golfball, Transform transform)
    {
        golfball.transform.position = transform.position;
        golfball.transform.rotation = transform.rotation;
    }

    private void stopRigidbodyVelocities(GameObject golfball)
    {
        Rigidbody rb = golfball.GetComponent<Rigidbody>();
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
    }

    private void finishCourse()
    {
        print("Course Finished!");
        currentHole = 1;
    }

    public Transform getStartPositionForCurrentHole()
    {
        return getStartPoint(currentHole).getTransform();
    }

    public float getTimeBetweenGoalAndNextHole()
    {
        return timeBetweenGoalAndNextHole;
    }

    private StartPoint[] getStartPoints()
    {
        return startsPoints;
    }

    private StartPoint getStartPoint(int holeNumber)
    {
        return getStartPoints()[holeNumber - 1];
    }

    bool setSingleton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return true;
        }

        Destroy(gameObject);
        return false;
    }
}
