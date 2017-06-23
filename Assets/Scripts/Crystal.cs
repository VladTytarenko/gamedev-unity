using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Collectable
{

    public int color = 0;

    protected override void hitting(HeroRabit rabit)
    {
        typeCrystal(LevelController.current.addCrystal(color));
        this.CollectedHide();
    }

    void typeCrystal(int crystal)
    {
        if (crystal == 1)
            FirstCrystal.crystal.findCrystal();
        else if (crystal == 2)
            SecondCrystal.crystal.findCrystal();
        else if (crystal == 3)
            ThirdCrystal.crystal.findCrystal();
    }
}