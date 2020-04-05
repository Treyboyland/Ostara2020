using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneOffSoundPool : ObjectPool<OneOffSound>
{
    static OneOffSoundPool _instance;

    public static OneOffSoundPool Instance
    {
        get
        {
            return _instance;
        }
    }
    protected override void Awake()
    {
        base.Awake();
        _instance = this;
    }
}
