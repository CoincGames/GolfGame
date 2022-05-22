using UnityEngine;

public class DirectionIndicator : MonoBehaviour
{
    [SerializeField]
    private GameObject golfball;

    private void Update()
    {
        transform.position = golfball.transform.position - (transform.forward * .15f);
    }

    public void rotate(float rotationDegree)
    {
        transform.RotateAround(golfball.transform.position, Vector3.up, rotationDegree);
    }
}
