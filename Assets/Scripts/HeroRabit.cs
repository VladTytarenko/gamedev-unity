/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabit : MonoBehaviour {

    public float speed = 1;
    Rigidbody2D myBody = null;
    bool isGrounded = false;
    bool JumpActive = false;
    float JumpTime = 0f;
    public float MaxJumpTime = 2f;
    public float JumpSpeed = 2f;
    Transform RabbitParent = null;
    public int MaxHealth = 2;
    int health = 1;
    bool isSuper = false;

	// Use this for initialization
	void Start () {
        myBody = this.GetComponent<Rigidbody2D>();
        LevelController.current.setStartPosition(this.transform.position);
        RabbitParent = this.transform.parent;
	}

    void FixedUpdate () {
        /*Vector3 from = transform.position + Vector3.up * 0.3f;
        Vector3 to = transform.position + Vector3.down * 0.1f;
        int layer_id = 1 << LayerMask.NameToLayer("Ground");
        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);

        if (hit)
        {
            isGrounded = true;
        } else {
            isGrounded = false;
        }
        Debug.DrawLine(from, to, Color.red);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            this.JumpActive = true;
        }

        if (this.JumpActive)
        {
            if (Input.GetButton("Jump"))
            {
                this.JumpTime += Time.deltaTime;
                if (this.JumpTime < this.MaxJumpTime)
                {
                    Vector2 vel = myBody.velocity;
                    vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
                    myBody.velocity = vel;
                }
            } else {
                this.JumpActive = false;
                this.JumpTime = 0;
            }
        }

        float value = Input.GetAxis("Horizontal");
        Animator animator = GetComponent<Animator>();
        if (Mathf.Abs(value) > 0)
        {
            Vector2 vel = myBody.velocity;
            vel.x = value * speed;
            myBody.velocity = vel;
            animator.SetBool("run", true);
        } else {
            animator.SetBool("run", false);
        }

        if (this.isGrounded)
        {
            animator.SetBool("jump", false);
        } else {
            animator.SetBool("jump", true);
        }

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (value < 0)
            sr.flipX = true;
        else if (value > 0)
            sr.flipX = false;
        float val = Input.GetAxis("Horizontal");

        SpriteRenderer spr = GetComponent<SpriteRenderer>();

        Animator animator = GetComponent<Animator>();

        if (Mathf.Abs(val) > 0)
        {

            Vector2 vec2 = myBody.velocity;

            vec2.x = val * speed;

            myBody.velocity = vec2;

            animator.SetBool("run", true);

        }
        else
        {

            animator.SetBool("run", false);

        }

        if (val < 0)
        {

            spr.flipX = true;

        }
        else if (val > 0)
        {

            spr.flipX = false;

        }

        Vector3 from = transform.position + Vector3.up * 0.3f;

        Vector3 to = transform.position + Vector3.down * 0.1f;

        int layer_id = 1 << LayerMask.NameToLayer("Ground");

        //Перевіряємо чи проходить лінія через Collider з шаром Ground

        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);

        if (hit)
        {

            if (hit.transform != null && hit.transform.GetComponent<MovingPlatform>() != null)
            {

                SetNewParent(this.transform, hit.transform);

            }
            else
            {

                SetNewParent(this.transform, this.RabbitParent);

            }

            isGrounded = true;

        }

        else
        {

            isGrounded = false;

        }

        //Намалювати лінію (для розробника)

        Debug.DrawLine(from, to, Color.red);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {

            this.JumpActive = true;

        }

        if (this.JumpActive)
        {

            //Якщо кнопку ще тримають

            if (Input.GetButton("Jump"))
            {

                this.JumpTime += Time.deltaTime;

                if (this.JumpTime < this.MaxJumpTime)
                {

                    Vector2 vel = myBody.velocity;

                    vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);

                    myBody.velocity = vel;

                }

            }
            else
            {

                this.JumpActive = false;

                this.JumpTime = 0;

            }

        }

        if (this.isGrounded)
        {

            animator.SetBool("jump", false);

        }
        else
        {

            animator.SetBool("jump", true);

        }

    }

    static void SetNewParent(Transform obj, Transform new_parent)
    {
        if (obj.transform.parent != new_parent)
        {
            Vector3 pos = obj.transform.position;
            obj.transform.parent = new_parent;
            obj.transform.position = pos;
        }
    }

    public void addHealth(int number)
    {
        this.health += number;
        if (this.health > MaxHealth)
            this.health = MaxHealth;
        this.onHealthChange();
    }

    public void removeHealth(int number)
    {
        this.health -= number;
        if (this.health < 0)
            this.health = 0;
        this.onHealthChange();
    }

    public void onHealthChange()
    {
        if (this.health == 1)
        {
            this.transform.localScale = Vector3.one;
        }
        else if (this.health == 2)
        {
            this.transform.localScale = Vector3.one * 2;
            isSuper = true;
        }
        else if (this.health == 0)
        {
            LevelController.current.onRabitDeath(this);
            isSuper = false;
        }
    }

    public void becomeSuper()
    {
        if (!isSuper)
        {
            isSuper = true;
            transform.localScale += new Vector3(0.5f, 0.5f, 0);
        }
    }

    public void BombTouch()
    {
        Animator animator = GetComponent<Animator>();
        if (isSuper)
        {
            isSuper = false;
            transform.localScale += new Vector3(-0.5F, -0.5f, 0);
        }
        else
        {
            animator.SetBool("death", true);
            LevelController.current.onRabitDeath(this);
            animator.SetBool("death", false);

        }
    }
}*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabit : MonoBehaviour
{
    public float speed = 1;
    Rigidbody2D body = null;
    bool isGrounded = false;
    bool JumpActive = false;
    float JumpTime = 0f;
    public float MaxJumpTime = 2f;
    public float JumpSpeed = 2f;
    Transform RabbitParent = null;
    public int MaxHealth = 2;
    int health = 1;
    bool isSuper = false;
    // Use this for initialization
    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        RabbitParent = this.transform.parent;
        LevelController.current.setStartPosition(this.transform.position);

    }
    static void SetNewParent(Transform obj, Transform new_parent)
    {
        if (obj.transform.parent != new_parent)
        {
            Vector3 pos = obj.transform.position;
            obj.transform.parent = new_parent;
            obj.transform.position = pos;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        float val = Input.GetAxis("Horizontal");
        SpriteRenderer spr = GetComponent<SpriteRenderer>();
        Animator animator = GetComponent<Animator>();
        if (Mathf.Abs(val) > 0)
        {
            Vector2 vec2 = body.velocity;
            vec2.x = val * speed;
            body.velocity = vec2;
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }
        if (val < 0)
        {
            spr.flipX = true;
        }
        else if (val > 0)
        {
            spr.flipX = false;
        }
        Vector3 from = transform.position + Vector3.up * 0.3f;
        Vector3 to = transform.position + Vector3.down * 0.1f;
        int layer_id = 1 << LayerMask.NameToLayer("Ground");
        //Перевіряємо чи проходить лінія через Collider з шаром Ground
        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
        if (hit)
        {
            if (hit.transform != null && hit.transform.GetComponent<MovingPlatform>() != null)
            {
                SetNewParent(this.transform, hit.transform);
            }
            else
            {
                SetNewParent(this.transform, this.RabbitParent);
            }
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        //Намалювати лінію (для розробника)
        Debug.DrawLine(from, to, Color.red);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            this.JumpActive = true;
        }
        if (this.JumpActive)
        {
            //Якщо кнопку ще тримають
            if (Input.GetButton("Jump"))
            {
                this.JumpTime += Time.deltaTime;
                if (this.JumpTime < this.MaxJumpTime)
                {
                    Vector2 vel = body.velocity;
                    vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
                    body.velocity = vel;
                }
            }
            else
            {
                this.JumpActive = false;
                this.JumpTime = 0;
            }
        }
        if (this.isGrounded)
        {
            animator.SetBool("jump", false);
        }
        else
        {
            animator.SetBool("jump", true);
        }
    }
    public void addHealth(int number)
    {
        this.health += number;
        if (this.health > MaxHealth)
            this.health = MaxHealth;
        this.onHealthChange();
    }

    public void removeHealth(int number)
    {
        this.health -= number;
        if (this.health < 0)
            this.health = 0;
        this.onHealthChange();
    }

    public void onHealthChange()
    {
        if (this.health == 1)
        {
            this.transform.localScale = Vector3.one;
        }
        else if (this.health == 2)
        {
            this.transform.localScale = Vector3.one * 2;
            isSuper = true;
        }
        else if (this.health == 0)
        {
            LevelController.current.onRabitDeath(this);
            isSuper = false;
        }
    }

    public void becomeSuper()
    {
        if (!isSuper)
        {
            isSuper = true;
            transform.localScale += new Vector3(0.5f, 0.5f, 0);
        }
    }

    public void BombTouch()
    {
        Animator animator = GetComponent<Animator>();
        if (isSuper)
        {
            isSuper = false;
            transform.localScale += new Vector3(-0.5F, -0.5f, 0);
        }
        else
        {
            animator.SetBool("death", true);
            LevelController.current.onRabitDeath(this);
            animator.SetBool("death", false);

        }
    }
}