using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    public Vector2 speed = new Vector2(1.0f, 1.0f);

    public float max_speed = 5.0f;
    public float min_speed = -5.0f;
    public int min_damage = 20;
    public float rotation_speed = 1.0f;

    public float probability = 0.5f;
    public float distance_to_remove = 30.0f;
    
    public Vector2 target_position;

    public float max_size = 2.5f;

    public float min_size = 0.7f;

    public Color new_color;
    
    public AudioClip hitSFX_clip;

    public GameObject explosion_prefab;

    private AudioSource hitSFX_source;
    private Rigidbody2D _body;
    private PolygonCollider2D _collider;
    private SpriteRenderer _SprRd;
    private bool is_deflected = false;

    void Start() {
        _body = GetComponent<Rigidbody2D>();
        _collider = GetComponent<PolygonCollider2D>();
        _SprRd = GetComponent<SpriteRenderer>();
        RandomizeLook();

        speed.x = Random.Range(min_speed, max_speed);
        speed.y = Random.Range(min_speed, max_speed);

        if(Random.value <= probability) {
            Vector2 direction = target_position - (Vector2)transform.position;
            speed = new Vector2(Mathf.Abs(speed.x), Mathf.Abs(speed.y));
            speed = speed * direction.normalized;
        }

        _body.rotation = rotation_speed;
        _body.velocity = speed;

        hitSFX_source = AddAudio(hitSFX_clip, false, false, 0.5f);
    }

    void Update() {
        if((Mathf.Abs(transform.position.x) >= distance_to_remove) || (Mathf.Abs(transform.position.y) >= distance_to_remove)) {
            Destroy(this.gameObject);
        }

        CheckCollision();

        _body.rotation += rotation_speed;
    }

    private void CheckCollision() {
        Vector3 max = _collider.bounds.max;
        Vector3 min = _collider.bounds.min;

        Vector2 corner1 = new Vector2(max.x, min.y - 0.1f);
        Vector2 corner2 = new Vector2(min.x, min.y - 0.2f);

        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        if(hit != null) { 

            if(hit.GetComponent<Asteroid>() != null) {
                Asteroid other = hit.GetComponent<Asteroid>();
                other.speed *= -1;
                this.speed *= -1;
            }

            if(hit.tag.Equals("Shield")) {
                Debug.Log("Asteroid deflected");
                is_deflected = true;
                //hitSFX_source.PlayOneShot(hitSFX_clip, 0.2f);
            }

            if(hit.GetComponent<EnemyShip>() != null) {
                EnemyShip enemy = hit.GetComponent<EnemyShip>();
                Instantiate(explosion_prefab, enemy.transform.position, enemy.transform.rotation);
            
                GameObject.Destroy(enemy.gameObject);
                //hitSFX_source.PlayOneShot(hitSFX_clip, 0.2f);

                if(is_deflected) {
                    GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreController>().IncrementScore(150);
                    is_deflected = false;
                }
            }
        }
    }

    public bool IsDeflected() {
        return is_deflected;
    }

    void RandomizeLook()
    {
        //Size
        float x_size = Random.Range(min_size, max_size);
        float y_size = Random.Range(min_size, max_size);
        if((Mathf.Abs(x_size - y_size) > 0.5f))
        {
            x_size = y_size - (Random.Range(0.2f, 0.5f));
        }

        transform.localScale = new Vector3(x_size, y_size, 1);


        //Color
        float m_red, m_blue, m_green;
        m_red = Random.Range(0.8f, 1);
        m_blue = Random.Range(0.8f, 1);
        m_green = Random.Range(0.8f, 1);
        new_color = new Color(m_red, m_green, m_blue);
        //new_color = Color.red;
        _SprRd.color = new_color;
    }

    void OnCollisionEnter(Collision other) 
    {
        // how much the character should be knocked back
        var magnitude = 5000;
        // calculate force vector
        var force = transform.position - other.transform.position;
        // normalize force vector to get direction only and trim magnitude
        force.Normalize();
        _body.AddForce(force * magnitude);
        
    }

    void OnTriggerEnter2D(Collider2D collider) 
    {
        hitSFX_source.Play();
        
        Debug.Log("Asteroid hit");

        if(collider.tag.Equals("Player")) {

            Debug.Log("Asteroid hit the player");
            Instantiate(explosion_prefab, transform.position, transform.rotation);
            int damage = Mathf.RoundToInt((this.GetComponent<Rigidbody2D>().mass * 10) * Mathf.Abs(speed.x) * Mathf.Abs(speed.y));
            damage += min_damage;
            Debug.Log(damage);
            collider.gameObject.GetComponent<PlayerHealth>().DecrementHealthBar(damage);
            GameObject.Destroy(this.gameObject);
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
}
