using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire : MonoBehaviour
{
    [SerializeField]
    float secondsToTake;

    [SerializeField]
    bool isImmortal;

    static GameTimer timer;

    public NullEvent OnAttackSuccessful = new NullEvent();

    bool FindTimer()
    {
        timer = timer == null ? Object.FindObjectOfType<GameTimer>() : timer;
        return timer != null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            if (!player.IsInvincible && FindTimer())
            {
                timer.OnSubtractTime.Invoke(secondsToTake);
                player.OnMakePlayerInvincible.Invoke(false);
                OnAttackSuccessful.Invoke();
            }
            if (!isImmortal)
            {
                gameObject.SetActive(false);
            }
        }
    }

    
}
