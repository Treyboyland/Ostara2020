using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPickup : TimePickup
{
    protected override void CompleteAction(Collider2D other)
    {
        if (OneOffSoundPool.Instance != null)
        {
            var sound = OneOffSoundPool.Instance.GetObject();
            sound.Clip = clip;
            sound.Volume = 1.0f;
            sound.gameObject.SetActive(true);
        }
        other.GetComponent<Player>().OnPlayerWinsGame.Invoke();
        timer.OnAddTime.Invoke(timeAdded);
        timer.OnStopTime.Invoke();
        OnPickupAtLocation.Invoke(transform.position);
        //gameObject.SetActive(false);
    }
}
