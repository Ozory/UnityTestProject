using UnityEngine;
using System.Collections;

public class Moviment : MonoBehaviour {


	public float Velocidade = 5f;
	public float Pulo = 10f;
	public float Rotacao = 30f;
	public float Gravidade =10f;

	private bool isJumping = false;


	private float holdJump = 0.1f;

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

	}

	void FixedUpdate ()
	{
		// Store the input axes.
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis("Vertical");
		
		// Move the player around the scene.
		Move (h, v);
	}

	void Move (float h, float v)
	{

		movement.Set (h, 0, v);

		// Normalise the movement vector and make it proportional to the speed per second.
		movement = movement.normalized * Velocidade * Time.deltaTime;
		//Velocidade = Velocidade * Time.deltaTime;

		// Move the player to it's current position plus the movement.
		playerRigidbody.MovePosition (transform.position + movement);

		// Set the movement vector based on the axis input.
		Jump();
		Rotate( movement);

	}

	void Jump(){
		if (Input.GetButton("Jump") && IsGrounded ==true){

			if(holdJump > 0){
				holdJump -= Time.deltaTime;
				Debug.Log(holdJump);
				Pulo=Pulo*2;
			}
			else{
				holdJump = 0.1f;
				Debug.Log("Soltei");
				Pulo=2;
			}

			playerRigidbody.AddRelativeForce(0, Pulo*100f, 0);
		}


		// Valido se alcancou a altura maxima
		if(playerRigidbody.position.y >=Pulo)
		{
			//Debug.Log("Alcancei a altura maxima");
			playerRigidbody.AddRelativeForce(0, (Pulo*Gravidade)*-1, 0);
		}
	}

	void Rotate(Vector3 move){
		if(move.x != 0f || move.z != 0f)
		{
			Rotating(move.x, move.z);
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
			isJumping=false;
			Debug.Log("Pousei");
		}
	}
	
	void OnCollisionExit (Collision coll)
	{
		if(coll.gameObject.tag =="chao"){
			IsGrounded = false;
			isJumping = true;
		}
	}

}
