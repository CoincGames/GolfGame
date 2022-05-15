using UnityEngine;

public class CycleColors : MonoBehaviour
{
    [SerializeField]
    private GameObject targetGameObject;
    private Renderer meshRenderer;

    [SerializeField]
    private Color[] colorsToCycleThrough;
    [SerializeField]
    [Min(.05f)]
    private float colorChangeRateInSeconds = 1f;
    private Color startColor;
    private Color endColor;
    private int currentEndIndex;
    private float startTime = 0f;

    private void Awake()
    {
        meshRenderer = targetGameObject.GetComponent<Renderer>();

        if (colorsToCycleThrough.Length == 0)
        {
            colorsToCycleThrough = new Color[1];
            colorsToCycleThrough[0] = new Color(0, 0, 0, 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (colorsToCycleThrough.Length == 1)
        {
            meshRenderer.sharedMaterial.color = colorsToCycleThrough[0];
            return;
        }

        startColor = colorsToCycleThrough[0];
        endColor = colorsToCycleThrough[1];
        currentEndIndex = 1;
    }

    // Update is called once per frame
    void Update()
    {
        lerpColors();
    }

    private void lerpColors()
    {
        if (startTime < colorChangeRateInSeconds)
        {
            meshRenderer.material.color = Color.Lerp(startColor, endColor, startTime / colorChangeRateInSeconds);
            startTime += Time.deltaTime;
            return;
        }

        startTime = 0;
        startColor = endColor;

        currentEndIndex++;
        if (currentEndIndex >= colorsToCycleThrough.Length)
        {
            currentEndIndex = 0;
        }
        endColor = colorsToCycleThrough[currentEndIndex];
    }
}