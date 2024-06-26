using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class GamePlayManager : MonoBehaviour
{
    public GameObject Player { get; private set; }
    public GameObject map;
    float fallingSpeed = 2f;
    public GameObject clearPanel;
    public GameObject failPanel;
    GameObject bullets;
    public PlayerController controller;
    public PlayerInteraction interaction;

    public InputManager inputManager { get; private set; }
    public Action<PointerEventData>[] skills = new Action<PointerEventData>[] {null,null,null,null};
    public Action GameOver;
    public Action GameClear;

    private void Awake()
    {
        Managers.Game = this;
        Setup();
    }
    void Setup()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        map = GameObject.FindGameObjectWithTag("Map");
        inputManager = Player.GetComponent<InputManager>();
        inputManager.Init();
        GameOver += () => Time.timeScale = 0.0f;

        bullets = Managers.Resource.Instantiate(nameof(Tags.Bullets));
        Managers.Pool.CreatePool(bullets,10);

        InvokeRepeating(nameof(GenerateBullets),2f,1f);
    }
    void GenerateBullets()
    {
        StartCoroutine(LoadBulletsFromPool());
    }
    IEnumerator LoadBulletsFromPool()
    {
        Poolable p = Managers.Pool.Pop(bullets,Managers.Pool.GetPoolParent(bullets));
        yield return new WaitForSeconds(5f);
        Managers.Pool.Push(p.gameObject);
    }
    public void CallGameOverLogic()
    {
        GameOver?.Invoke();
        Time.timeScale = 0.0f;
    }

    public void CallGameClearLogic()
    {
        GameClear?.Invoke();
        Time.timeScale = 0.0f;
    }
    public void StartGame()
    {
        Time.timeScale = 1.0f;
        map.GetComponent<Rigidbody>().velocity = fallingSpeed * Vector3.down;
        Player.transform.position = 
            new Vector3(
                Player.transform.position.x,
                Util.FindChild<Transform>(Managers.Game.map, nameof(Tags.StartLine)).position.y,
                Player.transform.position.z);
        Player.transform.GetChild(0).transform.position =
            Player.transform.position + 3*Vector3.up + Vector3.back;
    }
}