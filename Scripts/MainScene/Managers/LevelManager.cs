using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    public List<LevelSO> levelSO;

    private int level;
    private GameObject map;
    private Vector3 center;
    private Vector3 size;

    private void Awake()
    {
        UpdateLevel();
    }

    private void UpdateLevel()
    {
        level = Managers.Data.lvl;
        map = Instantiate(levelSO[level].map);

        foreach(GameObject obj in levelSO[level].objects)
        {
            Instantiate(obj);
        }

        MakeObstacles();
    }

    private void MakeObstacles()
    {
        center = map.transform.position;
        size = map.transform.localScale* 0.9f;
        foreach(ObstacleData data in levelSO[level].data)
        {
            for(int i=0; i<data.quantity; i++)
            {
                GameObject obj = Instantiate(data.prefab, map.transform);
                obj.transform.position = GetRandomPositionInArea();

                Obstacle obstacle;
                if (obj.CompareTag(nameof(Tags.Obstacle)))
                    obstacle = Util.GetOrAddComponent<Obstacle>(obj);
                else if (obj.CompareTag(nameof(Tags.Wire)))
                    obstacle = Util.GetOrAddComponent<Wire>(obj);
                else
                    return;

                int idx = 0;
                while (CheckOtherObstacles(obstacle, data.checkRadius))
                {
                    obj.transform.position = GetRandomPositionInArea();
                    obstacle = obj.GetComponent<Obstacle>();
                    idx++;
                    if(idx > 100)
                    {
                        Debug.Log("~~~");
                        break;
                    }
                }
            }
        }
    }

    private Vector3 GetRandomPositionInArea()
    {
        Vector3 pos = new Vector3(
            Random.Range(center.x-10/2,center.x+10/2),
            Random.Range(center.y-30/4,center.y+30/2),
//            Random.Range(center.x-size.x/2,center.x+size.x/2),
//            Random.Range(center.y-size.y/2,center.y+size.y/2),
            0f);
        return pos;
    }
    private bool CheckOtherObstacles(Obstacle obstacle, float checkRadius)
    {
        Collider[] colls;
        colls = Physics.OverlapSphere(obstacle.transform.position,checkRadius);
        foreach(Collider coll in colls)
        {
            if (coll.CompareTag(nameof(Tags.Obstacle)) || coll.CompareTag(nameof(Tags.Wire)))
                return true;
        }
        return false;
    }

}