using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireMovement : MonoBehaviour
{
    [SerializeField]
    Vampire vampire;

    [SerializeField]
    float speed;

    [SerializeField]
    float secondsToWait;

    [SerializeField]
    float secondsToWaitPursue;

    [SerializeField]
    float secondsToPursue;

    [SerializeField]
    Rigidbody2D rb2d;

    bool listenerAdded = false;

    static Player player;

    bool shouldPursue = false;

    Vector3 playerPos;
    Vector3 startPos;

    float elapsed = 0;

    bool ShouldPursue
    {
        get
        {
            return shouldPursue;
        }
        set
        {
            if (value == true && FindPlayer())
            {
                playerPos = player.transform.position;
                startPos = transform.position;
                shouldPursue = value;
            }
            else
            {
                elapsed = 0;
                shouldPursue = false;
            }
        }
    }

    bool FindPlayer()
    {
        player = player == null ? Object.FindObjectOfType<Player>() : player;
        return player != null;
    }

    private void OnEnable()
    {
        elapsed = 0;
        if (!listenerAdded)
        {
            listenerAdded = true;
            vampire.OnAttackSuccessful.AddListener(() =>
            {
                StopAllCoroutines();
                StartCoroutine(Wait());
            });
        }
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(TrackPlayer());
        }
    }

    private void FixedUpdate()
    {
        if (ShouldPursue)
        {
            elapsed += Time.fixedDeltaTime;
            //rb2d.MovePosition(Vector3.Lerp(transform.position, playerPos, elapsed / secondsToPursue));
            rb2d.velocity = (playerPos - startPos).normalized * speed;
        }
        else
        {
            rb2d.velocity = new Vector2();
        }
    }

    IEnumerator Wait()
    {
        ShouldPursue = false;
        yield return new WaitForSeconds(secondsToWait);
        StartCoroutine(TrackPlayer());
    }

    IEnumerator TrackPlayer()
    {
        while (!FindPlayer())
        {
            yield return new WaitForSeconds(0.5f);
        }

        while (true)
        {
            yield return new WaitForSeconds(secondsToWaitPursue);
            ShouldPursue = true;
            yield return new WaitForSeconds(secondsToPursue);
            ShouldPursue = false;
        }
    }
}
