using UnityEngine;

public class StartPoint : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, .025f);

        Gizmos.DrawRay(new Ray(transform.position, transform.forward));
    }

    public Transform getTransform()
    {
        return transform;
    }

    public Vector3 getPosition()
    {
        return transform.position;
    }
}
