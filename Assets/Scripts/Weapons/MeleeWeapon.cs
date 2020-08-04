using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon {

private bool hit = false;

public override void Attack() { hit = false; }

private void Awake() { Type = "Melee"; }

void PlayerAttack(GameObject go) { 
    if (go != null && go.tag == "Player") { 
    Damagable health = go.GetComponent<Damagable>();    
    if (health != null) {
    health.ApplyDamage(Damage);
    
    }
    if (DestroyOnHit) Destroy(gameObject);
    }   
}

private void OnTriggerEnter(Collider other) {
    GameObject go = other.gameObject;
    GameObject[] objects = new GameObject[1];
    objects[0] = go;
    if (go.tag != "Weapon") { PlayerAttack(go); }
}

}
