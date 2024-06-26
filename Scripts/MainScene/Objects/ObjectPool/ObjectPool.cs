using System.Collections.Generic;
using UnityEngine;
using System;

using KeyType = System.String;


public class ObjectPool : MonoBehaviour
{
    [SerializeField] private List<PoolObjectData> objects = new List<PoolObjectData>();
    [SerializeField] private List<PoolObjectData> skillEffects = new List<PoolObjectData>();
    [SerializeField] private List<PoolObjectData> temp = new List<PoolObjectData>();

    public Dictionary<string, Sprite> sprites = new();

    private Dictionary<KeyType, GameObject> sampleDict;              // Key - ������ ������Ʈ ����
    private Dictionary<KeyType, PoolObjectData> dataDict;            // Key - Ǯ ����
    private Dictionary<KeyType, Stack<GameObject>> poolDict;         // Key - Ǯ
    private Dictionary<GameObject, Stack<GameObject>> clonePoolDict; // ������ ���ӿ�����Ʈ - Ǯ

    public static ObjectPool instance;

    private void Awake()
    {
        instance = this;
        Init();
    }

    private void Init()
    {
        int len = objects.Count + temp.Count + skillEffects.Count;
        if (len == 0) return;

        // 1. Dictionary ����
        sampleDict = new Dictionary<KeyType, GameObject>(len);
        dataDict = new Dictionary<KeyType, PoolObjectData>(len);
        poolDict = new Dictionary<KeyType, Stack<GameObject>>(len);
        clonePoolDict = new Dictionary<GameObject, Stack<GameObject>>(len * PoolObjectData.INITIAL_COUNT);

        // 2. Data�κ��� ���ο� Pool ������Ʈ ���� ����
        foreach (var data in objects)
        {
            RegisterInternal(data);
        }
        foreach (var data in temp)
        {
            RegisterInternal(data);
        }
        for (int i = 0; i < skillEffects.Count; i++)
        {
            RegisterInternal(skillEffects[i]);
        }
    }

    /// <summary> Pool �����ͷκ��� ���ο� Pool ������Ʈ ���� ��� </summary>
    private void RegisterInternal(PoolObjectData data)
    {
        // �ߺ� Ű�� ��� �Ұ���
        if (poolDict.ContainsKey(data.key))
        {
            return;
        }

        // 1. ���� ���ӿ�����Ʈ ����, PoolObject ������Ʈ ���� Ȯ��
        GameObject sample = Instantiate(data.prefab);
        sample.name = data.prefab.name;
        sample.SetActive(false);

        // 2. Pool Dictionary�� Ǯ ���� + Ǯ�� �̸� ������Ʈ�� ����� ��Ƴ���
        Stack<GameObject> pool = new Stack<GameObject>(data.maxObjectCount);
        for (int i = 0; i < data.initialObjectCount; i++)
        {
            GameObject clone = Instantiate(data.prefab);
            clone.SetActive(false);
            pool.Push(clone);

            clonePoolDict.Add(clone, pool); // Clone-Stack ĳ��
        }

        // 3. ��ųʸ��� �߰�
        sampleDict.Add(data.key, sample);
        dataDict.Add(data.key, data);
        poolDict.Add(data.key, pool);
    }

    /// <summary> ���� ������Ʈ �����ϱ� </summary>
    private GameObject CloneFromSample(KeyType key)
    {
        if (!sampleDict.TryGetValue(key, out GameObject sample)) return null;

        return Instantiate(sample);
    }

    /// <summary> Ǯ���� �������� </summary>
    public GameObject Spawn(KeyType key)
    {
        // Ű�� �������� �ʴ� ��� null ����
        if (!poolDict.TryGetValue(key, out var pool))
        {
            return null;
        }

        GameObject go;

        // 1. Ǯ�� ��� �ִ� ��� : ��������
        if (pool.Count > 0)
        {
            go = pool.Pop();
        }
        // 2. ��� ���� ��� ���÷κ��� ����
        else
        {
            go = CloneFromSample(key);
            clonePoolDict.Add(go, pool); // Clone-Stack ĳ��
        }

        go.SetActive(true);
        go.transform.SetParent(null);

        return go;
    }

    /// <summary> Ǯ�� ����ֱ� </summary>
    public void Despawn(GameObject go)
    {
        // ĳ�̵� ���ӿ�����Ʈ�� �ƴ� ��� �ı�
        if (!clonePoolDict.TryGetValue(go, out var pool))
        {
            Destroy(go);
            return;
        }

        // ����ֱ�
        go.SetActive(false);
        //go.transform.SetParent(null); // �����߰�
        pool.Push(go);
    }
}
