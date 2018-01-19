using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBtnScripts : MonoBehaviour {

public void OnPlayBtnPress()
    {
        SceneManager.LoadScene("Level");
    }
}
