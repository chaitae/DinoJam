using UnityEngine;
using TMPro;

public class PointsText : MonoBehaviour
{
    public TextMeshProUGUI text;
    [Space]
    public float timeOnScreen;
    public AnimationCurve textAlpha;
    public AnimationCurve xPosition;
    public AnimationCurve yPosition;
    [Space]
    public float xMultiplier;
    public float yMultiplier;

    private float timer;
    private Color startColor;
    private Vector3 startPosition;

    public void Awake()
    {
        timer = 0;
        startColor = text.color;
        startPosition = transform.position;
    }

    public void FixedUpdate()
    {
        timer += Time.deltaTime;
        float ratio = timer / timeOnScreen;
        if(ratio > 1)
        {
            Destroy(gameObject);
            return;
        }
        startColor.a = textAlpha.Evaluate(ratio);
        text.color = startColor;
        Vector3 position = startPosition;
        position.x += xPosition.Evaluate(ratio) * xMultiplier;
        position.y += yPosition.Evaluate(ratio) * yMultiplier;
        transform.position = position;
    }
}
