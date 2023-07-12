using UnityEngine;

namespace Player
{
	[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/PlayerData")]
	public class PlayerData :  ScriptableObject
	{
		public float speed = 350;
	}
}