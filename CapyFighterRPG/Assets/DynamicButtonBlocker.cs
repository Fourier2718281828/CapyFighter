using UnityEngine;
using UnityEngine.UI;
using System;

public class DynamicButtonBlocker : MonoBehaviour
{
    public Predicate<int> CanInteract;
    private const int _Arg = 0;
    Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Update()
    {
        if(CanInteract == null) return;

        if (CanInteract.Invoke(_Arg))
        {
            _button.interactable = true;
        }
        else
        {
            _button.interactable = false;
        }
    }
}
