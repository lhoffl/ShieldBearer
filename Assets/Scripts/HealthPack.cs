using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour {
    
    public int health_boost = 15;
    public float speed = 0.75f;

    private Vector2 target_position;
    private bool reached_target_position = false;
    private int targets = 0;

    private void Start() {
        target_position = GameManager.GetRandomLocationInsideViewableArea();
    }

    private void Update() {
        UpdatePosition();
    }

    private void UpdatePosition() {

        if(!reached_target_position && Vector2.Distance(transform.position, target_position) > 0.5) {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target_position, step);
        } else if(!reached_target_position) {
            reached_target_position = true;
            targets++;
            if(targets >= 2) {
                Destroy(this.gameObject);
            }
        } else {
            reached_target_position = false;
            target_position = GameManager.GetRandomLocationOutsideViewableArea();
        }
    }

    public int HealAmount() {
        return health_boost;
    }

    void OnTriggerEnter2D(Collider2D collider) {

        if(collider.tag.Equals("Player")) {
            collider.gameObject.GetComponent<PlayerHealth>().IncrementHealthBar(health_boost);
            GameObject.Destroy(this.gameObject);
        }

        if(collider.tag.Equals("Shield") || collider.tag.Equals("Enemy") || collider.tag.Equals("Bullet")) {
            GameObject.Destroy(this.gameObject);
        } else if (collider.tag.Equals("Asteroid")) {
            if (collider.gameObject.GetComponent<Asteroid>().IsDeflected()) {
                collider.gameObject.GetComponent<PlayerHealth>().IncrementHealthBar(health_boost * 3);
            } else {
                GameObject.Destroy(this.gameObject);
            }
        }
    }
}
