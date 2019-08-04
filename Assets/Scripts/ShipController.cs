using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float duration = 1f;
    public float range_mod = 50;

    public AudioClip shipSFX_clip;

    private AudioSource shipSFX_source;
    

    private float amplitude = 0;

    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        shipSFX_source = AddAudio(shipSFX_clip, true, true, 0.2f);
        shipSFX_source.Play();

    }

    // Update is called once per frame
    void Update()
    {
        // NO
        //if ((Input.GetKey("up"))) changePos(0, 0.1f);
        //if (Input.GetKey("down")) changePos(0,-0.1f);

        ListAimlessly();

        changePos(0, (amplitude/range_mod));

        if(count == 60) {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreController>().IncrementScore(1);
            count = 0;
        } else {
            count++;
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
