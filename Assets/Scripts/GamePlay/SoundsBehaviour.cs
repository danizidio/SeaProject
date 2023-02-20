using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsBehaviour : MonoBehaviour
{
    public delegate void _onPlayingSound(AudioClip sfx);
    public static _onPlayingSound OnPlayingSound;

    AudioSource _audio;

    [SerializeField] bool isBGM;

    [SerializeField] AudioClip[] _sounds;

    private void Awake()
    {
        _audio = this.GetComponent<AudioSource>();
    }

    void Start()
    {
        if(isBGM)
        {
            int v = Random.Range(0, _sounds.Length);

            _audio.clip = _sounds[v];

            _audio.Play();
        }
    }

    void PlaySound(AudioClip sfx)
    {
        _audio.clip = sfx;
        _audio.Play();
    }

    private void OnEnable()
    {
        if(!isBGM) OnPlayingSound += PlaySound;
    }
    private void OnDisable()
    {
        OnPlayingSound -= PlaySound;
    }
}
