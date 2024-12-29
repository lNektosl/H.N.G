using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySO : ScriptableObject{
    public string abilityName;
    public string description;
    public int energy;

    public Sprite sprite;
    public AbilityType type;
}
