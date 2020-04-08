using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreenLoader : MonoBehaviour
{
    [SerializeField]
    private Image m_loadingBar;
    [SerializeField]
    private float m_timeBeforeLoadingMenu = 5.0f;

    private AsyncOperation asyncLoadScene;

    private void Start()
    {
        StartCoroutine(LoadAsynchronously("Level1"));

        Invoke("ActiveMenu", m_timeBeforeLoadingMenu); //minial time in splash screen
    }

    IEnumerator LoadAsynchronously(string levelName)
    {
        float timer = 0f;

        AsyncOperation operation = SceneManager.LoadSceneAsync(levelName);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            m_loadingBar.fillAmount = progress;
            timer += Time.deltaTime;

            if (timer > m_timeBeforeLoadingMenu)
            {
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
        yield return null;
    }
}
