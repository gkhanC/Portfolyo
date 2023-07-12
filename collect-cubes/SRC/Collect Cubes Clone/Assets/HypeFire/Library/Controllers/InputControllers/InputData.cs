using UnityEngine;

namespace HypeFire.Library.Controllers.InputControllers
{
	/// <summary>
	/// Kontrolcü taraflı derlenen kullanıcı girdisi.
	/// </summary>
	public struct InputData
	{
		public bool isActive;
		public Vector2 direction;
		public Vector3 position;

		/// <summary>
		/// right = 1, left = -1
		/// </summary>
		public float horizontalAxis => direction.x > 0f ? 1f : direction.x == 0f ? 0f : -1f;

		/// <summary>
		/// up = 1, down = -1
		/// </summary>
		private float verticalAxis => direction.y > 0f ? 1f : direction.y == 0f ? 0f : -1f;

		public InputData(bool active, Vector2 direction)
		{
			this.isActive = active;
			this.direction = direction;
			position = Vector3.zero;
		}

		public InputData(bool active, Vector3 position)
		{
			this.isActive = active;
			this.position = position;
			direction = Vector3.zero;
		}

		public InputData(bool active, Vector2 direction, Vector3 position)
		{
			this.isActive = active;
			this.position = position;
			this.direction = direction;
		}
	}
}