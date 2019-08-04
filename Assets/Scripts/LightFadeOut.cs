using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFadeOut : MonoBehaviour
{
    
    public float min_intensity = 0;
    public float max_intensity = 1;
    private Light lt;

    private float t;
    // Start is called before the first frame update
    void Start()
    {
        lt = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime; 
        lt.intensity = Mathf.Lerp(max_intensity, min_intensity, t / 1);
    }
}
