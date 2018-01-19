using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberScript : MonoBehaviour {

    public enum BorderType { BomberBorder, OnagrBorder, FlyBorder }
    public enum EnemyLivePhase { Walk, Attack, Die}

    public const string BOMBER_BORDER = "BomberBorder";
    public const string ONAGR_BORDER = "OnagrBorder";
    public const string FLY_BORDER = "FlyBorder";

    public float Speed = 1f;
    [SerializeField]
    private BorderType borderType;
    [SerializeField]
    private float HP = 50f;
    [SerializeField]
    private int ScoreCoins = 10;
    [SerializeField]
    private int AttackPower = 2;
    [SerializeField]
    private float AttackSpeed = 1f;
    private float currentHP;
    private string BorderName;

    private EnemyLivePhase enemyLivePhase = EnemyLivePhase.Walk;

    private HPbarEnemyScript hpBarScript;
    private Animator anim;
    private Rigidbody2D rbody;

    private void Awake()
    {
        currentHP = HP;
        hpBarScript = gameObject.transform.GetChild(0).GetComponent<HPbarEnemyScript>();
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        switch (borderType)
        {
            case BorderType.BomberBorder:
                BorderName = BOMBER_BORDER;
                break;
            case BorderType.OnagrBorder:
                BorderName = ONAGR_BORDER;
                break;
            case BorderType.FlyBorder:
                BorderName = FLY_BORDER;
                break;
            default:
                BorderName = BOMBER_BORDER;
                break;
        }
    }

    public void GetDamage(float _value)
    {
        if (enemyLivePhase != EnemyLivePhase.Die)
        {
            UpdateHP(_value);
        }
    }

    private void UpdateHP(float _value)
    {
        if (currentHP > 0)
        {
            currentHP -= _value;
            hpBarScript.UpdateHPBar(CalculatePercent());
        }
        if (currentHP <= 0)
        {
            enemyLivePhase = EnemyLivePhase.Die;
            anim.SetTrigger("Die");
            GameManager.instance.AllEnemyList.Remove(gameObject);
            GameManager.instance.UpdateScore(ScoreCoins);
            Destroy(gameObject, 0.7f);
        }
    }

    public void SetFalseActive()
    {
        enemyLivePhase = EnemyLivePhase.Die;
    }

    private void Update()
    {
        if (enemyLivePhase == EnemyLivePhase.Walk)
        {
            rbody.velocity = new Vector2(Speed, 0f);
        } else if (enemyLivePhase == EnemyLivePhase.Die)
        {
            rbody.velocity = new Vector2(0f, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((enemyLivePhase == EnemyLivePhase.Walk) && (collision.tag == BorderName))
        {
            enemyLivePhase = EnemyLivePhase.Attack;
            rbody.velocity = new Vector2(0f, 0f);
            anim.SetTrigger("Kick");
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        if (enemyLivePhase != EnemyLivePhase.Die) {
            yield return new WaitForSeconds(AttackSpeed);
            GameManager.instance.UpdateCastleHP(AttackPower);
            StartCoroutine(Attack());
        }
    }

    private int CalculatePercent()
    {
        return (int)(currentHP/ HP * 100f);
    }
}
