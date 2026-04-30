using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;

[CreateAssetMenu](fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
public class ScriptableObjectItem : ScriptableObject

{
    public int _plusHealth;
    public int _plusHunger;
    public int _powerUp;
}
