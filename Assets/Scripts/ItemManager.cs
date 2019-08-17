using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

    public GameObject healthpack_prefab;
    public int total_healthpacks = 1;
    public int chance_for_healthpack = 30;
    public int item_spawn_timer = 60;
    private int counter = 0;

    void Update() {
        counter++;
        if(counter >= item_spawn_timer) { 
            counter = 0;
            Debug.Log("Chance to spawn items");
            if(GameObject.FindGameObjectsWithTag("HealthPack").Length < total_healthpacks) {
                int random = Random.Range(0,100);
                if(random <= chance_for_healthpack) {
                    CreateHealthPack();
                }
            }
        }
    }

    private void CreateHealthPack() {
        Vector2 location = GameManager.GetRandomLocationOutsideViewableArea();
        Instantiate(healthpack_prefab, location, Quaternion.identity);
    }
}

