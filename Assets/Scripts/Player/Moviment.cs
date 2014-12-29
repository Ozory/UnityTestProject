using UnityEngine;
using System.Collections;

public class Moviment : MonoBehaviour {


	public float Velocidade = 5f;
	public float Pulo = 10f;
	public float Rotacao = 30f;

	private float VerticalSpeedy = 0f;

	private Vector3 movement;
	private Rigidbody playerRigidbody;

	private CollisionFlags collisionFlags;
	private CharacterController controller;
	private bool IsGrounded = false;

	void Awake(){

		playerRigidbody = GetComponent <Rigidbody> ();

		// Para impedir a rotacao do player enquanto anda
		playerRigidbody.freezeRotation = true;
	}

	void Update(){
		Jump(movement);
	}

	void FixedUpdate ()
	{
		// Store the input axes.
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		
		// Move the player around the scene.
		Move (h, v);

	}

	void Move (float h, float v)
	{
		// Set the movement vector based on the axis input.
		movement.Set (h, VerticalSpeedy, v);

		// Normalise the movement vector and make it proportional to the speed per second.
		movement = movement.normalized * Velocidade * Time.deltaTime;
		//Velocidade = Velocidade * Time.deltaTime;

		// Move the player to it's current position plus the movement.
		playerRigidbody.MovePosition (transform.position + movement);


		if(h != 0f || v != 0f)
		{
			Rotating(h, v);
		}
	}

	void Jump(Vector3 movement){
		if (Input.GetButton("Jump") && IsGrounded ==true){
			VerticalSpeedy = Mathf.Clamp(Pulo*100,Pulo*100,Pulo*100);
			playerRigidbody.AddRelativeForce(new Vector3(movement.x, VerticalSpeedy, movement.z));
			//IsGrounded=false;
		}
	}

	void Rotating (float horizontal, float vertical)
	{
		// Create a new vector of the horizontal and vertical inputs.
		Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);

		// Create a rotation based on this new vector assuming that up is the global y axis.
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

		// Create a rotation that is an increment closer to the target rotation from the player's rotation.
		Quaternion newRotation = Quaternion.Lerp(transform.rotation, targetRotation, Rotacao * Time.deltaTime);
		
		// Change the players rotation to this new rotation.
		playerRigidbody.MoveRotation(newRotation);
	}

	void OnCollisionEnter (Collision coll)
	{
		if(coll.gameObject.tag =="chao"){
			IsGrounded = true;
			VerticalSpeedy = 0f;
		}
	}
	
	void OnCollisionExit (Collision coll)
	{
		if(coll.gameObject.tag =="chao"){
			IsGrounded = false;
			if(VerticalSpeedy>0){
				VerticalSpeedy -= 20 * Time.deltaTime;
			}
		}
	}

}
