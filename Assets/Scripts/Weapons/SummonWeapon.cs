using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonWeapon : Weapon {

[SerializeField] GameObject summonMonster = null;
[SerializeField] int minSummons = 1;
[SerializeField] int maxSummons = 1;
[SerializeField] float minRange = 1;
[SerializeField] float maxRange = 2;

private List<GameObject> Summons = new List<GameObject>();

void Awake() { Type = "Summon"; }

public override void Attack() {
    if (Summons.Count < maxSummons) { 
    int summon = Random.Range(minSummons, maxSummons-Summons.Count);
    Debug.Log(summon);
    Debug.Log(summonMonster);
    for (int i = 0; i < summon; i++) { 
    GameObject sum = GameObject.Instantiate(summonMonster);
    sum.transform.position = new Vector3(transform.position.x + Random.Range(minRange, maxRange), transform.transform.position.y, transform.position.z + Random.Range(minRange, maxRange));
    Summons.Add(sum);
    }

    }
}

}
