using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeOrc : MonoBehaviour {


	public float speed = 1.0f;
	Rigidbody2D body =null;
	bool isGrounded = false;

	public Vector3 MoveBy;
	private Vector3 pointA,pointB;
	private Mode mode= Mode.GoToB; 
	public float DieTime = 1;
	bool die=false;
	float timeToBeDied;

	public GameObject carrot;
	float lastC=1.0f;
	float carWait=1.0f;
	public float radius=2.0f;
	void Start () {
		body = this.GetComponent<Rigidbody2D> ();
		pointA = this.transform.position;
		pointB = this.pointA + MoveBy;
		spr=GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();
	}

	public enum Mode
	{
		GoToA,
		GoToB,Attack,Die
	}
	private IEnumerator AttackC(){
		launchCarrot (dir);
		yield return new WaitForSeconds (0.8f);

	}
	private void carA(){
		if(lastC>=carWait){
			StartCoroutine (AttackC ());
			lastC = 0;
	} else
		{
			lastC+=Time.deltaTime;
		}
	}
	float dir;

	float getDirection(){
		Vector3 rabbit_position = HeroRabit.currentRabit.transform.position;
		Vector3 my_position = this.transform.position;
		if (Mathf.Abs (rabbit_position.x - my_position.x) < radius) {
			animator.SetBool ("attackHand", true);
				dir = rabbit_position.x - my_position.x;
				this.carA ();
			animator.SetBool ("attackHand", false);

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
	SpriteRenderer spr = null;
	Animator animator = null;
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
		//Перевіряємо чи проходить лінія через Collider з шаром Ground
		RaycastHit2D hit = Physics2D.Linecast (from, to, layer_id);
		if (hit) {
			isGrounded = true;
		}
		else {
			isGrounded = false;
		}
		//Намалювати лінію (для розробника)
		Debug.DrawLine (from, to, Color.red);

		if (die) {
			mode = Mode.Die;
			DieTime -= Time.deltaTime;
			if (DieTime <= 0) {
				die = true;
				Destroy (this.gameObject);
			}
		}


	}
	void launchCarrot(float direction){
		
		if (direction != 0) {
			GameObject obj = GameObject.Instantiate (this.carrot);
			obj.transform.position = this.transform.position;
			Vector3 v = new Vector3 (0, 1, 0);
			Vector3 vf = obj.transform.position + v;
			obj.transform.position = vf;
			Carrot car = obj.GetComponent<Carrot> ();
			this.animator.SetBool ("attackHand",true);
			car.launch (direction);
		}

	}
	void OnCollisionEnter2D(Collision2D coll){

		HeroRabit rab = coll.gameObject.GetComponentInParent<HeroRabit> ();
		if (rab != null) {
			if( ((rab.transform.position.y)-1) >= this.transform.position.y) {
				if (die) {
					return;
				}
				if (this.isGrounded) {
					die = true;
					animator.SetBool("die",true);
					timeToBeDied = DieTime;
				}
			} else {

				this.animator.SetTrigger ("attack");
				rab.removeHealth(1);
			}
		}
	}

}