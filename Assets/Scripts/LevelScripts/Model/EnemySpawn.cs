using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    public enum SpawnLivePhase { Starting, Spawning, Ending}
    public SpawnLivePhase spawnLivePhase;

    public GameObject Bomber;
    public GameObject Onagr;
    public GameObject Fly;

    public GameObject BomberSpawn;
    public GameObject FlySpawn;

    private Transform bomberSpawnTransform;
    private Transform flySpawnTransform;

    private int level;
    private int EmemyBomberOnLevel;
    private int EmemyOnagrOnLevel;
    private int EmemyFlyOnLevel;
    private int EnemyOnLevel;

    private void Awake()
    {
        spawnLivePhase = SpawnLivePhase.Starting;
        bomberSpawnTransform = BomberSpawn.transform;
        flySpawnTransform = FlySpawn.transform;
    }

    public void StartSpawnEnemy(int _level)
    {
        spawnLivePhase = SpawnLivePhase.Starting;
        level = _level;
        CalculateParametrs();
    }

    private void CalculateParametrs()
    {
        if (level == 1)
        {
            EmemyBomberOnLevel = level*10;
            EmemyOnagrOnLevel = 0;
            EmemyFlyOnLevel = 0;
        }
        if (level > 1 && level <= 3)
        {
            EmemyBomberOnLevel = level * 7;
            EmemyOnagrOnLevel = level * 2;
            EmemyFlyOnLevel = 0;
        }
        if (level > 3)
        {
            EmemyBomberOnLevel = level * 6;
            EmemyOnagrOnLevel = level * 2;
            EmemyFlyOnLevel = level * 3;
        }
        EnemyOnLevel = EmemyBomberOnLevel + EmemyOnagrOnLevel + EmemyFlyOnLevel;
        spawnLivePhase = SpawnLivePhase.Spawning;
        StartCoroutine(StartSpawnBomber(EmemyBomberOnLevel, 4f));
        StartCoroutine(StartSpawnOnagr(EmemyOnagrOnLevel, 8f));
        StartCoroutine(StartSpawnFly(EmemyFlyOnLevel, 2f));
    }

    IEnumerator StartSpawnBomber(int _count, float _delayTime)
    {
        yield return new WaitForSeconds(_delayTime);
        for (int i = 0; i < _count; i++)
        {
            if (GameManager.instance.gamePhase != GameManager.GamePhase.GameOver) {
                GameObject bomber = Instantiate(Bomber, new Vector3(bomberSpawnTransform.position.x, bomberSpawnTransform.position.y + UnityEngine.Random.Range(-1f, 1f), 0), bomberSpawnTransform.rotation);
                bomber.GetComponent<SpriteRenderer>().sortingOrder += i;
                GameManager.instance.AllEnemyList.Add(bomber);
                EnemyOnLevel--;
                if (EnemyOnLevel <= 0)
                {
                    spawnLivePhase = SpawnLivePhase.Ending;
                }
                yield return new WaitForSeconds(UnityEngine.Random.Range(3f, 8f));
            } else
            {
                break;
            }
        }
    }
    IEnumerator StartSpawnOnagr(int _count, float _delayTime)
    {
        yield return new WaitForSeconds(_delayTime);
        for (int i = 0; i < _count; i++)
        {
            if (GameManager.instance.gamePhase != GameManager.GamePhase.GameOver)
            {
                GameObject onagr = Instantiate(Onagr, new Vector3(bomberSpawnTransform.position.x, bomberSpawnTransform.position.y + UnityEngine.Random.Range(-1f, 1f), 0), bomberSpawnTransform.rotation);
                onagr.GetComponent<SpriteRenderer>().sortingOrder += i;
                GameManager.instance.AllEnemyList.Add(onagr);
                EnemyOnLevel--;
                if (EnemyOnLevel <= 0)
                {
                    spawnLivePhase = SpawnLivePhase.Ending;
                }
                yield return new WaitForSeconds(UnityEngine.Random.Range(8f, 15f));
            } else
            {
                break;
            }
        }
    }
    IEnumerator StartSpawnFly(int _count, float _delayTime)
    {
        yield return new WaitForSeconds(_delayTime);
        for (int i = 0; i < _count; i++)
        {
            if (GameManager.instance.gamePhase != GameManager.GamePhase.GameOver)
            {
                GameObject fly = Instantiate(Fly, new Vector3(flySpawnTransform.position.x, flySpawnTransform.position.y + UnityEngine.Random.Range(-1.5f, 1.5f), 0), flySpawnTransform.rotation);
                fly.GetComponent<SpriteRenderer>().sortingOrder += i;
                GameManager.instance.AllEnemyList.Add(fly);
                EnemyOnLevel--;
                if (EnemyOnLevel <= 0)
                {
                    spawnLivePhase = SpawnLivePhase.Ending;
                }
                yield return new WaitForSeconds(UnityEngine.Random.Range(3f, 6f));
            } else
            {
                break;
            }
        }
    }

}
