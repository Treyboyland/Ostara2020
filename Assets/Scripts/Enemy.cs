﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float secondsToStun = 0;

    [SerializeField]
    protected bool isImmortal = false;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            player.OnDamagePlayer.Invoke(secondsToStun);
            if (!isImmortal)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
