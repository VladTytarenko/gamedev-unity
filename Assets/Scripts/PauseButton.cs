using System.Collections;
using UnityEngine.Events;
using UnityEngine;

public class PauseButton : MonoBehaviour
{

    public UnityEvent signalOnClick = new UnityEvent();

    public void _onClick()
    {
        this.signalOnClick.Invoke();
    }
}
