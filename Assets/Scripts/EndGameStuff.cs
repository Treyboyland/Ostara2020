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

    [SerializeField]
    AudioClip clip;

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
            if (OneOffSoundPool.Instance != null)
            {
                var oneOff = OneOffSoundPool.Instance.GetObject();
                oneOff.Clip = clip;
                oneOff.Volume = 0.5f;
                oneOff.gameObject.SetActive(true);
            }
        });
        player.OnPlayerWinsGame.AddListener(() =>
        {
            winText.SetActive(true);
            loseText.SetActive(false);
            toActivate.SetActive(true);
        });
    }
}
