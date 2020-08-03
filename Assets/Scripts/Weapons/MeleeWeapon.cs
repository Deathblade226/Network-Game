using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon {

private bool hit = false;

public override void Attack() { hit = false; }

private void Awake() { Type = "Melee"; }

[PunRPC]
void RPC_PlayerAttack() { 
    GameObject go = null;
    go = AIUtilities.GetNearestGameObject(gameObject, "Player", 1, 360, true);
    if (go != null) { 
    Damagable health = go.GetComponent<Damagable>();    
    if (health != null) health.ApplyDamage(Damage);
    if (DestroyOnHit) Destroy(gameObject);
    }   
}

private void OnCollisionEnter(Collision collision) {
    if (!PV.IsMine) { return; }
    GameObject go = collision.collider.gameObject;
    if (gameObject.tag == "Player") { PV.RPC("RPC_PlayerAttack", RpcTarget.All); }
}

}
