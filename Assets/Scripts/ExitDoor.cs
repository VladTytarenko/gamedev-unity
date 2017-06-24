using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{

    public GameObject winPrefab;

    public string level = "LevelMenu";

    void OnTriggerEnter2D(Collider2D collider)
    {
        HeroRabit rabit = collider.GetComponent<HeroRabit>();

        if (rabit != null)
        {
            GameObject parent = UICamera.first.transform.parent.gameObject;
            NGUITools.AddChild(parent, winPrefab);
            HeroRabit.currentRabit.enabled = false;
            Destroy(HeroRabit.currentRabit.GetComponent<Rigidbody2D>());
        }
    }

}
