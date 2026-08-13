using System;
using UnityEngine;

[Serializable]
public class PlayerData : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private string playerName;


    public int Id => id;
    public string PlayerName => playerName;
}