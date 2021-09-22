using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmControllerScript : MonoBehaviour
{

    [SerializeField] private float _maxVolume = 0.6f;
    [SerializeField] private float _feedSpeed = 0.006f;
    [SerializeField] private float _bossModeToPitch = 2.0f;

    private AudioSource _mAudioSource;
    private BGM_PlayMode _mPlayMode;

    public enum BGM_PlayMode
    {
        PlayMode_FadeIn,
        PlayMode_FadeOut,
        PlayMode_Normal,
        PlayMode_Boss,
    }

    // Start is called before the first frame update
    void Start()
    {
        _mAudioSource = GetComponent<AudioSource>();
        _mAudioSource.volume = 0.0f;

        _mPlayMode = BGM_PlayMode.PlayMode_FadeIn;
    }

    public void SetPlayMode(BGM_PlayMode playMode)
    {
        if (_mPlayMode == playMode) return;

        _mPlayMode = playMode;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_mPlayMode)
        {
            case BGM_PlayMode.PlayMode_FadeIn:
                FadeIn();
                break;
            case BGM_PlayMode.PlayMode_FadeOut:
                FadeOut();
                break;
            case BGM_PlayMode.PlayMode_Normal:
                break;

            case BGM_PlayMode.PlayMode_Boss:
                PlayBossMode();
                break;

        }
    }

    private void PlayBossMode()
    {
        _mAudioSource.pitch = Mathf.Lerp(_mAudioSource.pitch, _bossModeToPitch * 1.1f, Time.time * _feedSpeed);
    }

    private void FadeOut()
    {

    }

    void FadeIn()
    {
        _mAudioSource.volume = Mathf.Lerp(_mAudioSource.volume, _maxVolume * 1.1f, Time.time * _feedSpeed);

        if (_mAudioSource.volume >= _maxVolume)
        {
            _mAudioSource.volume = _maxVolume;
            _mPlayMode = BGM_PlayMode.PlayMode_Normal;
            return;
        }
    }
}
