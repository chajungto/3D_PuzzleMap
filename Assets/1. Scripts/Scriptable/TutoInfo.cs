using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TutoInfo", menuName = "Scriptable Object/TutoInfo", order = int.MaxValue)]

public class TutoInfo : ScriptableObject
{
    [SerializeField]
    private int stage;
    public int Stage { get { return stage; } }

    [SerializeField]
    private string tutoText;
    public string TutoText { get { return tutoText; } }

}
