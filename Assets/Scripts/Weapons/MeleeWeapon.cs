using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon {

private bool hit = false;

public override void Attack() { hit = false; }

private void Awake() { Type = "Melee"; }
private void Update() {
    GameObject go = null;
    if (attack.Target != "") go = AIUtilities.GetNearestGameObject(gameObject, attack.Target, 0, attack.Nc.Fov, attack.Nc.SeeThroughWalls);
    if (go != null && !hit) { 
    hit = true;
    Damagable health = go.GetComponent<Damagable>();    
    if (health != null) health.ApplyDamage(Damage);
    if (DestroyOnHit) Destroy(gameObject);
    }   
}

}
