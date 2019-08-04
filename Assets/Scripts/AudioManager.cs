using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public AudioClip startBGM_clip;
    public AudioClip loopBGM_clip;
    public AudioClip stopBGM_clip;

    public AudioClip loopMenu_clip;
    public AudioClip stopMenu_clip;

    private AudioSource startBGM_source;
    private AudioSource loopBGM_source;
    private AudioSource stopBGM_source;

    private AudioSource loopMenu_source;
    private AudioSource stopMenu_source;

    private bool play_main_loop = false;

    void Start() {
        startBGM_source = AddAudio(startBGM_clip, false, true, 0.8f);
        loopBGM_source = AddAudio(loopBGM_clip, true, true, 0.8f);
        stopBGM_source = AddAudio(stopBGM_clip, false, true, 0.8f);

        loopMenu_source = AddAudio(loopMenu_clip, true, true, 0.8f);
        stopMenu_source = AddAudio(stopMenu_clip, false, true, 0.8f);

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
        if(play_main_loop && !loopMenu_source.isPlaying && !startBGM_source.isPlaying && !loopBGM_source.isPlaying) {
            loopBGM_source.Play();
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
        play_main_loop = false;
        loopBGM_source.Stop();
        stopBGM_source.Play();
    }

}
