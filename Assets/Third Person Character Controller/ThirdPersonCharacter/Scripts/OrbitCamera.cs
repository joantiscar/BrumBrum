using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OrbitCamera : MonoBehaviour
{
	public Transform target;
	private Transform cachedTransform;
	public float distance = 7f;
	public float xSpeed = 250f;
	public float ySpeed = 120f;
	public float yMinLimit = 20f;
	public float yMaxLimit = 80f;
	private float x = 270f;
	private float y = 40f;

	private void Start()
	{
		cachedTransform = transform;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		Apply();
	}

	private void LateUpdate()
	{
		Apply();
	}

	private void Apply()
	{
		if (target == null)
		{
			return;
		}

		x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
		y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
		y = ClampAngle(y, yMinLimit, yMaxLimit);
		Quaternion rotation = Quaternion.Euler(y, x, 0f);
		Vector3 position = target.position + (rotation * Vector3.back * distance);
		cachedTransform.position = position;
		cachedTransform.LookAt(target);
	}

	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360f)
		{
			angle += 360f;
		}

		if (angle > 360f)
		{
			angle -= 360f;
		}

		return Mathf.Clamp(angle, min, max);
	}
}

