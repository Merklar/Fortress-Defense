using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPbarEnemyScript : MonoBehaviour {


    public GameObject BlackBar;
    public GameObject RedBar;

    private float HPbarMaxLength;
    private float HPbarCurrentLength;
    private float OnePercent;
    private float localScaleY;

	void Awake () {
        localScaleY = RedBar.transform.localScale.y;
        HPbarMaxLength = RedBar.transform.localScale.x;
        OnePercent = HPbarMaxLength / 100f;
        BlackBar.SetActive(false);
        RedBar.SetActive(false);
    }

    public void UpdateHPBar(int _percent)
    {
        if (RedBar.activeSelf == false) {
            BlackBar.SetActive(true);
            RedBar.SetActive(true);
        }
        RedBar.transform.localScale = new Vector3(OnePercent * _percent, localScaleY, 1f);
    }
}
