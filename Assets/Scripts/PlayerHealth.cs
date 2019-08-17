using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
    public int buffer_area = 5;
    private Vector2 screen_area = GameManager.GetScreenArea();
    public Vector2 healthbar_location;
    public int healthbar_width = 20;
    public int max_health = 200;
    public int health;
    public Vector2 healthbar;

    void Start() {
        health = max_health;
        healthbar_location = new Vector2(screen_area.x/2 + buffer_area, screen_area.y + buffer_area);
        healthbar = new Vector2(health, healthbar_width);
    }

    private void OnGUI() {
        Texture2D texture = new Texture2D(1,1);
        texture.SetPixel(0,0,Color.red);
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        GUI.Box(new Rect(healthbar_location, healthbar), "");
    }

    public void DecrementHealthBar(int damage) {

        health -= damage;
        if(health <= 0) {
            health = 0;
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>().StopMainBGMLoop();
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreController>().ResetScore();
            SceneManager.LoadScene(sceneName:"GameOver");
        }
        healthbar.x = health;
    }

    public void IncrementHealthBar(int amount) {
        if(health >= max_health) {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreController>().IncrementScore(100);
            return;
        }

        health += amount;
        healthbar.x = health;
    }
}
