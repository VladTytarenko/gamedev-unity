using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    protected virtual void hitting(HeroRabit rabit){}

    void OnTriggerEnter2D(Collider2D collider)
    {

        HeroRabit rabit = collider.GetComponent<HeroRabit>();
        if (rabit != null)
        {
            this.hitting(rabit);
        }

    }
    public void CollectedHide()
    {
        Destroy(this.gameObject);
    }
}