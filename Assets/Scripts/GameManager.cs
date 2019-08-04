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
        /*
        Vector2 location = new Vector2(Random.Range((screen_x[0] - boundary), (screen_x[1] + boundary)), Random.Range((screen_y[0] - boundary), (screen_y[1] + boundary)));
        bool invalid_location = true;
        while(invalid_location)
        {
            //Check if generated location is inside the screen boundaries
            if(location.x >= screen_x[0] &&
               location.x <= screen_x[1] &&
               location.y >= screen_y[0] &&
               location.y <= screen_y[1])
            {
                invalid_location = true;
                location = new Vector2(Random.Range((screen_x[0] - boundary), (screen_x[1] + boundary)), Random.Range((screen_y[0] - boundary), (screen_y[1] + boundary)));
            }
            else
            {
                invalid_location = false;
            }
        }
        */

        /*
        float left_x = Random.Range(screen_x[0] - boundary, screen_x[0]);
        float right_x = Random.Range(screen_x[1], screen_x[1] + boundary);
        */
        
        float lower_y = Random.Range(screen_y[0] - boundary, screen_y[0] - 3);
        float upper_y = Random.Range(screen_y[1] + 3, screen_y[1] + boundary);
        

        float coin_flip = Random.Range(-1,1);

        float x = Random.Range(screen_x[0] - 1, screen_x[1] + 1);
        float y = 0;

        /*
        if (coin_flip < 0) x = left_x;
        else x = right_x;
        */

        
        coin_flip = Random.Range(-1,1);

        if (coin_flip < 0) y = lower_y;
        else y = upper_y;

        Vector2 location = new Vector2(x, y);
        


        /*
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
        */
        
        /* 
        if(Vector2.Distance(new Vector2(Mathf.Abs(location.x),Mathf.Abs(location.y)), new Vector2(0,0)) < game_area.x) {
            if(r_x % 2 == 0) {
                location.x = game_area.x + game_area.x / 2;
            }

            if(r_y % 2 == 0) {
                location.y = game_area.y + game_area.y / 2;
            }
        }
*/

        return location;
    }

    public static Vector2 GetRandomLocationInsideViewableArea() {
        Vector2 location = new Vector2(Random.Range(screen_x[0] + 2, screen_x[1] - 2), Random.Range(screen_y[0] + 2, screen_y[1] - 2));
        return location;
    }
}
