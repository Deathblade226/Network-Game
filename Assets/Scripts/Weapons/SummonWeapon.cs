using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonWeapon : Weapon {

[SerializeField] GameObject summonMonster = null;
[SerializeField] int minSummons = 1;
[SerializeField] int maxSummons = 1;

private List<GameObject> Summons = new List<GameObject>();
private bool canSummon = true;

void Awake() { Type = "Summon"; }

public void Attack() { canSummon = (Summons.Count <= maxSummons); }

void Update() {
    GameObject go = null;
    if (attack.Target != "") go = AIUtilities.GetNearestGameObject(gameObject, attack.Target, 0, attack.Nc.Fov, attack.Nc.SeeThroughWalls);
    if (go != null && canSummon) { 
    canSummon = false;
    
    int summon = Random.Range(minSummons, maxSummons-Summons.Count);

    for (int i = 0; i < summon; i++) { 
    
    } 

    }         
}

}
