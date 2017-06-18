using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectable
{
    protected override void hitting(HeroRabit rabit)
    {
        this.CollectedHide();
        rabit.bombTouch();
    }

}
