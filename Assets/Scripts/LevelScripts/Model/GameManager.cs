using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(UIManager))]
[RequireComponent(typeof(EnemySpawn))]
public class GameManager : MonoBehaviour {

    public enum GamePhase { Starting, Gaming, GameOver, NextLevel }

    public GamePhase gamePhase;

    public static GameManager instance = null;

    public int CastleHP { get; private set; }
    public int Score { get; private set; }
    public int Level = 0;
    public float TapPower = 10f;

    public List<GameObject> AllEnemyList= new List<GameObject>();

    [SerializeField]
    private int startCastleHP = 200;
    [SerializeField]
    private int startScore = 0;

    private CastleScript CastleScript;
    private EnemySpawn EnemySpawn;
    private UIManager uiManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        gamePhase = GamePhase.Starting;
        uiManager = GetComponent<UIManager>();
        CastleScript = FindObjectOfType<CastleScript>();
        EnemySpawn = GetComponent<EnemySpawn>();
    }

    void Start () {
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
        SetStartValue();
        uiManager.OnGetReadyPhase();
        //EnemySpawn.StartSpawnEnemy(Level);
    }
	
    public void GoNextLevel()
    {
        gamePhase = GamePhase.Starting;
        Level++;
        uiManager.UpdateUIText(Level, Score, CastleHP);
        EnemySpawn.StartSpawnEnemy(Level);
        gamePhase = GamePhase.Gaming;
    }

	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0)){
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Enemy") {
                    hit.collider.transform.gameObject.GetComponent<BomberScript>().GetDamage(TapPower);
                }
            }
        }
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Vector2 worldPointTouch = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            RaycastHit2D hit = Physics2D.Raycast(worldPointTouch, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Enemy")
                {
                    hit.collider.transform.gameObject.GetComponent<BomberScript>().GetDamage(TapPower);
                }
            }
        }
        if ((gamePhase != GamePhase.NextLevel) && (AllEnemyList.Count == 0) && (EnemySpawn.spawnLivePhase == EnemySpawn.SpawnLivePhase.Ending))
        {
            gamePhase = GamePhase.NextLevel;
            uiManager.Invoke("OnLevelClearPhase", 1f);
           // uiManager.OnLevelClearPhase();
        }
    }

    public void UpdateCastleHP(int _value)
    {
        if (gamePhase != GamePhase.GameOver) {
            CastleHP -= _value;
            uiManager.UpdateCastleHPText(CastleHP);
            CastleScript.UdpateCastleSprite(CastleHP);
            if (CastleHP <= 150)
            {
                CastleHP = 0;
                uiManager.UpdateCastleHPText(CastleHP);
                foreach (GameObject _go in AllEnemyList)
                {
                    _go.GetComponent<BomberScript>().SetFalseActive();
                }
                gamePhase = GamePhase.GameOver;
                Invoke("GameOver", 2f);
            }
        }
    }

    private void GameOver()
    {
        foreach (GameObject _go in AllEnemyList)
        {
            Destroy(_go);
        }
        AllEnemyList.Clear();
        uiManager.OnGameOverPhase();
        Debug.Log("GAME_OVER");
    }

    public void UpdateScore(int _value)
    {
        Score += _value;
        uiManager.UpdateScoreText(Score);
    }

    private void SetStartValue()
    {
        CastleHP = startCastleHP;
        Score = startScore;
        uiManager.UpdateUIText(Level, Score, CastleHP);
        CastleScript.UdpateCastleSprite(CastleHP);
        gamePhase = GamePhase.Gaming;
    }
}
