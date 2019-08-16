using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public int speed = 8;
    
    public GameObject explosion_prefab;
    
    private AudioSource explosionSFX_source;
    public int damage = 10;
    private Vector2 target;

    private BoxCollider2D _collider;
    private bool is_reflected = false;

    void Start() {
        _collider = GetComponent<BoxCollider2D>();
    }

    void Update() {

        CheckCollision();

        if(Mathf.Abs(Vector2.Distance(GameManager.screen_area, transform.position)) >= 222) {
            Debug.Log(Vector2.Distance(GameManager.screen_area, transform.position));
            Destroy(this.gameObject);
        }
    
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target, step);
    }

    public void SetTarget(Vector2 t) {
        target = t;
    }

    private void CheckCollision() {
        Vector3 max = _collider.bounds.max;
        Vector3 min = _collider.bounds.min;

        Vector2 corner1 = new Vector2(max.x, min.y - 0.1f);
        Vector2 corner2 = new Vector2(min.x, min.y - 0.2f);

        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        if(hit != null) {
            if(hit.tag.Equals("Player")) {
                PlayerHealth player = hit.GetComponent<PlayerHealth>();
                player.DecrementHealthBar(damage);
                Debug.Log("Player damaged");
                Instantiate(explosion_prefab, transform.position, transform.rotation);
                GameObject.Destroy(this.gameObject);
            }          

            if(hit.GetComponent<EnemyShip>() != null) {
                EnemyShip enemy = hit.GetComponent<EnemyShip>();
                if(is_reflected) {
                    Instantiate(explosion_prefab, transform.position, transform.rotation);
                    GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreController>().IncrementScore(30);
                    GameObject.Destroy(enemy.gameObject);
                }
            }

            if(hit.tag.Equals("Shield")) {
                is_reflected = true;
                speed *= -2;
            }
        }
    }

}
