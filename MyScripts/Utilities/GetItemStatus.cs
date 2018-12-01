using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChorusChart
{
    Ambient,
    Experimental,
    Hip,
    Loud
};

public class GetItemStatus : MonoBehaviour
{

    public static string BonusText(string name, int bonus)
    {

        return (bonus != 0) ? (name + " + " + bonus) : null;

    }

    public static string TotalBonusText(Instrument i)
    {

        return BonusText("\nLoud", i.loudBonus) +
                       BonusText("\nAmbient", i.ambientBonus) +
                       BonusText("\nExperimental", i.experimentalBonus) +
                       BonusText("\nHip", i.hipBonus);
    }

    public static string PriceText(Instrument i)
    {
        return "$ " + i.price.ToString();
    }
}