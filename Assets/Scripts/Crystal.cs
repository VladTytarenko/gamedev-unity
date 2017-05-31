using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Collectable
{

    protected override void OnRabitHit(HeroRabit rabit)
    {
        LevelController.current.addCristal(1);
        this.CollectedHide();
    }
}