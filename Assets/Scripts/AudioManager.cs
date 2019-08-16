using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public AudioClip startBGM_clip;
    public AudioClip loopBGM_clip;
    public AudioClip stopBGM_clip;

    public AudioClip loopMenu_clip;
    public AudioClip stopMenu_clip;

    public AudioClip bonusStart_clip;
    public AudioClip bonusLoop_clip;
    public AudioClip bonusEnd_clip;

    private AudioSource startBGM_source;
    private AudioSource loopBGM_source;
    private AudioSource stopBGM_source;

    private AudioSource bonusStart_source;
    private AudioSource bonusLoop_source;
    private AudioSource bonusEnd_source;

    private AudioSource loopMenu_source;
    private AudioSource stopMenu_source;

    private bool play_main_loop = false;
    private bool play_bonus_loop = false;

    void Start() {
        startBGM_source = AddAudio(startBGM_clip, false, true, 0.8f);
        loopBGM_source = AddAudio(loopBGM_clip, true, true, 0.8f);
        stopBGM_source = AddAudio(stopBGM_clip, false, true, 0.8f);

        loopMenu_source = AddAudio(loopMenu_clip, true, true, 0.8f);
        stopMenu_source = AddAudio(stopMenu_clip, false, true, 0.8f);

        bonusStart_source = AddAudio(bonusStart_clip, false, true, 0.5f);
        bonusLoop_source = AddAudio(bonusLoop_clip, true, true, 0.8f);
        bonusEnd_source = AddAudio(bonusEnd_clip, false, true, 0.5f);

        if(!loopMenu_source.isPlaying) {
            loopMenu_source.Play();
        }
    }

    public AudioSource AddAudio(AudioClip clip, bool loop, bool play_awake, float vol) {
        AudioSource new_audio = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        new_audio.clip = clip;
        new_audio.loop = loop;
        new_audio.playOnAwake = play_awake;
        new_audio.volume = vol;

        return new_audio;
    }

    void Update() {
        if(loopMenu_source.isPlaying) {
            StopMainBGMLoop();
        }
        if((play_main_loop || play_bonus_loop ) && 
            !startBGM_source.isPlaying && !bonusStart_source.isPlaying && !loopBGM_source.isPlaying && !bonusLoop_source.isPlaying) {
                if(play_main_loop) loopBGM_source.Play();
                if(play_bonus_loop) bonusLoop_source.Play();
        }
    }

    public void StartMainBGMLoop() {
        play_main_loop = true;
        loopMenu_source.Stop();
        stopMenu_source.Play();
        startBGM_source.Play();
    }

    public void RestartMenuLoop() {
        if(!loopMenu_source.isPlaying) {
            loopMenu_source.Play();
        }
    }

    public void StopMainBGMLoop() {
        if(play_main_loop) {
            startBGM_source.Stop();
            loopBGM_source.Stop();
            stopBGM_source.Play();
            play_main_loop = false;
        }

        if(play_bonus_loop) { 
            bonusStart_source.Stop();
            bonusLoop_source.Stop();
            bonusEnd_source.Play();
            play_bonus_loop = false;
        }
    }

    public void StartBonusLoop() {
        play_main_loop = false;
        play_bonus_loop = true;
        loopMenu_source.Stop();
        stopMenu_source.Play();
        bonusStart_source.Play();
    }

}
