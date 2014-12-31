using UnityEngine;
using System.Collections;

public class MoveJump : MonoBehaviour {

	public float Velocidade = 6.0F;
	public float Pulo = 8.0F;
	public float Rotacao = 10f;
	public float Gravidade = 20.0F;

	private Vector3 moveDirection = Vector3.zero;
	private Rigidbody playerRigidbody;
	private CharacterController controller;

	void Awake(){

		controller = GetComponent<CharacterController>();
		playerRigidbody = GetComponent <Rigidbody> ();
		
		// Para impedir a rotacao do player enquanto anda
		playerRigidbody.freezeRotation = true;
	}

	void Update() {

	}

	void FixedUpdate(){
		Move ();
	}

	void Move(){

		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		if (controller.isGrounded) {

			// TODO: ANIMATION


			moveDirection = new Vector3(h, 0, v);
			//moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= Velocidade;


			Jump();
		}
		//moveDirection.y -= Gravidade * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);

		if(h != 0f || v != 0f)
		{
			//Rotating(h, v);
		}
	}

	void Jump(){
		if (Input.GetButton("Jump")){

			//TODO: ANIMATION

			moveDirection.y = Pulo;
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
}
