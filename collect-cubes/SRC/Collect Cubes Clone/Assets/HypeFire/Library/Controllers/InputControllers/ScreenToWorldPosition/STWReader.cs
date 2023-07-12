using System;
using HypeFire.Library.Controllers.InputControllers.Joystick;
using HypeFire.Library.Utilities.Logger;
using UnityEngine;

namespace HypeFire.Library.Controllers.InputControllers.ScreenToWorldPosition
{
	public class STWReader : InputController
	{
		public static STWReader GloballAccess { get; set; } = null;
		[field: SerializeField] private bool isInit { get; set; } = false;
		[field: SerializeField] private GameObject listenerObject { get; set; } = null;
		private new Camera camera { get; set; }

		private Vector3 _wordPosition;
		private float _zPos;

		protected override void Awake()
		{
			base.Awake();
			GloballAccess = this;
		}

		private void Update()
		{
			ReadInput();
		}

		public void Init(Camera cam, GameObject checkObject)
		{
			camera = cam;
			listenerObject = checkObject;
			_zPos = Math.Abs(Camera.main.transform.position.z);
			isInit = true;
		}

		public void ReadInput()
		{
			if (isInit)
			{
				if (Input.GetButton("Fire1"))
				{
					_wordPosition = camera.ScreenToWorldPoint(GetInputPosition());
					listeners.Invoke(new Result(true, _wordPosition));
				}
				else if (Input.GetButtonUp("Fire1"))
				{
					listeners.Invoke(new Result(false, _wordPosition));
				}
			}
			else if (Input.GetButtonDown("Fire1"))
			{
				gameObject.LogWarning($"{nameof(STWReader)} is not initialized");
			}
		}

		public Vector3 GetInputPosition() =>
			(Input.mousePosition + (Vector3.forward * _zPos));
	}
}