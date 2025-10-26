using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// This is a custom editor script that should allow the interface wrapper to be drawn in the inspector.
/// 
/// REM-i
/// </summary>

[CustomPropertyDrawer(typeof(InterfaceWrapper<>), true)]
public class InterfaceDrawer : PropertyDrawer
{
    /// <summary>
    /// This is the method that will be called to draw the interface wrapper in the inspector.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="property"></param>
    /// <param name="label"></param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Draw the object field for the interface wrapper
        var objProperty = property.FindPropertyRelative("_object");

        // Call boilerplate begin
        EditorGUI.BeginProperty(position, label, property);

        // Pull our interface type from reflection
        var interfaceType = GetInterfaceTypeFromProperty(property);

        // This will draw the object picker
        var newObj = EditorGUI.ObjectField(
            position,
            label,
            objProperty.objectReferenceValue,
            typeof(UnityEngine.Object),
            true
        );

        // This verifies that the new object is of the correct type
        if (newObj != null)
        {
            if (newObj is GameObject go)
            {
                var component = go.GetComponent(interfaceType);

                if (component != null)
                {
                    newObj = component;
                }
                else
                {
                    Debug.LogWarning($"Gameobject does not have a component implementing {interfaceType.Name}.");
                    newObj = null;
                }
            }
            else if (!interfaceType.IsAssignableFrom(newObj.GetType()))
            {
                Debug.LogWarning($"Object does not implement {interfaceType.Name}.");
                newObj = null;
            }
        }

        // Set the object reference value to the new object
        objProperty.objectReferenceValue = newObj;

        // Call end boilerplate and apply changes
        EditorGUI.EndProperty();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="property"></param>
    /// <returns></returns>
    public static Type GetInterfaceTypeFromProperty(SerializedProperty property)
    {
        if (property == null) return null;

        // Split the property path for its elements
        string[] elements = property.propertyPath.Split('.');

        // Start at either monobehaviour or scriptable object type
        Type currentType = property.serializedObject.targetObject.GetType();
        FieldInfo field = null;

        foreach (string element in elements)
        {
            if (element == "Array")
            {
                // This just ignores the cases where we run into Unity's inserted "Array" element (done for all lists and arrays)
                continue;
            }
            else if (element.StartsWith("data["))
            {
                // This is when we have an element inside an array/list
                if (currentType.IsArray)
                {
                    // If we are using an array, we get the element type
                    currentType = currentType.GetElementType();
                }
                // This means we are working with a list and a generic T
                else if (currentType.IsGenericType)
                {
                    // Extract the generic type definition
                    Type genericDef = currentType.GetGenericTypeDefinition();

                    // If the generic type is a List or InterfaceWrapper, we can get the type of the element
                    if (genericDef == typeof(List<>) || genericDef == typeof(InterfaceWrapper<>))
                    {
                        currentType = currentType.GetGenericArguments()[0];
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            // Otherwise, we are working with an individual element
            else
            {
                field = currentType.GetField(element, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (field == null) return null;

                currentType = field.FieldType;
            }
        }

        // After we process all the elements, we check if the current type is a generic type of InterfaceWrapper
        if (currentType.IsGenericType && currentType.GetGenericTypeDefinition() == typeof(InterfaceWrapper<>))
        {
            return currentType.GetGenericArguments()[0];
        }

        return null;
    }
}
