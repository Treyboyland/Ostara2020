using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UiTimer : MonoBehaviour
{
    [SerializeField]
    GameTimer timer = null;

    [SerializeField]
    Animator animator = null;

    TextMeshProUGUI textBox;

    // Start is called before the first frame update
    void Start()
    {
        textBox = GetComponent<TextMeshProUGUI>();
        timer.OnTimeUpdated.AddListener(UpdateTime);
    }

    void UpdateTime(float time)
    {
        animator.SetFloat("Time", time);
        TimeSpan ts = new TimeSpan(0, 0, 0, (int)time, (int)((time % 1) * 1000)); //Dirty, but doesn't need to be totally precise

        if (ts.Hours == 0 && ts.Minutes == 0)
        {
            textBox.text = string.Format("{0:D2}.{1:D3}", ts.Seconds, ts.Milliseconds);
        }
        else if (ts.Hours == 0 && ts.Minutes != 0)
        {
            textBox.text = string.Format("{0:D2}:{1:D2}.{2:D3}", ts.Minutes, ts.Seconds, ts.Milliseconds);
        }
        else
        {
            textBox.text = string.Format("{0:D1}:{1:D2}:{2:D2}.{3:D3}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
        }
    }
}
