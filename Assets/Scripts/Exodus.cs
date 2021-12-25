using System;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "New Сonsequence", menuName = "Сonsequence")]
public class Exodus : ScriptableObject
{
    
    public string nameOfExodus;
    public byte exodusInt;
    public bool spawnNext;
    public bool HideCurrentCharacter;
}
