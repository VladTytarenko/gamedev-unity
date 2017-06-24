using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HeroRabit : MonoBehaviour
{
    public static HeroRabit currentRabit = null;
    public float maxJumpTime = 2f;
    public float jumpSpeed = 2f;
    public float speed = 1;
    public int maxHealth = 2;
    Transform rabitParent = null;
    Rigidbody2D body = null;
    Animator animator;
    float JumpTime = 0f;
    bool isGrounded = false;
    bool jumpActive = false;
    bool isRabitDie = false;
    bool isSuper = false;
    int health = 1;
    int lifes = 3;

    LevelInfo stat = new LevelInfo();

    //
    public AudioClip groundSound;
    AudioSource groundSource;
    //

    void Update() { }

    static void SetNewParent(Transform obj, Transform new_parent)
    {
        if (new_parent != obj.transform.parent)
        {
            Vector3 position = obj.transform.position;
            obj.transform.parent = new_parent;
            obj.transform.position = position;
        }
    }

    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rabitParent = this.transform.parent;
        LevelController.current.setStartPosition(this.transform.position);

        //
        groundSource = gameObject.AddComponent<AudioSource>();

        //
    }

    void Awake()
    {
        currentRabit = this;
    }

    void FixedUpdate()
    {
        Animator animator = GetComponent<Animator>();
        Vector3 from = transform.position + Vector3.up * 0.3f;
        Vector3 to = transform.position + Vector3.down * 0.1f;
        int layer_id = 1 << LayerMask.NameToLayer("Ground");
        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);

        if (hit)
        {
            if (hit.transform != null && hit.transform.GetComponent<MovingPlatform>() != null)
            {
                SetNewParent(this.transform, hit.transform);
            }
            else
            {
                SetNewParent(this.transform, this.rabitParent);
            }
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        
        Debug.DrawLine(from, to, Color.red);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            this.jumpActive = true;
        }
        if (this.jumpActive)
        {
            
            if (Input.GetButton("Jump"))
            {
                this.JumpTime += Time.deltaTime;
                if (this.JumpTime < this.maxJumpTime)
                {
                    Vector2 vel = body.velocity;
                    vel.y = jumpSpeed * (1.0f - JumpTime / maxJumpTime);
                    body.velocity = vel;
                }
            }
            else
            {
                this.jumpActive = false;
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

        float value = Input.GetAxis("Horizontal");
        SpriteRenderer spr = GetComponent<SpriteRenderer>();

        if (Mathf.Abs(value) > 0)
        {
            Vector2 vec2 = body.velocity;
            vec2.x = value * speed;
            body.velocity = vec2;
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }
        if (value < 0)
        {
            spr.flipX = true;
        }
        else if (value > 0)
        {
            spr.flipX = false;
        }

    }

    public void bombTouch()
    {
        if (isSuper)
        {
            transform.localScale += new Vector3(-0.4f, -0.4f, 0);
            isSuper = false;
        }
        else
        {
            animator.SetBool("death", true);
            LevelController.current.onRabitDeath(this);
            animator.SetBool("death", false);
        }
    }

    public void becomeSuper()
    {
        if (!isSuper)
        {
            isSuper = true;
            transform.localScale += new Vector3(0.4f, 0.4f, 0);
        }
    }

    public void healthChanging()
    {
        if (this.health == 0)
        {
            animator.SetBool("death", true);
            LevelController.current.onRabitDeath(this);
            isSuper = false;
            animator.SetBool("death", false);
        }
        else if (this.health == 1)
        {
            this.transform.localScale = Vector3.one;
        }
        else if (this.health == 2)
        {
            this.transform.localScale = Vector3.one * 2;
            isSuper = true;
        }
    }

    public void addHealth(int num)
    {
        this.health += num;
        if (this.health > maxHealth)
            this.health = maxHealth;
        this.healthChanging();
    }

    public void removeHealth(int number)
    {
        this.health -= number;
        if (this.health < 0)
            this.health = 0;
        this.healthChanging();
    }

    public Vector3 btnLft()
    {
        BoxCollider2D boxcol = this.GetComponent<BoxCollider2D>();

        Vector3 world = transform.TransformPoint(boxcol.offset);
        float rbot = world.y - (boxcol.size.y / 2f);
        float rlef = world.x - (boxcol.size.x / 2f);

        return new Vector3(rlef, rbot, 0f);
    }

    public void deadRabit()
    {
        if (!isRabitDie)
            StartCoroutine(rebirthLater());
    }

    public bool isRabitDied()
    {
        return isRabitDie;
    }

    IEnumerator rebirthLater()
    {
        //removeLifes();
        healthChanging();
        Animator animator = GetComponent<Animator>();
        animator.SetBool("die", true);
        isRabitDie = true;

        yield return new WaitForSeconds(2f);

        if (checkLife())
        {
            isRabitDie = false;
            isSuper = false;
            animator.SetBool("die", false);
            this.transform.localScale = this.transform.localScale;
            LevelController.current.onRabitDeath(this);
        }
    }

    public void removeLifes()
    {
        if (lifes == 3)
            Life3.life.loseLife();
        else if (lifes == 2)
            Life2.life.loseLife();
        else if (lifes == 1)
            Life1.life.loseLife();
        lifes--;
    }

    public bool checkLife()
    {
        if (lifes < 1)
        {
            SceneManager.LoadScene("LevelMenu");
            return false;
        }
        return true;
    }

    public void saveStats()
    {
        string level = SceneManager.GetActiveScene().name;

        string lastLevelStr = PlayerPrefs.GetString(level);
        LevelInfo lastStats = JsonUtility.FromJson<LevelInfo>(lastLevelStr);
        if (lastStats == null)
        {
            lastStats = new LevelInfo();
        }

        if (FruitsCount.max == LevelController.current.getFruits())
            stat.fruits = (true || lastStats.fruits);

        bool hasCrystals = true;
        for (int i = 0; i < LevelController.current.getCrystalArr().Length && hasCrystals; i++)
        {
            if (!LevelController.current.getCrystalArr()[i]) hasCrystals = false;
        }
        stat.crystals = (hasCrystals || lastStats.crystals);

        if (!isRabitDie)
            stat.levelFinish = (true || lastStats.levelFinish);

        string str = JsonUtility.ToJson(stat);
        PlayerPrefs.SetString(level, str);
    }
}