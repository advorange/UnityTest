using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	public string Name;
	public float Damage = 10.0f;
	public int FramesPerAttack = 5;
	public Transform Player;

	protected abstract void TriggerWeapon();
}