using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static Vector2 game_area = new Vector2(30,20);
    public static Vector2 screen_area = new Vector2(game_area.x/4, game_area.y/4);

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

    private void Update() {
        score = GetComponent<ScoreController>().GetCurrentScore();
        GetComponent<EnemyManager>().total_enemies = ((int)Mathf.Sqrt(score/12)) * difficulty_modifier;
        GetComponent<AsteroidManager>().total_asteroids = ((int)Mathf.Sqrt(score/8)) * difficulty_modifier;

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
        Vector2 location = new Vector2(Random.Range(game_area.x, -game_area.x), Random.Range(game_area.y, -game_area.y));
        int r_x = Random.Range(0,100);
        int r_y = Random.Range(0,100);

        if(r_x % 2 == 0) {
            location.x += (game_area.x * 1.5f);
        } else {
            location.x -= (game_area.x * 1.5f);
        }

        if(r_y % 2 == 0) {
            location.y += (game_area.y *1.5f);
        } else {
            location.y -= (game_area.y *1.5f);
        }
        
        if(Vector2.Distance(new Vector2(Mathf.Abs(location.x),Mathf.Abs(location.y)), new Vector2(0,0)) < game_area.x) {
            if(r_x % 2 == 0) {
                location.x = game_area.x + game_area.x / 2;
            }

            if(r_y % 2 == 0) {
                location.y = game_area.y + game_area.y / 2;
            }
        }

        return location;
    }

    public static Vector2 GetRandomLocationInsideViewableArea() {
        Vector2 location = new Vector2(Random.Range(screen_area.x, -screen_area.x), Random.Range(screen_area.y, -screen_area.y));
        return location;
    }
}
