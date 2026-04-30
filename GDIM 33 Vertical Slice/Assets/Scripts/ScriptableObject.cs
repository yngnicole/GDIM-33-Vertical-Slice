using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
public class ScriptableObjectItem : ScriptableObject

{
    public int plusHealth;
    public int plusHunger;
    public int powerUp;
    public Sprite icon;
}
