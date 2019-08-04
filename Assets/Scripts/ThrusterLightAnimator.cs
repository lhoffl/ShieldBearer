using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterLightAnimator : MonoBehaviour
{

    private Light lt;
    public float duration = 0.86f;

    public float intensity_change = 5f;

    public float max_intensity = 10f;

    // Start is called before the first frame update
    void Start()
    {
        lt = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        PulseLight();
    }

    void PulseLight()
    {
        float phi = Time.time / duration * 2 * Mathf.PI;
        float amplitude = Mathf.Sin(phi) * intensity_change + max_intensity;
        lt.intensity = amplitude;
    }
}
