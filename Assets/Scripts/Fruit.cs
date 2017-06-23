using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Collectable
{

    protected override void hitting(HeroRabit rabit)
    {
        FruitsCount.count.countFruitsLabel(LevelController.current.addFruit());
        this.CollectedHide();
    }
}