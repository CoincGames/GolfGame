using UnityEngine;

public class StartPointPreview : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, .05f);
    }
}
