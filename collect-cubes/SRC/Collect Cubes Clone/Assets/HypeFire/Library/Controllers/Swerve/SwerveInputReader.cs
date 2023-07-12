using UnityEngine;

namespace HypeFire.Library.Controllers.Swerve
{
	public class SwerveInputReader
	{
		private Transform _goTransform;
		private Vector3 _inputPosition;
		public Camera _cam;

		public SwerveInputReader(Transform goTransform, Camera cam)
		{
			_goTransform = goTransform;
			_cam = cam;
		}

		public float GetHorizontalPosition()
		{
			_inputPosition = Input.mousePosition;
			_inputPosition.z = Mathf.Abs(_cam.transform.position.z);
			return _cam.ScreenToWorldPoint(_inputPosition).x;
		}

	}
}