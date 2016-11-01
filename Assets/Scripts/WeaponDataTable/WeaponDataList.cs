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
	public static Dictionary<String, int> properties = new Dictionary<string, int>{	{ "name", 300 },
																					{ "type", 150 },
																					{ "damage", 100 },
																					{ "fireRate", 100 },
																					{ "critChance", 100 },
																					{ "accuracy", 100 },
																					{ "magSize", 100 },
																					{ "reloadSpeed", 100 },
																					{ "mesh", 300 } };
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