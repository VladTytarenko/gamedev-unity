using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : Collectable
{

    protected override void hitting(HeroRabit rabit)
    {
        //LevelController.current.addCoins(1);
        CoinsQuantity.coins.labelText(LevelController.current.addCoins(1));
        this.CollectedHide();
    }
}