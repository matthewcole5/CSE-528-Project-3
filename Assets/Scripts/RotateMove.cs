using UnityEngine;

public class RotateMove : MonoBehaviour
{
	public bool isLocal = true;
	public float rSpeed = 5; // degrees
	public float mSpeed = 0.1f; // meters
	
	void Update()
		{
		transform.Rotate(0, rSpeed * Time.deltaTime, 0);
		if (isLocal)
			transform.Translate(0, 0, mSpeed * Time.deltaTime);
		else
			transform.Translate(0, 0, mSpeed * Time.deltaTime,
		Space.World);
		}
}
