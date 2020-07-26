using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonWeapon : Weapon {

[SerializeField] GameObject summonMonster = null;
[SerializeField] int minSummons = 1;
[SerializeField] int maxSummons = 1;

private bool canSummon = false;
private List<GameObject> Summons = new List<GameObject>();

void Awake() { Type = "Summon"; }

public override void Attack() { canSummon = true; }

private void Update() {
    if (Summons.Count < maxSummons && canSummon) {
    canSummon = false;
    int summon = Random.Range(minSummons, maxSummons-Summons.Count);
    GameObject sum = GameObject.Instantiate(summonMonster, new Vector3(transform.position.x, transform.transform.position.y, transform.position.z) + transform.forward * 5, transform.rotation);
    //sum.transform.position = new Vector3(transform.position.x, transform.transform.position.y, transform.position.z);
    //sum.transform.position += transform.forward * 5;
    Summons.Add(sum);
    }    
}

}
