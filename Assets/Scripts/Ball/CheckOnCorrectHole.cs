using UnityEngine;

public class CheckOnCorrectHole : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject golfball;

    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody>();
        golfball = rb.gameObject;
    }

    private void FixedUpdate()
    {
        if (rb.IsSleeping() && !onCorrectGround())
        {
            MouseSwing mouseSwing = golfball.GetComponentInParent<MouseSwing>();
            GameManager.instance.sendToPosition(mouseSwing.getResetPosition(), golfball);
            print("Resetting: " + mouseSwing.getResetPosition());
        }
    }

    private bool onCorrectGround()
    {
        RaycastHit hitGround;
        Ray ray = new Ray(golfball.transform.position, Vector3.down);

        Physics.Raycast(ray, out hitGround);

        // If the collider straight below us is not a course floor, you are not on the right ground
        if (!hitGround.collider.CompareTag("CourseFloor"))
        {
            return false;
        }

        GroundBelongsToHole groundScript = hitGround.collider.gameObject.GetComponent<GroundBelongsToHole>();
        int currentHole = groundScript.getOwningHole();

        return GameManager.instance.getCurrentHole() == currentHole;
    }
}
