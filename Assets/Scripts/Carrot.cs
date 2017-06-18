using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Bomb
{

    bool isLaunched = false;

    public float speed = 4;

    Vector3 vspeed = new Vector3();

    void Start()
    {
        StartCoroutine(destroyLater());
    }

    void Update()
    {
        if (isLaunched)
        {
            this.transform.position += vspeed * Time.deltaTime;
        }
    }

    protected void OnRabitHit(HeroRabit rabit)
    {
        this.CollectedHide();
        rabit.bombTouch();
    }

    public void launch(float dir)
    {
        vspeed = new Vector3(speed * dir, 0, 0);

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (dir < 0) sr.flipX = true;

        isLaunched = true;
    }

    IEnumerator destroyLater()
    {
        yield return new WaitForSeconds(1.7f);
        Destroy(this.gameObject);
    }
}