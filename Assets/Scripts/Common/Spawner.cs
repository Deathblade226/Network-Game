using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

[SerializeField] GameObject spawn = null;
[SerializeField] float spawnTimmer = 5f;

private GameObject Spawned = null;
private float timmer = 0;

void Start() {
    if (spawn == null) { Debug.Log("There is no monster to spawn."); }
    else {
    Spawned = GameObject.Instantiate(spawn, transform.position + transform.up, Quaternion.identity);
    timmer = spawnTimmer;
    }
}

void Update() {
    if (timmer <= 0 && Spawned == null) { 
    Spawned = GameObject.Instantiate(spawn, transform.position + transform.up, Quaternion.identity);
    timmer = spawnTimmer;
    } else if (timmer > 0 && Spawned == null) {
    timmer -= Time.deltaTime;
    }  
}

}
