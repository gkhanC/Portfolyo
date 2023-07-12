using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CameraPositionSetter : MonoBehaviour
{
	public Collider _collider;
	private bool isInCameraView;

	private void Start()
	{
		_collider = GetComponent<BoxCollider>();
		IsInCameraView();
	}

	private void Update()
	{
		if (!isInCameraView)
		{
			var newCamPos = Camera.main.transform.position + Vector3.up;
			Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, newCamPos, .1f);
			IsInCameraView();
		}
		else
		{
			this.enabled = false;
		}
	}

	public void IsInCameraView()
	{
		Bounds bounds = _collider.bounds;
		Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
		isInCameraView = GeometryUtility.TestPlanesAABB(frustumPlanes, bounds);
	}
}