using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField]
    float initialTime = 0;



    public FloatEvent OnAddTime = new FloatEvent();
    public FloatEvent OnSubtractTime = new FloatEvent();

    public NullEvent OnEndTimeReached = new NullEvent();

    public FloatEvent OnTimeUpdated = new FloatEvent();

    public FloatEvent OnSetNewMultiplier = new FloatEvent();

    public NullEvent OnStopTime = new NullEvent();

    public NullEvent OnStartTime = new NullEvent();

    float timeRemaining = 0;

    [SerializeField]
    float timeMultiplier = 1.0f;

    bool timeStopped = false;

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
            OnTimeUpdated.Invoke(timeRemaining);
        });

        OnAddTime.AddListener((toAdd) =>
        {
            timeRemaining += toAdd;
            OnTimeUpdated.Invoke(timeRemaining);
        });

        OnSetNewMultiplier.AddListener((mult) =>
        {
            timeMultiplier = mult;
        });

        OnStopTime.AddListener(() => timeStopped = true);
        OnStartTime.AddListener(() => timeStopped = false);

        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        while (timeRemaining != 0)
        {
            if (!timeStopped)
            {
                timeRemaining = Mathf.Max(0, timeRemaining - Time.deltaTime * timeMultiplier);
                OnTimeUpdated.Invoke(timeRemaining);
            }
            //Debug.LogWarning(timeRemaining);
            yield return null;
        }
        //Debug.LogWarning("Done: " + timeRemaining);

        OnTimeUpdated.Invoke(timeRemaining);
        OnEndTimeReached.Invoke();
    }
}
