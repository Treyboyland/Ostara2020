using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOverTime : MonoBehaviour
{
    [SerializeField]
    AnimationCurve curve = null;

    [SerializeField]
    float secondsOfTime = 0;

    private void OnEnable()
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(Animate());
        }
    }


    IEnumerator Animate()
    {
        float time = 0;
        Vector3 currentScale = transform.localScale;

        while (time < secondsOfTime)
        {
            time += Time.deltaTime;
            float scale = curve.Evaluate(time / secondsOfTime);
            transform.localScale = currentScale * scale;
            yield return null;
        }
    }
}
