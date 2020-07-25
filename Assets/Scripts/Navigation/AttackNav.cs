using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNav : MonoBehaviour {

[SerializeField] float attackCD = 3.0f;
[SerializeField] float attackRange = 2.0f;
[SerializeField] NavigationController nc = null;
[SerializeField] Weapon weapon = null;
[SerializeField] bool lookForTowers = false;

public string Target { get; set; } = "";
public bool Active { get; set; } = false;
public NavigationController Nc { get => nc; set => nc = value; }
public bool Attacking { get => attacking; }

private float AttackTime = 0;
private bool attacking = false;
private GameObject tower = null;

private void Start() {
	weapon.attack = this;
}
private void Update() {
	if (lookForTowers) tower = AIUtilities.GetNearestGameObject(gameObject, "Defence", attackRange);

	if (tower != null) { Target = tower.tag; Active = true; }
	else {Target = ""; Active = false; Nc.Agent.isStopped = false; }

	if (Target != "" && Active) { 

	var target = AIUtilities.GetNearestGameObject(gameObject, Target, Nc.Range, Nc.Fov, Nc.SeeThroughWalls);

	if (target != null) {

	attacking = ((transform.position - target.transform.position).magnitude <= attackRange && AttackTime <= 0);
	
	if (attacking) {
	if (weapon.Type == "Melee") ((MeleeWeapon)weapon).Attack();
	if (Nc.Animator != null) Nc.Animator.SetTrigger("Attack");  

	transform.LookAt(target.transform);
	AttackTime = attackCD; Nc.Agent.isStopped = true; }

	else if ((transform.position - target.transform.position).magnitude <= attackRange) { Nc.Agent.isStopped = true; AttackTime -= Time.deltaTime; }

	else { Nc.Animator.SetTrigger("StopAttack"); Nc.Agent.SetDestination(target.transform.position); Nc.Agent.isStopped = false; AttackTime -= Time.deltaTime; }

	}
	}        
}

public void StartAttacking() { 
	Active = true;
}

public void StopAttacking() { 
	Active = false;
	Target = "";
}

}
