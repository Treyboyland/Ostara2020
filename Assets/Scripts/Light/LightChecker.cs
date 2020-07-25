using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChecker : MonoBehaviour
{
    [SerializeField]
    Player player = null;

    [SerializeField]
    PlayerSpeedModifier modifier = null;

    [SerializeField]
    LightPool lightPool = null;

    // Start is called before the first frame update
    void Start()
    {
        player.OnAttemptLightCast.AddListener(CheckLight);
    }

    void CheckLight(Player player)
    {
        if (player.LightAmount == 0 || modifier.IsActive) //Light can only be cast one at a time
        {
            return;
        }

        var light = lightPool.GetObject();
        if(LightBlast.SpeedModifier == null)
        {
            LightBlast.SpeedModifier = modifier;
        }

        light.transform.position = player.transform.position;

        light.gameObject.SetActive(true);
    }
}
