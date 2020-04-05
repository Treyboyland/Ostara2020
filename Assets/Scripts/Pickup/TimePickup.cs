using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePickup : Pickup
{
    [SerializeField]
    protected float timeAdded;

    protected static GameTimer timer;

    public Vector3Event OnPickupAtLocation = new Vector3Event();

    protected void Start()
    {
        if (timer == null)
        {
            timer = Object.FindObjectOfType<GameTimer>();
        }
    }

    protected override bool CheckCollision(Collider2D other)
    {
        return other.GetComponent<Player>() != null;
    }

    protected override void CompleteAction(Collider2D other)
    {
        timer.OnAddTime.Invoke(timeAdded);
        OnPickupAtLocation.Invoke(transform.position);
        gameObject.SetActive(false);
    }

}
