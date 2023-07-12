using System;
using HypeFire.Library.Utilities.Extensions.Object;
using UnityEngine;
using UnityEngine.Serialization;

namespace HypeFire.Library.GameSpecial
{
	public class PositionalFollowers : MonoBehaviour
	{
		[field: SerializeField] private Rules rules { get; set; } = new Rules();

		[field: SerializeField] private GameObject targetObject { get; set; } = null;


		private void Update()
		{
			if (targetObject.IsNull())
				return;

			if (rules.isFollowInstantly)
			{
				var pos = transform.position;
				pos.x = rules.isFollowXPosition ? targetObject.transform.position.x : pos.x;
				pos.y = rules.isFollowYPosition ? targetObject.transform.position.y : pos.y;
				pos.z = rules.isFollowZPosition ? targetObject.transform.position.z : pos.z;
				transform.position = pos;
			}
			else
			{
				var pos = transform.position;
				pos.x = rules.isFollowXPosition ? targetObject.transform.position.x : pos.x;
				pos.y = rules.isFollowYPosition ? targetObject.transform.position.y : pos.y;
				pos.z = rules.isFollowZPosition ? targetObject.transform.position.z : pos.z;
				transform.position = Vector3.Lerp(transform.position, pos, rules.lerpTime);
			}
		}

		[Serializable]
		public struct Rules
		{
			public bool isFollowXPosition;
			public bool isFollowYPosition;
			public bool isFollowZPosition;

			[FormerlySerializedAs("isFollowİnstantly")]
			public bool isFollowInstantly;

			public float lerpTime;

			public Rules(bool isFollowXPosition = false, bool isFollowYPosition = false,
				bool isFollowZPosition = false, bool isFollowInstantly = true, float lerpTime = .1f)
			{
				this.lerpTime = lerpTime;
				this.isFollowInstantly = isFollowInstantly;
				this.isFollowXPosition = isFollowXPosition;
				this.isFollowYPosition = isFollowYPosition;
				this.isFollowZPosition = isFollowZPosition;
			}
		}
	}
}