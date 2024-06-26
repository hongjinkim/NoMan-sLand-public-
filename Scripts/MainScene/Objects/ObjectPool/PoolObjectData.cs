using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using KeyType = System.String;

[System.Serializable]
public class PoolObjectData
{
    public const int INITIAL_COUNT = 10;
    public const int MAX_COUNT = 50;

    public KeyType key;
    public GameObject prefab;
    public int initialObjectCount = 0; // ������Ʈ �ʱ� ���� ����
    public int maxObjectCount = 0;     // ť ���� ������ �� �ִ� ������Ʈ �ִ� ����
}
