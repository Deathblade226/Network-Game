using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonWeapon : Weapon {

[SerializeField] GameObject summonMonster = null;
[SerializeField] int minSummons = 1;
[SerializeField] int maxSummons = 1;

private List<GameObject> Summons = new List<GameObject>();

void Awake() { Type = "Summon"; }

public override void Attack() {
    if (Summons.Count < maxSummons) {
    int summon = Random.Range(minSummons, maxSummons-Summons.Count);
    
    for (int i = 0; i < summon; i++) { 
    GameObject sum = GameObject.Instantiate(summonMonster);
    sum.transform.position = new Vector3(transform.position.x, transform.transform.position.y, transform.position.z);
    sum.transform.position += transform.forward * 5;
    Summons.Add(sum);
    }

    } else {
    
    Vector3 after = transform.forward * -5;

    attack.Nc.Agent.SetDestination(after);

    }
}

}
