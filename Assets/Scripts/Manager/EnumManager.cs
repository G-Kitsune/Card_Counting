using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnumPlayer
{
    Player,
    AI
}

public enum EnumPhase
{
    Draw,
    TakeOrDiscard,
    TakeEffect,
    RecycleCard,
    EndTurn,
    AdditionalTurn
}

public enum EnumCard
{
    Back = -1,
    NormalSheep,
    NormalWolf,
    GroupSheep,
    GroupWolf,
    ShadowSheep,
    ShadowWolf,
    NormalDog
}

public enum EnumReason
{
    TurnStart,
    Draw,
    TakeOrDiscard,
}

public enum EnumGameEnd
{
    NotYet,
    PlayerWin,
    AiWin
}