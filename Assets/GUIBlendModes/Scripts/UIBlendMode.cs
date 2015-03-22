using UnityEngine;
using UnityEngine.UI;

namespace GUIBlendModes
{
	/// <summary>
	/// Controls blending mode of UI elements.
	/// </summary>
	[AddComponentMenu("UI/Effects/Blend Mode")]
	[RequireComponent(typeof(MaskableGraphic)), ExecuteInEditMode]
	public class UIBlendMode : MonoBehaviour
	{
		/// <summary>
		/// Current blend mode of the UI element.
		/// </summary>
		public BlendMode BlendMode
		{
			get { return blendMode; }
			set { SetBlendMode(value, ShaderOptimization); }
		}

		/// <summary>
		/// Use material with an optimized version of the selected blend mode shader.
		/// Improves performance, but UI elements using this optimization 
		/// won't blend with each other.
		/// </summary>
		public bool ShaderOptimization
		{
			get { return shaderOptimization; }
			set { SetBlendMode(BlendMode, value); }
		}

		[SerializeField]
		private BlendMode editorBlendMode;
		private BlendMode blendMode;
		[SerializeField]
		private bool editorShaderOptimization;
		private bool shaderOptimization;
		private MaskableGraphic source;
		private bool isDisabled;

		private void OnEnable ()
		{
			isDisabled = false;
			SetBlendMode(editorBlendMode, editorShaderOptimization);
		}

		private void OnDisable ()
		{
			isDisabled = true;
			SetBlendMode(BlendMode.Normal, false);
		}

		/// <summary>
		/// Sets specific blend mode to the UI element.
		/// </summary>
		/// <param name="blendMode">Blend mode.</param>
		/// <param name="shaderOptimization">Use optimized version of a shader.
		/// Improves performance, but UI elements using this optimization
		/// won't blend with each other.</param>
		public void SetBlendMode (BlendMode blendMode, bool shaderOptimization = false)
		{
			if (this.blendMode == blendMode && this.shaderOptimization == shaderOptimization) return;
			if (!source) source = GetComponent<MaskableGraphic>();

			source.material = BlendMaterials.GetMaterial(blendMode, source is Text, shaderOptimization);

			this.blendMode = blendMode;
			this.shaderOptimization = shaderOptimization;
			if (!isDisabled)
			{
				editorBlendMode = blendMode;
				editorShaderOptimization = shaderOptimization;
			}
		}

		/// <summary>
		/// Used by the editor script to actuate parameters.
		/// Shouldn't be called manually.
		/// </summary>
		public void SyncEditor ()
		{
			if (Application.isEditor && !isDisabled 
				&& (BlendMode != editorBlendMode || ShaderOptimization != editorShaderOptimization)) 
				SetBlendMode(editorBlendMode, editorShaderOptimization);
		}
	}
}
