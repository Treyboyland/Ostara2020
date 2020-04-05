using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameStuff : MonoBehaviour
{
    [SerializeField]
    GameObject toActivate;

    [SerializeField]
    GameObject winText;

    [SerializeField]
    GameObject loseText;

    [SerializeField]
    Player player;

    [SerializeField]
    GameTimer timer;

    // Start is called before the first frame update
    void Start()
    {
        toActivate.SetActive(false);
        timer.OnEndTimeReached.AddListener(() =>
        {
            winText.SetActive(false);
            loseText.SetActive(true);
            toActivate.SetActive(true);
            player.IsGameOver = true;
        });
        player.OnPlayerWinsGame.AddListener(() =>
        {
            winText.SetActive(true);
            loseText.SetActive(false);
            toActivate.SetActive(true);
        });
    }
}
