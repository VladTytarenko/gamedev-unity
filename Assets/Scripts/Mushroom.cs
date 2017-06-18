using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mushroom : Collectable
{
    protected override void hitting(HeroRabit rabit)
    {
        rabit.becomeSuper();
        this.CollectedHide();
    }
}