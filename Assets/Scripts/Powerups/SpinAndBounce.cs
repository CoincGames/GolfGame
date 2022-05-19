using UnityEngine;

public class SpinAndBounce : MonoBehaviour
{
    [SerializeField]
    private float turnRate = .5f;

    [SerializeField]
    private float bounceRate = .5f;
    [SerializeField]
    private float bounceHeight = 1f;

    private Vector3 startPos;

    private void Awake()
    {
        startPos = transform.position;
    }

    void FixedUpdate()
    {
        // Rotation
        transform.RotateAround(transform.position, Vector3.up, turnRate);

        // Bounces
        float rate = Time.time * bounceRate;
        float bounce = (float)(Mathf.Sin(rate)) * bounceHeight;
        transform.position = new Vector3(startPos.x, startPos.y + bounce, startPos.z);
    }
}
