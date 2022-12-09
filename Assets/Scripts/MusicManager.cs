using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance = null;

    [SerializeField] AudioSource Music;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else  if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Play(AudioClip clip)
    {
        Music.clip = clip;
        Music.Play();
    }

    public void Pause()
    {
        Music.Pause();
    }

    public void UnPause()
    {
        Music.UnPause();
    }
}
