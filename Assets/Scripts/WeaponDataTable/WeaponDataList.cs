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
																						{ "type", 0.75f },
																						{ "damage", 0.75f },
																						{ "fireRate", 0.75f },
																						{ "critChance", 0.75f },
																						{ "accuracy", 0.75f },
																						{ "magSize", 0.75f },
																						{ "reloadSpeed", 0.75f },
																						{ "mesh", 1.0f },
																						{ "weaponImage", 1.0f },
																						{ "ammoImage", 1.0f} };
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
	public Sprite weaponImage;
	public Sprite ammoImage;

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
		weaponImage = null;
		ammoImage = null;
	}

	public WeaponDataEntry(WeaponDataEntry other)
	{
		name = other.name;
		type = other.type;
		damage = other.damage;
		fireRate = other.fireRate;
		critChance = other.critChance;
		accuracy = other.accuracy;
		magSize = other.magSize;
		reloadSpeed = other.reloadSpeed;
		mesh = other.mesh;
		weaponImage = other.weaponImage;
		ammoImage = other.ammoImage;
	}
}