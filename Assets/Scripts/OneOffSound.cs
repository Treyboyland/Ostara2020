using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneOffSound : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;

    public AudioClip Clip { get; set; }

    public float Volume { get; set; }


    private void OnEnable()
    {
        StartCoroutine(PlayThenDisable());
    }

    IEnumerator PlayThenDisable()
    {
        audioSource.clip = Clip;
        audioSource.volume = Volume;
        audioSource.Play();
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
