using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Config/New Game Config", order = 0)]
public class GameConfig : ScriptableObject
{
    public int StartMoney;
    public AudioConfig[] PopcornSounds;
    public MoneyUpgrades[] MoneyUpgrades;
    public SwingUpgrades[] SwingUpgrades;
    public SwingCountUpgrades[] SwingCountUpgrades;
    public PhysicMaterial OutPhysicMaterial, BasePhysicMaterial;
}

[System.Serializable]
public class AudioConfig
{
    public AudioClip Audio;
    public float Volume;
}
[System.Serializable]
public struct MoneyUpgrades
{
    public float MoneyPerSpawn;
    public int Cost;
}

[System.Serializable]
public struct SwingUpgrades
{
    public int Cost;
}
[System.Serializable]
public struct SwingCountUpgrades
{
    public int Cost;
}
