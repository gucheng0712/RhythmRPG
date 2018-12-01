using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerClass
{
    Low,
    Middle,
    High
}

[System.Serializable]
public class PlayerData
{
    public PlayerClass playerClass;

    public Sprite playerIcon;

    public string playerPerformLocation;

    public RuntimeAnimatorController beginAnimator;
    public RuntimeAnimatorController middleAnimator;
    public RuntimeAnimatorController endAnimator;
    public Sprite beginPlayerSprite;
    public Sprite middlePlayerSprite;
    public Sprite endPlayerSprite;

    public PlayerData()
    {
    }

    public PlayerData(PlayerClass _playerClass, Sprite _playerIcon)
    {
        playerClass = _playerClass;
        playerIcon = _playerIcon;
    }





}
