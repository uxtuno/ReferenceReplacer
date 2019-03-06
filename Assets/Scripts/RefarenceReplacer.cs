using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RefarenceReplacer : EditorWindow
{
	[MenuItem("Window/Reference Replacer")]
	static void Open()
	{
		var window = CreateInstance<RefarenceReplacer>();
		window.Show();
	}

	Object targetObject;
	Object replaceObject;

	void OnGUI()
	{
		targetObject = EditorGUILayout.ObjectField("Target Object", targetObject, typeof(Object), true);	
		replaceObject = EditorGUILayout.ObjectField("Replace Object", replaceObject, typeof(Object), true);

		if (GUILayout.Button("Replace")) {
			foreach (var gameObject in FindObjectsOfType<GameObject>()) {
				foreach (var component in gameObject.GetComponents<MonoBehaviour>()) {
					var serializedObject = new SerializedObject(component);
					var iterator = serializedObject.GetIterator();
					while(iterator.Next(true)) {
						if (iterator.propertyType == SerializedPropertyType.ObjectReference) {
							if (iterator.objectReferenceValue == targetObject) {
								iterator.objectReferenceValue = replaceObject;
							}
						}
					}
					
					serializedObject.ApplyModifiedProperties();
				}
			}
		}
	}
}
