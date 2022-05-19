using UnityEngine;

public class BombPowerup : Pickup
{
    private void OnTriggerEnter(Collider other)
    {
        print("Here");
        // Do custom bomb logic here


        // Call parent pickup method
        base.PickupPowerup();
    }
}
