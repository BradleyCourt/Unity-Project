using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditorInternal;
using System.Collections;
using System.Collections.Generic;
using System.Text;

/** references
 * http://va.lent.in/unity-make-your-lists-functional-with-reorderablelist/
 */


public class WeaponDataListWindow : EditorWindow
{
	//References
	static SerializedObject dataList; 
	static WeaponDataList dataRef;
	static ReorderableList listDisplay;
	static EditorWindow currentWindow;
	static WeaponDataEntry currentEntry;

	//Functionality
	Vector2 scrollPosTop;
	static Editor imageViewer;
	static Editor weaponViewer;
	static Sprite imagePreview;
	static GameObject weaponPreview;

	//Display settings
	static GUIStyle s_tableStyle = new GUIStyle();
	static GUIStyle s_headerStyle = new GUIStyle();
	static GUIStyle s_previewStyle = new GUIStyle();
	static Color c_evenColumn = new Color(0.8f, 0.8f, 1.0f);
	static Color c_oddColumn = new Color(0.9f, 0.9f, 1.0f);
	static Color c_previewBackground = new Color(0.7f, 0.7f, 0.95f);
	static float cellPad = 1;


	public static void Init(SerializedObject serializedDataList)
	{
		//Create and set current window, set title
		currentWindow = EditorWindow.GetWindow(typeof(WeaponDataListWindow));
		currentWindow.titleContent = new GUIContent("Table Editor");
		currentWindow.minSize = new Vector2(600, 500);
		//Store reference to target data
		dataRef = (WeaponDataList) serializedDataList.targetObject;
		//Data must be in serialised object form for reorderable lists
		dataList = new SerializedObject(dataRef);
		//Create reorderable list
		listDisplay = new ReorderableList(dataList, dataList.FindProperty("weaponList"), true, true, false, false);

		#region Styles
		s_tableStyle.margin = new RectOffset(6, 6, 4, 4);
        s_headerStyle = new GUIStyle(EditorStyles.toolbarButton);
		s_headerStyle.alignment = TextAnchor.MiddleCenter;
		s_headerStyle.fontSize = 10;
		Texture2D prevBack = new Texture2D(1, 1);
		prevBack.SetPixel(0, 0, c_previewBackground);
		s_previewStyle.normal.background = prevBack;
		s_previewStyle.active.background = prevBack;
		#endregion Styles

		#region Delegates
		listDisplay.drawElementCallback = DrawElementCallback;
		listDisplay.drawHeaderCallback = DrawHeaderCallback;
		listDisplay.onSelectCallback = SelectEntryCallback;
		#endregion Delegates
	}

	void OnGUI()
	{
		//dataList = new SerializedObject(dataRef);
		dataList.Update();

		GUILayout.BeginVertical(s_tableStyle, GUILayout.Height(currentWindow.position.height / 2));
		//Layout list management button along top
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("Add", GUILayout.MaxWidth(100)))		{ AddListEntry(); }
		if (GUILayout.Button("Remove", GUILayout.MaxWidth(100)))	{ RemoveListEntry(listDisplay.index); }
		if (GUILayout.Button("Clone", GUILayout.MaxWidth(100)))		{ CloneListEntry(listDisplay.index); }
		GUILayout.EndHorizontal();

		//Fill remaining space with scroll view displaying the table
		scrollPosTop = GUILayout.BeginScrollView(scrollPosTop);
		listDisplay.DoLayoutList();
		GUILayout.EndScrollView();
		GUILayout.EndVertical();

		//Detailed editor for individual entries
		GUILayout.BeginHorizontal();// GUILayout.MaxHeight(currentWindow.position.height / 2));
		float prevSize = currentWindow.position.height / 4; //preview areas must be square, size dictated by height of window
		//Name, Type, and Visual Settings
		GUILayout.BeginVertical(GUILayout.Width((currentWindow.position.width - prevSize) / 2));
		GUILayout.FlexibleSpace();
		GUILayout.EndVertical();
		//Combat Settings
		GUILayout.BeginVertical(GUILayout.Width((currentWindow.position.width - prevSize) / 2));
		GUILayout.FlexibleSpace();
		GUILayout.EndVertical();
		//Preview Windows
		GUILayout.BeginVertical();
		if (imageViewer != null && weaponViewer != null) //Returning null, needs fixing
		{
			Debug.Log("Drawded itz!");
			imageViewer.OnPreviewGUI(GUILayoutUtility.GetRect(prevSize, prevSize), s_previewStyle);
			weaponViewer.OnPreviewGUI(GUILayoutUtility.GetRect(prevSize, prevSize), s_previewStyle);
		}
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();

		dataList.ApplyModifiedProperties();
	}

	#region List Editing Methods
	void AddListEntry()
	{

		WeaponDataEntry newEntry = new WeaponDataEntry(dataRef.weaponList[dataRef.weaponList.Count - 1]);
		dataRef.weaponList.Add(newEntry);
	}

	void RemoveListEntry(int index)
	{
		if (index < 0 || index >= dataRef.weaponList.Count) { return; }

		WeaponDataEntry oldEntry = dataRef.weaponList[index];
		dataRef.weaponList.Remove(oldEntry);
	}

	void CloneListEntry(int index)
	{
		if (index < 0 || index >= dataRef.weaponList.Count) { return; }

		WeaponDataEntry newEntry = new WeaponDataEntry(dataRef.weaponList[index]);
		dataRef.weaponList.Add(newEntry);
	}
	#endregion List Editing Methods

	#region Formatting and Style Methods
	static string FormatPropertyName(string propertyName)
	{
		//Create string builder to allow for editing
		StringBuilder formattedName = new StringBuilder(propertyName);
		//Convert underscored names to formatted names
		formattedName.Replace('_', ' ');
		//Convert camelCase names to formatted names
		for (int i = 1; i < formattedName.Length; i += 1)
		{
			if (char.IsUpper(formattedName[i]) && (formattedName[i - 1] != ' '))
			{
				formattedName.Insert(i, ' ');
			}
		}
		//Convert first letter to captial
		formattedName[0] = char.ToUpper(formattedName[0]);

		return formattedName.ToString();
	}

	static void ColumnWeightToSize(ref Dictionary<string, float> properties, float totalWidth)
	{
		List<string> keys = new List<string>(properties.Keys);

		float totalWeight = 0;
		for (int i = 0; i < keys.Count; i += 1)
		{
			totalWeight += properties[keys[i]];
		}

		for (int i = 0; i < keys.Count; i += 1)
		{
			float portionOfWidth = properties[keys[i]] / totalWeight;
			properties[keys[i]] = totalWidth * portionOfWidth;
		}
	}

	#endregion Formatting and Style Methods

	#region ReorderableList Methods
	static void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
	{
		//Gets element currently being rendered by list
		SerializedProperty element = listDisplay.serializedProperty.GetArrayElementAtIndex(index);
		//Get list of displayed properties and their column weighting, and a list of keys
		Dictionary<string, float> properties = WeaponDataList.properties;
		List<string> keys = new List<string>(properties.Keys);
		//convert column weighting to widths, and add space before current row
		ColumnWeightToSize(ref properties, rect.width);
		rect.y += 2;

		//Loop through properties and display them
		for (int i = 0; i < properties.Count; i += 1)
		{
			Rect position = new Rect(rect.x + cellPad, rect.y, properties[keys[i]] - (cellPad*2), EditorGUIUtility.singleLineHeight);
			rect.x += properties[keys[i]];
			SerializedProperty property = element.FindPropertyRelative(keys[i]);
            EditorGUI.PropertyField(position, property, GUIContent.none);
		}
	}

	static void DrawHeaderCallback(Rect rect)
	{
		//Offset due to dragable icon
		float startOffset = 14f;
		rect.x += startOffset;
		rect.width -= startOffset;
		//Get list of displayed properties and their column weighting, and a list of keys
		Dictionary<string, float> properties = WeaponDataList.properties;
		List<string> keys = new List<string>(properties.Keys);
		//Covert column weighting to widths
		ColumnWeightToSize(ref properties, rect.width);

		//Loop through properties and display them
		for (int i = 0; i < properties.Count; i += 1)
		{
			GUI.backgroundColor = (i % 2 == 0) ? c_evenColumn : c_oddColumn;
			Rect position = new Rect(rect.x, rect.y, properties[keys[i]], EditorGUIUtility.singleLineHeight);
			rect.x += properties[keys[i]];
			EditorGUI.LabelField(position, FormatPropertyName(keys[i]), s_headerStyle);
		}
		GUI.backgroundColor = Color.white;
	}

	static void SelectEntryCallback(ReorderableList list)
	{
		currentEntry = dataRef.weaponList[listDisplay.index];
		imagePreview = currentEntry.weaponImage;
		weaponPreview = currentEntry.mesh;
		imageViewer = Editor.CreateEditor(imagePreview);
		weaponViewer = Editor.CreateEditor(weaponViewer);
	}
	#endregion ReorderableList Methods
}
