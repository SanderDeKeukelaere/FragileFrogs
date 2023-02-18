using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private const string LOADSCENENAME = "LoadingScreen";

    [SerializeField] string _sceneName;
    [SerializeField] TextMeshProUGUI _progress;

    private static string _gotoScene;

    private bool _isLoading = false;

    public void LoadScene()
    {
        LoadScene(_sceneName);
    }
    public static void LoadScene(string sceneName)
    {
        _gotoScene = sceneName;
        SceneManager.LoadScene("LoadingScreen");
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == LOADSCENENAME && _isLoading == false)
        {
            _isLoading = true;
            StartCoroutine(LoadSceneAsync());
            //SceneManager.LoadSceneAsync(_gotoScene);
        }
        if (_isLoading)
        {
            //SceneManager.load
        }
    }
    IEnumerator LoadSceneAsync()
    {
        yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_gotoScene);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        //When the load is still in progress, output the Text and progress bar

        int ticksToComplete = 10;

        while (!asyncOperation.isDone)
        {
            //Output the current progress
            _progress.text = $"({Math.Round(asyncOperation.progress * 100f)})";
            //Debug.Log("Loading progress: " + (asyncOperation.progress * 100) + "%");

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                if (ticksToComplete == 0)
                {
                    asyncOperation.allowSceneActivation = true;
                }
                ticksToComplete--;
            }

            yield return null;
        }
    } 
}