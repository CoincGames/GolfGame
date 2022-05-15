using UnityEngine;

public class Pickup : MonoBehaviour
{
    public void PickupPowerup()
    {
        Destroy(gameObject);
    }
}