using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private Text LevelText;
    [SerializeField]
    private Text ScoreText;
    [SerializeField]
    private Text CastleHPText;
    [SerializeField]
    private GameObject GetReadyText;
    [SerializeField]
    private GameObject TryAgainBtn;
    [SerializeField]
    private GameObject GoNextBtn;
    [SerializeField]
    private GameObject PopUp;
    [SerializeField]
    private GameObject GameOverImage;
    [SerializeField]
    private GameObject LevelClearedImage;

    private Vector3 EndImagePosition = new Vector3(0, 3f);
    private Vector3 StartImagePosition;
    private Vector3 StartGRPosition;
    private Vector3 EndGRPosition = new Vector3(0, 1f);

    private void Awake()
    {
        StartImagePosition = GameOverImage.transform.position;
        StartGRPosition = GetReadyText.transform.position;
    }

    public void UpdateUIText(int _level, int _score, int _hp)
    {
        LevelText.text = _level.ToString();
        ScoreText.text = _score.ToString();
        CastleHPText.text = _hp.ToString();
    }

    public void UpdateScoreText(int _value)
    {
        ScoreText.text = _value.ToString();
    }

    public void UpdateCastleHPText(int _value)
    {
        CastleHPText.text = _value.ToString();
    }

    public void PopUpShow()
    {
        PopUp.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        PopUp.SetActive(true);
        PopUp.GetComponent<Image>().color = new Color(0, 0, 0, 0.6f);
    }

    public void PopUpHide()
    {
        PopUp.SetActive(false);
    }
    public void OnGameOverPhase()
    {
        PopUpShow();
        GameOverImage.SetActive(true);
        Invoke("TryAgainButtonEnable", 1f);
        GameOverImage.transform.DOMove(EndImagePosition, 1f);
    }

    public void OnLevelClearPhase()
    {
        PopUpShow();
        LevelClearedImage.SetActive(true);
        Invoke("GoNextButtonEnable", 1f);
        LevelClearedImage.transform.DOMove(EndImagePosition, 1f);
    }

    public void OnGetReadyPhase()
    {
        GetReadyText.transform.position = StartGRPosition;
        GetReadyText.SetActive(true);
        GetReadyText.transform.DOMove(EndGRPosition, 1f);
        Invoke("LevelStart", 2f);
    }

    private void LevelStart()
    {
        GetReadyText.SetActive(false);
        GameManager.instance.GoNextLevel();
    }

    private void TryAgainButtonEnable()
    {
        TryAgainBtn.SetActive(true);
    }
    private void GoNextButtonEnable()
    {
        GoNextBtn.SetActive(true);
    }

    public void TryAgainBtnScript()
    {
        SceneManager.LoadScene("Level");
    }

    public void GoNextBtnScript()
    {
        PopUpHide();
        GoNextBtn.SetActive(false);
        LevelClearedImage.transform.position = StartImagePosition;
        LevelClearedImage.SetActive(false);
        OnGetReadyPhase();

    }
}
