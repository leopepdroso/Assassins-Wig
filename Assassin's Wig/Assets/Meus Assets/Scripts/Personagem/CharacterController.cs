using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	public float maxSpeed =1f;
	private bool facingRight = true;
	public Rigidbody2D charRigidBody;
	Animator anim;
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 700f;
	bool doubleJump = false;

	void Start () {
		Rigidbody2D charRigidBody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();
	}

	void FixedUpdate () {

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);
		if (grounded) {
			doubleJump = false;
		}

		anim.SetFloat ("VSpeed", charRigidBody.velocity.y);
		float move = Input.GetAxis("Potato");
		anim.SetFloat ("Speed", Mathf.Abs (move));
		charRigidBody.velocity = new Vector2 (move * maxSpeed, charRigidBody.velocity.y);
		if (move > 0 && !facingRight) {
			Flip();
			//			anim.SetBool ("Correndo", true);
		} else if (move < 0 && facingRight) {
			Flip ();
		}

	}
	void Update() {
		if ((grounded || !doubleJump) && Input.GetKeyDown (KeyCode.Space)) {
			anim.SetBool("Grounded", false);
			charRigidBody.AddForce(new Vector2(0, jumpForce));
			if (!doubleJump && !grounded) {
				doubleJump = true;
			}
		}



	}

	void Flip () {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
