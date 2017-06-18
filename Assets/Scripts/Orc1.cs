using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc1 : MonoBehaviour {

    public enum Mode {
        GoToA,
        GoToB,
        Attack
    }

    public Vector3 moveBy = new Vector3(2, 0);
    public Vector3 speed  = new Vector3(2, 0);

    Vector3 forward;
    Vector3 backward;

    Mode mode = Mode.GoToB;
    Mode last = Mode.GoToB;

    Vector3 pointA;
    Vector3 pointB;

    bool isKiller = false;
    bool isHidden = false;

    void Start () {
        this.pointA = this.transform.position;
        this.pointB = this.pointA + moveBy;
        forward  =  speed;
        backward = -speed;
    }
	
	void Update () {
        if(isHidden) return;

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

        }

        Animator animator = GetComponent<Animator>();
        if(value != 0) {
            animator.SetBool("run", true);
        } else {
            animator.SetBool("run", false);
        }

        if(value != 0) {
            this.transform.position += speed * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
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
        animator.SetTrigger("attack");
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
        if(rabitPos.x > Mathf.Min(pointA.x, pointB.x)
        && rabitPos.x < Mathf.Max(pointA.x, pointB.x)) {
            return true;
        }
        return false;
    }

    float getDirection(Vector3 myPos) {
        Vector3 rabitPos = HeroRabit.currentRabit.transform.position;
        if(isKiller) return 0;
        if(checkRabit(rabitPos)) {
            if(mode != Mode.Attack) last = mode;
            mode = Mode.Attack;
            if(myPos.x < rabitPos.x) return 1; 
            else                     return -1;
        } else if(mode == Mode.Attack) {
            mode = last;
        }
        if(mode == Mode.GoToA) { return -1; }
        if(mode == Mode.GoToB) { return  1; }
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
