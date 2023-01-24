using System.Collections;
using System.Collections.Generic;
using System.Threading;
using MyBox;
using UnityEngine;

public class FadeEffect : MonoBehaviour
{
    [Separator("Default Settings")] 
    [PositiveValueOnly] [Range(0, 1)] [SerializeField]
    private float fadeDuration = 3f;
    [SerializeField] private SpriteRenderer image;

    public void FadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(FadeTo(0.0f, fadeDuration));
    }
    
    IEnumerator FadeTo(float aValue, float aTime)
    {
        Color oldColor = image.color;
        float alpha = oldColor.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, Mathf.Lerp(alpha,aValue,t));
            image.color = newColor;
            yield return null;
        }
    }
}
