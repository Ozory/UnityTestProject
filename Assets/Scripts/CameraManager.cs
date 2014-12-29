using UnityEngine;
using System.Collections;
	
public class CameraManager : MonoBehaviour
{
	public Transform target;            // The position that that camera will be following.
	public float smoothing = 5f;        // The speed with which the camera will be following.
	public float SmoothinFocus = 10f;	// Smothing to focus
	public float distance = 5.0f;
	private float zAngle = 10.0f;


	private float defaultSmothing = 0f;
	private float defaultDistance = 0f;

	void Start ()
	{
		defaultSmothing = smoothing;
		defaultDistance = distance;
	}
		
	void FixedUpdate ()
	{
		zAngle = distance / 2;
		Vector3 pos = new Vector3(target.position.x,distance, target.position.z-zAngle);

		// Smoothly interpolate between the camera's current position and it's target position.
		transform.position = Vector3.Lerp (transform.position, pos, smoothing * Time.deltaTime);

		this.smoothing = defaultSmothing;
	}

	#region METODOS DA CAMERA

	public void ChangeTarget(Transform newTarget, float smoth, float seconds, float zoom)
	{
		Transform oldTarget = this.target;
		float oldSmoothing = this.SmoothinFocus;
		float oldDistance = distance;

		this.target = newTarget;
		this.SmoothinFocus = smoth;
		this.distance = zoom>0?zoom:distance;

		StartCoroutine(Focus(seconds,oldTarget,smoth,oldDistance));
	}

	public void ZoomIn(float distance)
	{
		float oldDistance = this.distance;
		float oldZangle = this.zAngle;

		this.distance = distance;
		this.zAngle= distance/2;

		this.defaultSmothing = smoothing;
		this.smoothing = this.SmoothinFocus;
	}

	private IEnumerator Focus(float seconds, Transform oldTarget, float smoth, float distance) {
		yield return new WaitForSeconds(seconds);
		this.target = oldTarget;
		this.SmoothinFocus = smoth;
		this.distance = distance;
	}

	public void ResetZoom(){
		this.distance = defaultDistance;
	}
	
	#endregion
}