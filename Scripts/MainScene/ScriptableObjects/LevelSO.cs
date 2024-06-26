using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level_", menuName ="Scriptables/Level",order =1)]
public class LevelSO : ScriptableObject
{
    public int level;
    public List<ObstacleData> data;
    public GameObject map;
    public List<GameObject> objects;
    public AudioClip audio;
}

[Serializable]
public class ObstacleData
{
    public GameObject prefab;
    public float checkRadius;
    public int quantity;
}
