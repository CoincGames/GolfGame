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

    private void Awake()
    {
        if (setSingleton())
        {
            // Anything for setting up this specific manager, do here
        }
    }

    public void finishHole(GameObject golfball)
    {
        currentHole++;

        if (currentHole <= startsPoints.Length)
        {
            sendToNextStartPoint(golfball);
            return;
        }

        finishCourse();
    }

    private void sendToNextStartPoint(GameObject golfball)
    {
        Vector3 nextStartPosition = getStartPoint(currentHole).getPosition();
        golfball.transform.position = nextStartPosition;
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
