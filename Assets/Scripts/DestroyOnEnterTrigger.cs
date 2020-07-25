using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnEnterTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        //TODO: Determine Destructable property...As class?
        var wall = other.gameObject.GetComponent<Wall>();
        var enemy = other.gameObject.GetComponent<Enemy>();
        if (wall != null)
        {
            if (wall.IsDestructable)
            {
                wall.gameObject.SetActive(false);
            }
        }

        if (enemy != null)
        {
            //TODO: Determine if vampires should also die...
            enemy.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //TODO: Determine Destructable property...As class?
        var wall = other.gameObject.GetComponent<Wall>();
        var enemy = other.gameObject.GetComponent<Enemy>();
        if (wall != null)
        {
            if (wall.IsDestructable)
            {
                wall.gameObject.SetActive(false);
            }
        }

        if (enemy != null)
        {
            //TODO: Determine if vampires should also die...
            enemy.gameObject.SetActive(false);
        }
    }
}
