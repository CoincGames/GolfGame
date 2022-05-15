using UnityEngine;

public class SpinAndBounce : MonoBehaviour
{
    [SerializeField]
    private float turnRate = .5f;

    [SerializeField]
    private float bounceRate = .5f;
    [SerializeField]
    private float bounceHeight = 1f;

    void FixedUpdate()
    {
        transform.RotateAround(transform.position, Vector3.up, turnRate);
    }
}
