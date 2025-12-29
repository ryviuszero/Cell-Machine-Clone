using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor; // 仅编辑器环境导入命名空间
#endif

public class TitleUI : MonoBehaviour
{
    public GameObject levelBtnPrefab;
    public GameObject mainScreen;
    public GameObject levelScreen;
    public GameObject levelGrid;

    private static bool firstLoad = true;

    private Color compCol = new Color(0.45f, 0.59f, 0.93f);

    private void Start()
    {
        if (firstLoad)
        {
            ShowMain();
        }
        else
        {
            ShowLevels();
        }
        firstLoad = false;
        BuildLevelSelect();
    }

    public void ToGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ToGame(int level)
    {
        GameData.level = level;
        SceneManager.LoadScene("Game");
    }

    private void BuildLevelSelect()
    {
        for (int i = 0; i < 17; i++)
        {
            GameObject gameObject = Instantiate(levelBtnPrefab, levelGrid.transform);
            gameObject.GetComponentInChildren<TMP_Text>().text = (i + 1).ToString();
            int ti = i;
            gameObject.GetComponent<Button>().onClick.AddListener(delegate { ToGame(ti); });
            if (PlayerPrefs.HasKey("lev" + i))
            {
                gameObject.GetComponent<Image>().color = compCol;
                gameObject.GetComponentInChildren<TMP_Text>().color = compCol;
            }
        }
    }

    public void ShowMain()
    {
        Debug.Log("ShowMain");
        mainScreen.SetActive(value: true);
        levelScreen.SetActive(value: false);
        Camera.main.transform.position = new Vector3(mainScreen.transform.position.x, mainScreen.transform.position.y, -10f);

    }

    public void ShowLevels()
    {
        mainScreen.SetActive(value: false);
        levelScreen.SetActive(value: true);
        Camera.main.transform.position = new Vector3(levelScreen.transform.position.x, levelScreen.transform.position.y, -10f);
    }

    public void QuitGame()
    {
        // 编辑器环境：停止播放模式
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        // 打包后环境：退出游戏
        Application.Quit();
        #endif
        // Application.Quit();
    }
}
