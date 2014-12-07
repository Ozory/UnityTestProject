using UnityEngine;
using System.Collections;
	
public class CameraManager : MonoBehaviour
{
	public Transform target;            // The position that that camera will be following.
	public float smoothing = 5f;        // The speed with which the camera will be following.
		
	public float distance = 5.0f;
	public float zAngle = 10.0f;

	void Start ()
	{

	}
		
	void FixedUpdate ()
	{
		Vector3 pos = new Vector3(target.position.x,distance, target.position.z-zAngle);

		// Smoothly interpolate between the camera's current position and it's target position.
		transform.position = Vector3.Lerp (transform.position, pos, smoothing * Time.deltaTime);
	}

	#region METODOS DA CAMERA

	public void ChangeTarget(Transform newTarget, float smoth, float seconds, float zoom)
	{
		Transform oldTarget = this.target;
		float oldSmoothing = this.smoothing;
		float oldDistance = distance;

		this.target = newTarget;
		this.smoothing = smoth;
		this.distance = zoom>0?zoom:distance;

		StartCoroutine(Focus(seconds,oldTarget,smoth,oldDistance));
	}

	public void ZoomIn(float distance)
	{
		float oldDistance = this.distance;
		float oldZangle = this.zAngle;

		this.distance = distance;
		this.zAngle= distance/2;
		this.smoothing = this.zAngle*10;
	}

	private IEnumerator Focus(float seconds, Transform oldTarget, float smoth, float distance) {
		yield return new WaitForSeconds(seconds);
		this.target = oldTarget;
		this.smoothing = smoth;
		this.distance = distance;
	}
	
	#endregion
}