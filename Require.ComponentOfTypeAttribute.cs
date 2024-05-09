using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.AttributeUsage(System.AttributeTargets.Field)]
public partial class Require_ComponentOfTypeAttribute : PropertyAttribute {
	public readonly System.Type type;

	public Require_ComponentOfTypeAttribute(System.Type type) {
		this.type = type;
	}
}

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(Require_ComponentOfTypeAttribute))]
public class Require_ComponentOfType_Drawer : PropertyDrawer {
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		Require_ComponentOfTypeAttribute attribute = (Require_ComponentOfTypeAttribute)base.attribute;

		if (property.isArray && property.propertyType == SerializedPropertyType.ObjectReference) {
			EditorGUI.BeginProperty(position, label, property);
			EditorGUI.PropertyField(position, property, label, true);

			for (int i = 0; i < property.arraySize; i++) {
				SerializedProperty element = property.GetArrayElementAtIndex(i);
				ValidateAndAssignComponent(element, attribute);
			}

			EditorGUI.EndProperty();
		}
		else {
			ValidateAndAssignComponent(property, attribute);
			EditorGUI.PropertyField(position, property, label, true);
		}
	}

	private void ValidateAndAssignComponent(SerializedProperty property, Require_ComponentOfTypeAttribute attribute) {
		if (property.propertyType == SerializedPropertyType.ObjectReference) {
			GameObject obj = property.objectReferenceValue as GameObject;
			obj = (property.objectReferenceValue as Transform)?.gameObject;

			if (obj != null) {
				Component foundComponent = obj.GetComponent(attribute.type);

				if (foundComponent != null) {
					Debug.Log("Component of required type found on assign gameObject.");
					property.objectReferenceValue = foundComponent;
				}
				else {
					Debug.Log("Component of required type not found on assign gameObject.");

					property.objectReferenceValue = null;
				}
			}
		}
		else if (attribute.type != null && property.objectReferenceValue != null) {
			if (!attribute.type.IsAssignableFrom(property.objectReferenceValue.GetType())) {
				Debug.Log("Object is not of type " + attribute.type.Name + ".");
				property.objectReferenceValue = null;
			}
		}
	}
}
#endif
