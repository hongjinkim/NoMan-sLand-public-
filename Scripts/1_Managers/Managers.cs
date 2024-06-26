using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Resources;
using TMPro.EditorUtilities;
using UnityEditorInternal;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers instance = null;
    static Managers Instance { get { Init(); return instance; } }

    DataManager data = new DataManager();
    GamePlayManager game;
    PoolManager pool = new PoolManager();
    ResourceManager resource = new ResourceManager();
    SoundManager sound = new SoundManager();
    UIManager ui = new UIManager();

    public static GameObject SoundRoot { get; private set; }

    public static DataManager Data { get { return instance?.data; } }
    public static GamePlayManager Game { get { return instance?.game; } set { instance.game = value; } }
    public static PoolManager Pool { get { return Instance?.pool; } }
    public static ResourceManager Resource { get { return Instance?.resource; } }
    public static SoundManager Sound { get { return Instance?.sound; } }
    public static UIManager UI { get { return Instance?.ui; } }

    // Start is called before the first frame update
    void Awake()
    {
        Init();
    }

    static void Init()
    {
        if(instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if(go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            instance = go.GetComponent<Managers>();

            instance.data.Init();
            instance.pool.Init();
            instance.sound.Init();
            SoundRoot = GameObject.Find("@SoundRoot");
        }
    }
}
