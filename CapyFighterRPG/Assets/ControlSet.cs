using UnityEngine;
using UnityEngine.UI;
using System;

public class ControlSet : MonoBehaviour
{
    [SerializeField] private GameObject _controlsCanvas;
    [SerializeField] private Button _moveUp;
    [SerializeField] private Button _moveDown;
    [SerializeField] private Button _attack;
    [SerializeField] private Button _equipShield;
    [SerializeField] private Button _superAttack;

    public Button MoveUpButton => _moveUp;
    public Button MoveDownButton => _moveDown;
    public Button AttackButton => _attack;
    public Button EquipShieldButton => _equipShield;
    public Button SuperAttackButton => _superAttack;

    private void Appear() => _controlsCanvas.SetActive(true);

    private void Disappear() => _controlsCanvas.SetActive(false);

}
