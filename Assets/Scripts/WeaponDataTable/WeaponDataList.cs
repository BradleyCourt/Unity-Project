using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum WeaponType
{
	Handgun, Shotgun, Assault, Rifle
}

[Serializable]
public class WeaponDataList : ScriptableObject
{
	public List<WeaponDataEntry> weaponList = new List<WeaponDataEntry>();
	public static Dictionary<String, float> properties = new Dictionary<string, float>{	{ "name", 1.0f },
																						{ "type", 0.5f },
																						{ "damage", 0.5f },
																						{ "fireRate", 0.5f },
																						{ "critChance", 0.5f },
																						{ "accuracy", 0.5f },
																						{ "magSize", 0.5f },
																						{ "reloadSpeed", 0.5f },
																						{ "mesh", 1.0f } };
}

[Serializable]
public struct WeaponDataEntry
{
	public string name;
	public WeaponType type;
	public int damage;
	public float fireRate;
	public float critChance;
	public float accuracy;
	public int magSize;
	public float reloadSpeed;
	public GameObject mesh;

	public WeaponDataEntry(string wName)
	{
		name = wName;
		type = WeaponType.Handgun;
		damage = 0;
		fireRate = 0f;
		critChance = 0f;
		accuracy = 100f;
		magSize = 10;
		reloadSpeed = 0;
		mesh = null;
	}

	public WeaponDataEntry(string wName, WeaponType wType, int wDamage, float wFireRate, float wCritChance, float wAccuracy, int wMagSize, float wReloadSpeed, GameObject wMesh)
	{
		name = wName;
		type = wType;
		damage = wDamage;
		fireRate = wFireRate;
		critChance = wCritChance;
		accuracy = wAccuracy;
		magSize = wMagSize;
		reloadSpeed = wReloadSpeed;
		mesh = wMesh;
	}
}