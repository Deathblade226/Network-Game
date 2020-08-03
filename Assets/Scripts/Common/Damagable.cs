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
[SerializeField] int score = 0;

private float maxHealth;

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
	if (m_healthBar != null) m_healthBar.value = health;
}

[PunRPC]
public void ApplyDamage(float damageAmount) {
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
