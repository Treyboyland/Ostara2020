using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField]
    float initialTime;



    public FloatEvent OnAddTime = new FloatEvent();
    public FloatEvent OnSubtractTime = new FloatEvent();

    public NullEvent OnEndTimeReached = new NullEvent();

    public FloatEvent OnTimeUpdated = new FloatEvent();

    public FloatEvent OnSetNewMultiplier = new FloatEvent();

    float timeRemaining = 0;

    [SerializeField]
    float timeMultiplier = 1.0f;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        timeRemaining = initialTime;
        OnSubtractTime.AddListener((toSubtract) =>
        {
            timeRemaining = Mathf.Max(timeRemaining - toSubtract, 0);
        });

        OnAddTime.AddListener((toAdd) =>
        {
            timeRemaining += toAdd;
        });

        OnSetNewMultiplier.AddListener((mult) =>
        {
            timeMultiplier = mult;
        });

        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        while (timeRemaining != 0)
        {
            timeRemaining = Mathf.Max(0, timeRemaining - Time.deltaTime * timeMultiplier);
            OnTimeUpdated.Invoke(timeRemaining);
            //Debug.LogWarning(timeRemaining);
            yield return null;
        }
        //Debug.LogWarning("Done: " + timeRemaining);

        OnTimeUpdated.Invoke(timeRemaining);
        OnEndTimeReached.Invoke();
    }
}
