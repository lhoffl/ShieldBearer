using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public float degree = 90;
    public float radius = 1;
    public float min_speed = 0;
    public float max_speed = 1f;
    public float friction = 0.025f;
    public float accel_speed = 0.1f;
    public float rotate_speed = 0;
    public bool clockwise = true;
    
    private Vector3 shieldPosition;
    private CapsuleCollider2D _collider;
    private GameObject turn_signal;

    // Start is called before the first frame update
    void Start()
    {
        turn_signal = GameObject.FindGameObjectWithTag("TurnSignal");
        UpdatePosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")) clockwise = !clockwise;

        if (Input.GetKey("space")) UpdateSpeed(accel_speed);

        if (Input.GetKeyUp("space")) {
            turn_signal.GetComponent<SpriteRenderer>().flipY = !clockwise;
            if(clockwise) {
                turn_signal.transform.localPosition = new Vector3(0, -0.18f, 0);
            } else {
                turn_signal.transform.localPosition = new Vector3(0, 0.21f, 0);
            }
        }
        UpdatePosition();
        UpdateSpeed(0);
    }

    void UpdatePosition()
    {
        shieldPosition = new Vector3((radius * Mathf.Cos(degree * 0.0174532925f)), (radius * Mathf.Sin(degree * 0.0174532925f)), 0);
        transform.localPosition = shieldPosition;
        transform.localRotation = Quaternion.Euler(0, 0, (degree));
    }

    void UpdateSpeed(float speed_change)
    {
        rotate_speed = rotate_speed + speed_change - friction;
        if (rotate_speed > max_speed) rotate_speed = max_speed;
        if (rotate_speed < min_speed) rotate_speed = min_speed;

        if (clockwise) degree = (degree + rotate_speed) % 360;
        else degree = (degree - rotate_speed) % 360;
    }

}