using UnityEngine;

[CreateAssetMenu(fileName = "UnitObject", menuName = "Unit")]
public class Unit : ScriptableObject
{
    public GameObject Placeholder;
    public float Damage;

    public string AttackAnimationName;
}
