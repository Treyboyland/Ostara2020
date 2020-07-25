using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedModifier : MonoBehaviour
{
    [SerializeField]
    float multiplier = 0.0f;

    [SerializeField]
    float secondsToMaintain = 0.0f;

    [SerializeField]
    Player player = null;

    float elapsedTime = 0.0f;


    bool trackTime = false;

    /// <summary>
    /// True if the boost is currently active
    /// </summary>
    /// <value></value>
    public bool IsActive
    {
        get
        {
            return trackTime;
        }
    }

    public NullEvent OnBoostPlayerSpeed = new NullEvent();

    // Start is called before the first frame update
    void Start()
    {
        OnBoostPlayerSpeed.AddListener(() =>
        {
            if (trackTime)
            {
                elapsedTime = 0.0f;
            }
            else
            {
                trackTime = true;
                player.Speed *= multiplier;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (trackTime)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= secondsToMaintain)
            {
                elapsedTime = 0.0f;
                trackTime = false;
                player.ReturnToNormalSpeed();
            }
        }
    }
}
