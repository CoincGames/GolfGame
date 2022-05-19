using UnityEngine;

public class HoleTrigger : MonoBehaviour
{
    public int LayerOnEnter;
    public int LayerOnExit;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.layer = LayerOnEnter;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.layer = LayerOnExit;
        }
    }
}
