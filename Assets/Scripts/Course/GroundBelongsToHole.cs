using UnityEngine;

public class GroundBelongsToHole : MonoBehaviour
{
    [SerializeField]
    [Min(1)]
    private int belongsToHole = 1;

    public int getOwningHole()
    {
        return belongsToHole;
    }
}
