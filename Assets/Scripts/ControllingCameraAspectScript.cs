using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllingCameraAspectScript : MonoBehaviour
{
	public float orthographicSize = 4;
	private float aspect = 2.0f/3.0f; 


	void Start()
	{
		Camera camera = GetComponent<Camera> ();

		Camera.main.projectionMatrix = Matrix4x4.Ortho(
			-orthographicSize * aspect, orthographicSize * aspect,
			-orthographicSize, orthographicSize,
			camera.nearClipPlane, camera.farClipPlane);
	}


}
