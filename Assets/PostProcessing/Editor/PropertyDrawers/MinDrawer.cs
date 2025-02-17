using UnityEngine;
using UnityEngine.PostProcessing;
/*
     Problem:
     Assets\PostProcessing\Editor\PropertyDrawers\MinDrawer.cs(6,34): error CS0104: 'MinAttribute' is an ambiguous reference between 'UnityEngine.PostProcessing.MinAttribute' and 'UnityEngine.MinAttribute'
     
     Solution:
      by replacing
      "MinAttribute"
      to
      "UnityEngine.PostProcessing.MinAttribute"
      in MinDrawer.cs
     */

namespace UnityEditor.PostProcessing
{
    [CustomPropertyDrawer(typeof(UnityEngine.PostProcessing.MinAttribute))]
    sealed class MinDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            UnityEngine.PostProcessing.MinAttribute attribute = (UnityEngine.PostProcessing.MinAttribute)base.attribute;

            if (property.propertyType == SerializedPropertyType.Integer)
            {
                int v = EditorGUI.IntField(position, label, property.intValue);
                property.intValue = (int)Mathf.Max(v, attribute.min);
            }
            else if (property.propertyType == SerializedPropertyType.Float)
            {
                float v = EditorGUI.FloatField(position, label, property.floatValue);
                property.floatValue = Mathf.Max(v, attribute.min);
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Use Min with float or int.");
            }
        }
    }
}
