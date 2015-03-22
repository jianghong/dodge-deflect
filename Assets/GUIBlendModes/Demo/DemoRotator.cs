using UnityEngine;

namespace GUIBlendModes
{
	public class DemoRotator : MonoBehaviour
	{
		private void Update ()
		{
			transform.Rotate(new Vector3(0, 0, -1), Time.deltaTime / 3);
		}
	}
}