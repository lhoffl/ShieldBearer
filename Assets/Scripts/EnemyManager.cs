using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public GameObject enemy_prefab;
    public int total_enemies = 8;

    void Start() {
        for(int i = 0; i < total_enemies; i++) {
            CreateEnemy();
        }
    }

    void Update() {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length < total_enemies) {
            CreateEnemy();
        }
    }

    private void CreateEnemy() {
        Vector2 location = GameManager.GetRandomLocationOutsideViewableArea();
        Instantiate(enemy_prefab, location, Quaternion.identity);
    }
}
