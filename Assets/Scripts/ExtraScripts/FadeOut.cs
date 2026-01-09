using UnityEngine;
using System.Collections;

public class FadeAndDestroy2D : MonoBehaviour
{
    public float duration = 2f; // Время исчезновения

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
            StartCoroutine(FadeOut());
        }
        else
        {
            Debug.LogError("Компонент SpriteRenderer не найден");
        }
    }

    public AnimationCurve fadeCurve = AnimationCurve.Linear(0, 1, 1, 0); // по умолчанию линейное

    IEnumerator FadeOut()
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float curveValue = fadeCurve.Evaluate(t); // значение по кривой [0..1]
            float alpha = Mathf.Lerp(originalColor.a, 0f, curveValue);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        Destroy(gameObject);
    }
}