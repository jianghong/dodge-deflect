using UnityEngine;

namespace GUIBlendModes
{
	/// <summary> 
	/// Manages all the materials for UI blending.
	/// </summary> 
	public static class BlendMaterials
	{
		public static Material[] Materials;
		public static bool Initialized;

		/// <summary>
		/// Loads the blending materials from Resources to the array. Automatically called on a first blend mode usage, 
		/// but may be called manually at any point beforehand to manage loading times.
		/// </summary>
		public static void Initialize ()
		{
			Materials = new Material[84];

			for (int i = 0; i < 21; i++)
				Materials[i] = Resources.Load<Material>("UIBlend" + ((BlendMode)(i + 1)).ToString());
			for (int i = 21; i < 42; i++)
				Materials[i] = Resources.Load<Material>("UIBlend" + ((BlendMode)(i - 20)).ToString() + "Optimized");
			for (int i = 42; i < 63; i++)
				Materials[i] = Resources.Load<Material>("UIFontBlend" + ((BlendMode)(i - 41)).ToString());
			for (int i = 63; i < 84; i++)
				Materials[i] = Resources.Load<Material>("UIFontBlend" + ((BlendMode)(i - 62)).ToString() + "Optimized");

			Initialized = true;
		}

		/// <summary>
		/// Returns specific blend mode material.
		/// </summary>
		/// <param name="mode">Blend mode.</param>
		/// <param name="font">Is target element a Text?</param>
		/// <param name="optimized">Use optimized version of the shader?</param>
		public static Material GetMaterial (BlendMode mode, bool font, bool optimized)
		{
			if (!Initialized) Initialize();

			if (font) return mode == BlendMode.Normal ? null
				: Materials[((int)mode - 1) + (optimized ? 63 : 42)];
			else return mode == BlendMode.Normal ? null
				: Materials[((int)mode - 1) + (optimized ? 21 : 0)];
		}
	}
}
