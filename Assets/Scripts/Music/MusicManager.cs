using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public static MusicManager _instance;

    [Header("背景音效")]
    public AudioClip BackgroundMusic;
    public AudioClip BarMusic;

    AudioSource bgMusic;
    AudioSource barMusic;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        bgMusic = gameObject.AddComponent<AudioSource>();
        barMusic = gameObject.AddComponent<AudioSource>();

        DontDestroyOnLoad(gameObject);

        //BgMusic();
    }

    public void BgMusic()
    {
        _instance.bgMusic.clip= BackgroundMusic;
        _instance.bgMusic.loop=true;
        _instance.bgMusic.Play();

    }

    public void BrMusic()
    {
        _instance.barMusic.clip = BarMusic;
        _instance.barMusic.loop = true;
        _instance.barMusic.Play();
    }
}
