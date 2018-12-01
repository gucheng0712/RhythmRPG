using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "GameData/Instrument/New Instrument", fileName = "New Instrument.asset")]
public class Instrument : ScriptableObject
{

    [Header("Instrument Art")]
    public Sprite icon;
    public Sprite lockedIcon;
    public Sprite unlockedIcon;
    public Sprite iconWithPlayer;

    [Space(20)]

    [Header("Instrument Info")]
    public int id;
    public string itemName;
    public string description;
    public int price;
    public string abilityName;
    public string soldMsg;
    [Space(20)]

    [Header("Instrument Stats")]
    public ChorusChart chorusChart;
    public int ambientBonus;
    public int experimentalBonus;
    public int hipBonus;
    public int loudBonus;
}
