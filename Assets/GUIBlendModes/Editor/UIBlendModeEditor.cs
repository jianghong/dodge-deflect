using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace GUIBlendModes
{
	[CustomEditor(typeof(UIBlendMode)), CanEditMultipleObjects]
	public class UIBlendModeEditor : Editor
	{
		private bool showOptimization;
		private GUIContent blendModeContent = new GUIContent("Blend Mode", "Blend mode of the element.");
		private GUIContent shaderOptimizationContent = new GUIContent("Shader Optimization", 
			"Use optimized version of a shader. Improves performance, but UI elements using this optimization won't blend with each other.");
		private SerializedProperty blendMode;
		private SerializedProperty shaderOptimization;

		private List<BlendMode> BasicModes = new List<BlendMode>() { 
			BlendMode.Normal, 
			//BlendMode.Darken, 
			//BlendMode.Multiply, 
			//BlendMode.LinearBurn,  
			//BlendMode.Lighten, 
			//BlendMode.LinearDodge, 
			//BlendMode.Subtract 
		};

		private void OnEnable ()
		{
			blendMode = serializedObject.FindProperty("editorBlendMode");
			shaderOptimization = serializedObject.FindProperty("editorShaderOptimization");
		}

		public override void OnInspectorGUI ()
		{
			serializedObject.Update();
			EditorGUILayout.PropertyField(blendMode, blendModeContent);
			if (Event.current.type == EventType.Layout)
				showOptimization = !BasicModes.Contains((Selection.activeObject as GameObject).GetComponent<UIBlendMode>().BlendMode);
			if (showOptimization) EditorGUILayout.PropertyField(shaderOptimization, shaderOptimizationContent);
			serializedObject.ApplyModifiedProperties();
			foreach (var obj in Selection.objects)
				((GameObject)obj).GetComponent<UIBlendMode>().SyncEditor();
		}
	}
}
