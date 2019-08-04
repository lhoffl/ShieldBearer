using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour {

    public GameObject asteroid_prefab;
    public int total_asteroids = 15;

    void Start() {
        for(int i = 0; i < total_asteroids; i++) {
            CreateAsteroid();
        }
    }

    void Update() {
        if(GameObject.FindGameObjectsWithTag("Asteroid").Length < total_asteroids) {
            CreateAsteroid();
        }
    }

    private void CreateAsteroid() {
        Vector2 location = GameManager.GetRandomLocationOutsideViewableArea();
        Instantiate(asteroid_prefab, location, Quaternion.identity);
    }
}
