using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

[SerializeField] float damage = 0.0f;
[SerializeField] bool destroyOnHit = false;
public AttackNav attack { get; set; }
public string Type = "Weapon";
public float Damage { get => damage; set => damage = value; }
public bool DestroyOnHit { get => destroyOnHit; set => destroyOnHit = value; }
}

