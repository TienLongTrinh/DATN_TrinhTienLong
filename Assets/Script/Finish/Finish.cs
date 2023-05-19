using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Finish : MonoBehaviour
{

    private bool levelComleted = false;
    [SerializeField] private AudioClip finishSound;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] public Slider loading;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !levelComleted)
        {
            SoundManager.instance.PlaySound(finishSound);
            levelComleted = true;
            loadingScreen.SetActive(true);
            LoadNextScene();
           // Invoke("LoadNextScene", 2f); 
        }
    }
    private void LoadNextScene()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        loading.value = 0;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        asyncLoad.allowSceneActivation = false;
        float progress = 0;
        while (!asyncLoad.isDone)
        {
            progress = Mathf.MoveTowards(progress, asyncLoad.progress, Time.deltaTime);
            loading.value = progress;
            if (progress >= 0.9f)
            {
                loading.value = 1;
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
        /*while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }*/

        asyncLoad.allowSceneActivation = true;
    }

    /* private void CompleteLevel()
     {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
     }*/

}
