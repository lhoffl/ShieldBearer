using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static Vector2 game_area = new Vector2(30,20);
    public static Vector2 screen_area = new Vector2(game_area.x/4, game_area.y/4);

    public static float[] screen_x = {-18, 18};   //Left Bound, Right Bound
    public static float[] screen_y = {-5, 5};  //Lower Bound, Upper Bound

    public static float boundary = 10;

    private Camera cam;
    void Start() {
       GetScreenBounds();
    }
    public static GameManager instance = null;

    private int score = 0;
    private int difficulty_modifier = 2;

    private void Awake() {
        if(instance == null) {
            instance = this;
        } else if (instance != null) {
            GameObject.Destroy(this.gameObject);
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    void GetScreenBounds()
    {
        cam = Camera.main;

        var vertExtent = cam.orthographicSize;
        var horzExtent = vertExtent * Screen.width / Screen.height;


        screen_area = new Vector2(horzExtent, vertExtent);
        game_area = new Vector2(horzExtent + boundary, vertExtent + boundary);

        
        screen_x[0] = (screen_area.x) * -1;
        screen_x[1] = (screen_area.x);
        screen_y[0] = (screen_area.y) * -1;
        screen_y[1] = (screen_area.y);
    }

    private void Update() {
        score = GetComponent<ScoreController>().GetCurrentScore();
        GetComponent<EnemyManager>().total_enemies = (((int)Mathf.Sqrt(score/20)) * difficulty_modifier);
        GetComponent<AsteroidManager>().total_asteroids = ((int)Mathf.Sqrt(score/12)) * difficulty_modifier;

        if(score <= 100) {
            GetComponent<AsteroidManager>().total_asteroids = 10;
        }

        if(score >= 700) {
            difficulty_modifier = 3;
        }
    }


    public static Vector2 GetGameArea() {
        return game_area;
    }

    public static Vector2 GetScreenArea() {
        return screen_area;
    }

    public static Vector2 GetRandomLocationOutsideViewableArea() {
        
        float lower_y = Random.Range(screen_y[0] - boundary, screen_y[0] - 3);
        float upper_y = Random.Range(screen_y[1] + 3, screen_y[1] + boundary);
        

        float coin_flip = Random.Range(-1,1);

        float x = Random.Range(screen_x[0] - 1, screen_x[1] + 1);
        float y = 0;
        
        coin_flip = Random.Range(-1,1);

        if (coin_flip < 0) y = lower_y;
        else y = upper_y;

        Vector2 location = new Vector2(x, y);

        return location;
    }

    public static Vector2 GetRandomLocationInsideViewableArea() {
        Vector2 location = new Vector2(Random.Range(screen_x[0] + 2, screen_x[1] - 2), Random.Range(screen_y[0] + 2, screen_y[1] - 2));
        return location;
    }
}
