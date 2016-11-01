using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditorInternal;
using System.Collections;
using System.Collections.Generic;

/** references
 * 
 * http://va.lent.in/unity-make-your-lists-functional-with-reorderablelist/
 * 
 * 
 */


public class WeaponDataListWindow : EditorWindow
{
	//References
	static SerializedObject dataList;
	static WeaponDataList dataRef;
	static ReorderableList listDisplay;
	static EditorWindow currentWindow;

	//Functionality
	Vector2 scrollPosTop;

	//Display settings
	static GUIStyle s_tableStyle = new GUIStyle();
	static GUIStyle s_headerStyle = new GUIStyle();
	static Color c_evenColumn = new Color(0.8f, 0.8f, 1.0f);
	static Color c_oddColumn = new Color(0.9f, 0.9f, 1.0f);
	static float cellPad = 1;


	public static void Init(SerializedObject serializedDataList)
	{
		//Create and set current window, set title
		currentWindow = EditorWindow.GetWindow(typeof(WeaponDataListWindow));
		currentWindow.titleContent = new GUIContent("Table Editor");
		//Store reference to target data
		dataRef = (WeaponDataList) serializedDataList.targetObject;
		//Data must be in serialised object form for reorderable lists
		dataList = new SerializedObject(dataRef);
		//Create reorderable list
		listDisplay = new ReorderableList(dataList, dataList.FindProperty("weaponList"), true, true, true, true);

		#region Styles
		s_tableStyle.margin = new RectOffset(6, 6, 4, 4);
        s_headerStyle = new GUIStyle(EditorStyles.toolbarButton);
		s_headerStyle.alignment = TextAnchor.MiddleCenter;
		s_headerStyle.fontSize = 10;
		#endregion Styles

		#region Delegates
		listDisplay.drawElementCallback = DrawElementCallback;
		listDisplay.drawHeaderCallback = DrawHeaderCallback;
		#endregion Delegates
	}

	void OnGUI()
	{
		//dataList = new SerializedObject(dataRef);
		dataList.Update();

		scrollPosTop = GUILayout.BeginScrollView(scrollPosTop, GUILayout.Height(currentWindow.position.height / 2));
		GUILayout.BeginVertical(s_tableStyle);
		
		listDisplay.DoLayoutList();

		GUILayout.EndVertical();
		GUILayout.EndScrollView();

		dataList.ApplyModifiedProperties();
	}

	#region ReorderableList Methods
	static void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
	{
		if (index == 0)	Debug.Log("Element: " + rect);
		//Gets element currently being rendered by list
		SerializedProperty element = listDisplay.serializedProperty.GetArrayElementAtIndex(index);
		//Get list of displayed properties and their column widths
		Dictionary<string, int> data = WeaponDataList.properties;
		List<string> keys = new List<string>(data.Keys);
		float columnWidth = rect.width / data.Count; //temp fixed column width
		rect.y += 2;

		for (int i = 0; i < data.Count; i += 1)
		{
			Rect position = new Rect(rect.x + cellPad, rect.y, columnWidth - (cellPad*2), EditorGUIUtility.singleLineHeight);
			rect.x += columnWidth;
			SerializedProperty property = element.FindPropertyRelative(keys[i]);
            EditorGUI.PropertyField(position, property, GUIContent.none);
		}
	}

	static void DrawHeaderCallback(Rect rect)
	{
		Debug.Log("Header: " + rect);
		//Offset due to dragable icon
		float startOffset = 14f;
		rect.x += startOffset;
		rect.width -= startOffset;
		//Get list of displayed properties and their column widths
		Dictionary<string, int> data = WeaponDataList.properties;
		List<string> keys = new List<string>(data.Keys);
		float columnWidth = rect.width / data.Count; //temp fixed column width

		for (int i = 0; i < data.Count; i += 1)
		{
			GUI.backgroundColor = (i % 2 == 0) ? c_evenColumn : c_oddColumn;
			Rect position = new Rect(rect.x, rect.y, columnWidth, EditorGUIUtility.singleLineHeight);
			rect.x += columnWidth;
			EditorGUI.LabelField(position, keys[i], s_headerStyle);
		}
		GUI.backgroundColor = Color.white;
	}
	#endregion ReorderableList Methods
}
