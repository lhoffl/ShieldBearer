using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float duration = 1f;
    public float range_mod = 50;
    public float speed = 0.035f;
    public int moving_cooldown = 40;

    public AudioClip shipSFX_clip;

    private AudioSource shipSFX_source;
    
    private GameObject shield;

    private float amplitude = 0;
    private bool moving = false;
    private int time_count = 0;
    private int shield_count = 0;

    // Start is called before the first frame update
    void Start()
    {
        shield = GameObject.FindGameObjectWithTag("Shield");
        shipSFX_source = AddAudio(shipSFX_clip, true, true, 0.2f);
        shipSFX_source.Play();

    }

    // Update is called once per frame
    void Update()
    {
        // FINE
        if ((Input.GetKey("w"))) {
            changePos(0, speed);
            moving = true;
        }
        if (Input.GetKey("s")) {
            changePos(0,-speed);
            moving = true;
        }
        if (Input.GetKey("a")) {
            changePos(-speed,0);
            moving = true;
        }
        if (Input.GetKey("d")) {
            changePos(speed,0);
            moving = true;
        }

        if ((Input.GetKeyUp("w")) || Input.GetKeyUp("s") || Input.GetKeyUp("a") || Input.GetKeyUp("d")) {
            moving = false;
        }

        // Disable the shield when moving
        if(moving) {
            shield.SetActive(false);
            shield_count = 0;
        } else if(!moving && moving_cooldown != shield_count) {
            shield_count++;
        } else if(!moving && moving_cooldown == shield_count) {
            shield.SetActive(true);
        }

        //ListAimlessly();

        changePos(0, (amplitude/range_mod));

        if(time_count == 60) {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreController>().IncrementScore(1);
            time_count = 0;
        } else {
            time_count++;
        }

        /* 
        if (goingUp) changePos(0, (amplitude/2));
        else changePos(0,-range);
        */
    }

        
    void changePos(float x, float y)
    {
        transform.position += new Vector3(x, y, 0);
    }

    void ListAimlessly()
    {
        float phi = Time.time / duration * 2;// * Mathf.PI;
        amplitude = Mathf.Sin(phi);
        changePos(0, (amplitude/range_mod));
    }

    public AudioSource AddAudio(AudioClip clip, bool loop, bool play_awake, float vol) {
        AudioSource new_audio = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        new_audio.clip = clip;
        new_audio.loop = loop;
        new_audio.playOnAwake = play_awake;
        new_audio.volume = vol;

        return new_audio;
    }
}
