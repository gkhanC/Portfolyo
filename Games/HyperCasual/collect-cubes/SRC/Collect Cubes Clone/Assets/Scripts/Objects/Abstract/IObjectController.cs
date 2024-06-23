using UnityEngine;

namespace Objects.Abstract
{
	public interface IObjectController
	{
		public GameObject getGameObject { get; }
		public Transform getTransform { get; }
		public void SetColor(Color color);
		public void SetLocalPosition(Vector3 position);
		public void SetSize(float size);
		public void SetSize(Vector3 size);
	}
}