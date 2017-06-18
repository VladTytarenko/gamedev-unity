using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenOrc : MonoBehaviour {

	public float speed = 1.0f;
	Rigidbody2D body = null;
	bool isGrounded = false;
	public Vector3 MoveBy;
	private Vector3 pointA,pointB;
	private Mode mode = Mode.GoToB; 
	public float DieTime = 1.0f;
	bool die = false;
	float timeToBeDied;
    SpriteRenderer spr = null;
    Animator animator = null;

	void Start () {
		body = this.GetComponent<Rigidbody2D> ();
		pointA = this.transform.position;
		pointB = this.pointA + MoveBy;
		spr = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();
	}
	public enum Mode
	{
		GoToA,
	GoToB,Attack,Die
	}


	float getDirection(){
		Vector3 rabbit_position = HeroRabit.currentRabit.transform.position;
		Vector3 my_position = this.transform.position;
		if (rabbit_position.x > Mathf.Min (pointA.x, pointB.x) && rabbit_position.x < Mathf.Max (pointA.x, pointB.x)) {
			mode = Mode.Attack;
		}
		if (mode == Mode.GoToA) {
			if (hasArrived (my_position, pointA)) {
				mode = Mode.GoToB;

			}
		} else if (mode== Mode.GoToB) {
			if (hasArrived (my_position, pointB)) {
				mode = Mode.GoToA;

			}
		} else if (mode == Mode.Attack) {
			if (my_position.x < rabbit_position.x) {
				return 1;
			} else {
				return -1;
			}
		} 
		if (mode == Mode.GoToA) {
			
			if (my_position.x < pointA.x) {
				return 1;
			} else {
				return -1;
			}
		} else if (mode == Mode.GoToB) {
			if (my_position.x < pointB.x) {
				return 1;
			} else {
				return -1;
			}
		} else {
			return 0;
		}
	}
	bool hasArrived(Vector3 position,Vector3 target){
		position.z = 0;
		target.z = 0;
		return Vector3.Distance (position, target) < 0.5f;
	}
	
	void FixedUpdate () {
		float val = this.getDirection();


		if (Mathf.Abs (val) > 0) {
			Vector2 vec2 = body.velocity;
			vec2.x = val * speed;
			body.velocity = vec2;
			animator.SetBool ("run", true);
		} else {
			animator.SetBool ("run", false);
		}
		if (val > 0) {
			spr.flipX = true;
		} else if (val < 0) {
			spr.flipX = false;
		}
		Vector3 from = transform.position + Vector3.up * 0.3f;
		Vector3 to = transform.position + Vector3.down * 0.1f;
		int layer_id = 1 << LayerMask.NameToLayer ("Ground");
		
		RaycastHit2D hit = Physics2D.Linecast (from, to, layer_id);
		if (hit) {
			isGrounded = true;
		}
		else {
			isGrounded = false;
		}
		
		Debug.DrawLine (from, to, Color.red);

	if (die) {
			DieTime -= Time.deltaTime;
			if (DieTime <= 0) {
				die = true;
				Destroy (this.gameObject);
			}
		}


	}

	void OnCollisionEnter2D(Collision2D coll){
		if (!die) {
			HeroRabit rab = coll.gameObject.GetComponent<HeroRabit> ();
			if (rab != null) {
				if (((rab.transform.position.y) - 1) >= this.transform.position.y) {
					if (die) {
						return;
					}
					if (this.isGrounded) {
						die = true;
						animator.SetBool ("die", true);
						timeToBeDied = DieTime;
					}
				} else {
			
					this.animator.SetTrigger ("attack");
					rab.removeHealth (1);
					this.animator.SetBool ("run",true);
				}
			}

		}

	}
}