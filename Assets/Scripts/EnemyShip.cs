using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour {

    public int health = 10;
    public GameObject bullet_prefab;
    public GameObject explosion_prefab;
    public int bullet_cooldown = 30;
    public Vector2 target_position;

    public enum ShipType {
        BASIC = 0,
        MOVE_AND_SHOOT = 1,
        ALWAYS_ON_THE_MOVE = 2
    };

    public Sprite basic_sprite;
    public Color basic_color;
    public Sprite move_and_shoot_sprite;
    public Color move_and_shoot_color;
    public Sprite always_on_the_move_sprite;
    public Color always_on_color;

    public ShipType ship_type;

    public int speed;

    
    public AudioClip laserSFX_clip;
    public AudioClip explosionSFX_clip;

    private int cooldown_timer = 0;
    private CircleCollider2D _collider;
    private Vector2 screen_area = GameManager.GetScreenArea();
    private SpriteRenderer _sprite_renderer;


    private AudioSource laserSFX_source;
    private AudioSource explosionSFX_source;
    private bool can_shoot = false;

    void Start() {
        _collider = GetComponent<CircleCollider2D>();
        _sprite_renderer = GetComponent<SpriteRenderer>();
        target_position = GameManager.GetRandomLocationInsideViewableArea();

        int random_type = (Random.Range(0, System.Enum.GetValues(typeof(ShipType)).Length));
        switch(random_type) {
            case 0: 
                ship_type = ShipType.BASIC;
                _sprite_renderer.sprite = basic_sprite;
                SetLight(basic_color);
                break;
            case 1:
                ship_type = ShipType.MOVE_AND_SHOOT;
                _sprite_renderer.sprite = move_and_shoot_sprite;
                SetLight(move_and_shoot_color);
                health = 15;
                break;
            case 2:
                ship_type = ShipType.ALWAYS_ON_THE_MOVE;
                _sprite_renderer.sprite = always_on_the_move_sprite;
                SetLight(always_on_color);
                can_shoot = true;
                health = 20;
                break;
            default:
                ship_type = ShipType.BASIC;
                break;
        }

        laserSFX_source = AddAudio(laserSFX_clip, false, false, 0.3f);
        explosionSFX_source = AddAudio(explosionSFX_clip, false, false, 0.5f);
    }

    void Update() {

        if(health <= 0) {
            Instantiate(explosion_prefab, transform.position, transform.rotation);
            //explosionSFX_source.Play();
            GameObject.Destroy(this.gameObject);
        }

        UpdatePosition();

        if(cooldown_timer == 0 && can_shoot) {
            FireBullet();
            cooldown_timer = bullet_cooldown;
        } else if (can_shoot) {
            cooldown_timer--;
        }
    }

    private void UpdatePosition() {
        bool reached_target_position = false;

        if(!reached_target_position && Vector2.Distance(transform.position, target_position) > 0.5) {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target_position, step);
        } else {
            reached_target_position = true;
            can_shoot = true;

            if(ship_type != ShipType.BASIC) {
                this.SetTargetPosition(GameManager.GetRandomLocationInsideViewableArea());
                reached_target_position = false;
            }
        }

    }

    private void FireBullet() {

        // Don't fire a bullet if outside the screen area
        if(Mathf.Abs(transform.position.x) - screen_area.x > 0.5 || Mathf.Abs(transform.position.y) - screen_area.y > 0.5) return;

        Vector2 bullet_target = GameObject.FindGameObjectWithTag("Player").transform.position;
        GameObject bullet = Instantiate(bullet_prefab, transform.position + (transform.forward * 2), transform.rotation);
        bullet.GetComponent<Bullet>().SetTarget(bullet_target);
        health--;
        laserSFX_source.Play();

        
        if(ship_type == ShipType.MOVE_AND_SHOOT) {
            can_shoot = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag.Equals("Shield")) {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreController>().IncrementScore(15);
            health = 0;
        }
        if(other.gameObject.tag.Equals("Player")){
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().DecrementHealthBar(10);
            health = 0;
        }
    }

    private void SetTargetPosition(Vector2 t) {
        target_position = t;
    }

    public ShipType GetShipType()
    {
        return ship_type;
    }

    private void SetLight(Color ship_color)
    {
        Component[] lights = GetComponentsInChildren<Light>();

        foreach(Light lt in lights)
        {
            lt.color = ship_color;
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
