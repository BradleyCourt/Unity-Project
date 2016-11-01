using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class CustomMenuItems
{
	[MenuItem("Assets/Create/DataTables/WeaponData")]
	public static WeaponDataList CreateWeaponDataTable()
	{
		WeaponDataList asset = ScriptableObject.CreateInstance<WeaponDataList>();
		string filePath;

		//Get appropriate filepath
		if (AssetDatabase.IsValidFolder("Assets/DataTables"))
		{
			filePath = "Assets/DataTables/WeaponDataTable.asset";
		}
		else
		{
			filePath = "Assets/WeaponDataTable.asset";
		}

		if (File.Exists(filePath))
		{
			if (EditorUtility.DisplayDialog("Warning!", "Asset already exists, would you like to overwrite?", "Yes", "No"))
			{
				AssetDatabase.CreateAsset(asset, filePath);
			}
		}
		else
		{
			AssetDatabase.CreateAsset(asset, filePath);
		}
		AssetDatabase.SaveAssets();
		return asset;
	}
}

[CustomEditor(typeof(WeaponDataList))]
public class WeaponDataListEditor : Editor
{
	Vector2 scrollPos;
	WeaponDataList dataList;
	SerializedObject editorTarget;

	public void OnEnable()
	{
		editorTarget = serializedObject;
	}

	public override void OnInspectorGUI()
	{
		dataList = (WeaponDataList)target;
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Add Test Data"))
		{
			dataList.weaponList.Add(new WeaponDataEntry("gun" + dataList.weaponList.Count));
		}
		if (GUILayout.Button("Edit Data Table"))
		{
			WeaponDataListWindow.Init(editorTarget);
		}
		GUILayout.EndHorizontal();
		scrollPos = GUILayout.BeginScrollView(scrollPos);
		for (int i = 0; i < dataList.weaponList.Count; i += 1)
		{
			GUILayout.BeginHorizontal();
			GUILayout.Label(dataList.weaponList[i].name);
			GUILayout.Label(dataList.weaponList[i].type.ToString());
			GUILayout.EndHorizontal();
		}
		GUILayout.EndScrollView();
	}
}

