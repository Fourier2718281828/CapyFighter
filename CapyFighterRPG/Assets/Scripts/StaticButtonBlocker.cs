using UnityEngine;
using UnityEngine.UI;
using System;

public class StaticButtonBlocker : MonoBehaviour
{
    public Predicate<int> CanInteract;
    private const int _Arg = 0;

    private void Start()
    {
        Button button = GetComponent<Button>();
        if(CanInteract.Invoke(_Arg))
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }
}
