using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class StartMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject loadinginterface;
    public Image loadingProgressbar; 

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {

    }

    public void StartGame()
    {
        HideMenu();
        /*ShowLoadingScreen();*/
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Game"));
    }

    private void ShowLoadingScreen()
    {
        loadinginterface.SetActive(true);
    }

    private void HideMenu()
    {
        menu.SetActive(false);
    }

    IEnumerator LoadingScreen()
    {
        float totalProgress = 0;
        for(int i = 0;i < scenesToLoad.Count;i++)
        {
            while(!scenesToLoad[i].isDone)
            {
                totalProgress += scenesToLoad[i].progress;
                loadingProgressbar.fillAmount = totalProgress / scenesToLoad.Count;
                yield return null;
            }
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}