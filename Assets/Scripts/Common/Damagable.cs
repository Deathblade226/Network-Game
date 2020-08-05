using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damagable : MonoBehaviour {

[SerializeField] float m_health = 100;
[SerializeField] Damage m_damage = null;
[SerializeField] Slider m_healthBar = null;
[SerializeField] [Range(-1,1)]float m_damageReduction = 0;
[SerializeField] float m_regenAmount = 0;
[SerializeField] float m_regenCd = 0;
[SerializeField] bool m_constantRegen = false;
[SerializeField] float m_damageCd = 0;
[SerializeField] PhotonView PV = null;
[SerializeField] int score = 0;

private float maxHealth;
private float damageCd;
private float regenCd;

public float MaxHealth { get => maxHealth; set => maxHealth = value; }
public float health { get => m_health; set => m_health = value; }
public bool destroyed { get; set; } = false;
public float DamageReduction { get => m_damageReduction; set => m_damageReduction = value; }
private void Start() { MaxHealth = health; 
	if (m_healthBar != null) { 
	m_healthBar.maxValue = health;
	m_healthBar.value = health;
	}
}

private void Update() {
	if (m_healthBar != null) { m_healthBar.value = health; }
	if (damageCd > 0) { damageCd -= Time.deltaTime; }
	if (damageCd <= 0 && regenCd > 0) { regenCd -= Time.deltaTime; }
	else { regenCd = m_regenCd; }
	PV.RPC("RegenHealth", RpcTarget.All);
}

[PunRPC]
public void ApplyDamage(float damageAmount) {
	if (damageCd <= 0) {
	damageCd = m_damageCd;
	health = health - (damageAmount - (damageAmount*DamageReduction));
	if (!destroyed && health <= 0) {
	//Game.game.Currency += score;
	if (m_damage != null) {
	Damage damage = Instantiate(m_damage, transform.position, Quaternion.identity);
	damage.Spawn(transform.position, Vector3.zero, Vector3.up);
	}
	Destroy(gameObject);
	destroyed = true;
	}
	}
}

[PunRPC]
public void RegenHealth() {
	if (m_constantRegen || (damageCd <= 0 && regenCd <= 0)) {
	if (health + m_regenAmount <= maxHealth) { health += m_regenAmount; }
	else if (health + m_regenAmount > maxHealth && health < maxHealth) { health = maxHealth; }
	}
}

public void RunRPCMethod(float damage) { 
	PV.RPC("ApplyDamage", RpcTarget.All, damage);

}

}
