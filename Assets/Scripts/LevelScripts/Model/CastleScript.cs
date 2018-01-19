using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class CastleScript : MonoBehaviour {

    public int DestroyValue1 = 75;
    public int DestroyValue2 = 50;
    public int DestroyValue3 = 25;

    [SerializeField]
    private Sprite[] SpriteArray = new Sprite[4];
    private SpriteRenderer SpriteRenderer;

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UdpateCastleSprite(int _hp)
    {
        if (_hp > DestroyValue1)
        {
            SpriteRenderer.sprite = SpriteArray[0];
        } else if ((_hp <= DestroyValue1) && (_hp > DestroyValue2))
        {
            SpriteRenderer.sprite = SpriteArray[1];
        }
        else if ((_hp <= DestroyValue2) && (_hp > DestroyValue3))
        {
            SpriteRenderer.sprite = SpriteArray[2];
        }
        else if (_hp <= DestroyValue3)
        {
            SpriteRenderer.sprite = SpriteArray[3];
        }
    }
}
