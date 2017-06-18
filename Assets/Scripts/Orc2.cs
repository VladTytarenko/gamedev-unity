using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc2 : MonoBehaviour {

    public enum Mode {
        GoToA,
        GoToB,
        Attack
    }

    public Vector3 moveBy = new Vector3(0.5f, 0, 0);
    public Vector3 speed  = new Vector3(2.0f, 0, 0);

    Vector3 forward;
    Vector3 backward;

    Vector3 pointA;
    Vector3 pointB;

    Mode mode = Mode.GoToB;
    Mode last = Mode.GoToB;

    public float radius = 8.0f;
    public GameObject prefabCarrot;

    bool isKiller = false;
    bool isHidden = false;
    bool inZone   = false;

    float reload = 0;

    void Start() {
        this.pointA = this.transform.position;
        this.pointB = this.pointA + moveBy;
        forward  =  speed;
        backward = -speed;
    }

    void Update() {
        if(isHidden) return;
        if(isKiller) return;

        Vector3 myPosition = this.transform.position;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        float value = getDirection(myPosition);
        if(value > 0) {
            sr.flipX = true;
            speed = forward;
        } else if(value < 0) {
            sr.flipX = false;
            speed = backward;
        }

        if(mode == Mode.GoToB) {
            if(isArrived(myPosition, pointB) || myPosition.x > pointB.x) {
                mode = Mode.GoToA;
            }
        } else if(mode == Mode.GoToA) {
            if(isArrived(myPosition, pointA) || myPosition.x < pointA.x) {
                mode = Mode.GoToB;
            }
        } else if(mode == Mode.Attack) {
            if(inZone && (Time.time - reload > 2f)) {
                launchCarrot(value);
                reload = Time.time;
            }
        }

        Animator animator = GetComponent<Animator>();
        if(mode != Mode.Attack && value != 0) {
            animator.SetBool("run", true);
        } else {
            animator.SetBool("run", false);
        }

        if(mode != Mode.Attack && value != 0) {
            this.transform.position += speed * Time.deltaTime;
        }
    }

    void launchCarrot(float direction) {
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("attack");

        GameObject obj = GameObject.Instantiate(this.prefabCarrot);

        obj.transform.position = this.transform.position + new Vector3(0, 0.7f, 0);

        Carrot carrot = obj.GetComponent<Carrot>();
        carrot.launch(direction);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(isKiller) return;

        HeroRabit rabit = collider.GetComponent<HeroRabit>();

        if(rabit != null) {
            if(topLft().y > rabit.btnLft().y)
                StartCoroutine(killRabit(rabit));
            else
                StartCoroutine(killedByRabit());
        }
    }

    IEnumerator killRabit(HeroRabit rabit) {
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("attack2");
        rabit.deadRabit();
        isKiller = true;

        yield return new WaitForSeconds(2f);

        isKiller = false;
    }

    IEnumerator killedByRabit() {
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("die");
        isHidden = true;

        yield return new WaitForSeconds(0.8f);

        Destroy(this.gameObject);
    }

    bool checkRabit(Vector3 rabitPos) {
        if(Mathf.Abs(this.transform.position.x - rabitPos.x) < radius) {
            return true;
        }
        return false;
    }

    float getDirection(Vector2 myPos) {
        Vector3 rabitPos = HeroRabit.currentRabit.transform.position;
        if(isKiller) return 0;
        if(checkRabit(rabitPos)) {
            inZone = true;
            if(mode != Mode.Attack) last = mode;
               mode = Mode.Attack;
            if(myPos.x < rabitPos.x) return  1;
            else                     return -1;
        } else if(mode == Mode.Attack) {
            mode = last;
        }
        if(speed.x == 0) return 0;
        if(mode == Mode.GoToA) { return -1; }
        if(mode == Mode.GoToB) { return  1; }
        inZone = false;
        return 0;
    }

    bool isArrived(Vector3 pos, Vector3 target) {
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos, target) < 0.02f;
    }

    Vector3 topLft() {
        BoxCollider2D boxcol = this.GetComponent<BoxCollider2D>();

        Vector3 world = transform.TransformPoint(boxcol.offset);
        float top = world.y + (boxcol.size.y / 2f);
        float lef = world.x - (boxcol.size.x / 2f);

        return new Vector3(lef, top, 0f);
    }
}
