using UnityEngine;
using System.Collections;
	
public class CameraManager : MonoBehaviour
{
	public Transform target;            // The position that that camera will be following.
	public float smoothing = 5f;        // The speed with which the camera will be following.
	public float SmoothinFocus = 10f;	// Smothing to focus
	public float distance = 5.0f;

	public bool follow = false;

	private float zAngle = 5.0f;
	
	private float defaultDistance = 0f;
	

	public float height = 3.0f;
	public float damping = 5.0f;
	public bool smoothRotation = true;
	public float rotationDamping = 10.0f;

	void Start ()
	{
		defaultDistance = distance;
	}
		
	void FixedUpdate ()
	{
		zAngle = distance / 2;
//		Vector3 pos = new Vector3(target.position.x,distance, target.position.z-zAngle);
//		// Smoothly interpolate between the camera's current position and it's target position.
//		transform.position = Vector3.Lerp (transform.position, pos, smoothing * Time.deltaTime);


		Vector3 wantedPosition = target.TransformPoint(0, height, -distance);
		transform.position = Vector3.Lerp (transform.position, wantedPosition, Time.deltaTime * damping);
		
		if (smoothRotation) {
			Quaternion wantedRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
			transform.rotation = Quaternion.Slerp (transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
		}
		else transform.LookAt (target, target.up);

	}

	#region METODOS DA CAMERA

	public void ChangeTarget(Transform newTarget, float smoth, float seconds, float zoom)
	{
		Transform oldTarget = this.target;
		float oldDistance = distance;

		this.target = newTarget;
		this.SmoothinFocus = smoth;
		this.distance = zoom>0?zoom:distance;

		StartCoroutine(Focus(seconds,oldTarget,smoth,oldDistance));
	}
	
	public void ZoomIn(float distance)
	{
		this.distance = distance;
		this.zAngle= distance/2;
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