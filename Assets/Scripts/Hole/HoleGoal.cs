using UnityEngine;
using System.Collections;

public class HoleGoal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(MoveToNextHole(other.gameObject));
        }
    }

    IEnumerator MoveToNextHole(GameObject gameObject)
    {
        yield return new WaitForSeconds(GameManager.instance.getTimeBetweenGoalAndNextHole());

        GameManager.instance.finishHole(gameObject);
    }
}
