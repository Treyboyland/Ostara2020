using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameStuff : MonoBehaviour
{
    [SerializeField]
    GameObject toActivate;

    [SerializeField]
    GameTimer timer;

    // Start is called before the first frame update
    void Start()
    {
        toActivate.SetActive(false);
        timer.OnEndTimeReached.AddListener(() => toActivate.SetActive(true));
    }
}
