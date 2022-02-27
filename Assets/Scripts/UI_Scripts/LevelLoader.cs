using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator anim;
    public float transitionTime = 1f; 
    
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        anim.SetTrigger("start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    public void ReloadCurrentScene(float startWait)
    {
        StartCoroutine(ReloadCurrentSceneCoroutine(startWait));
    }

    IEnumerator ReloadCurrentSceneCoroutine(float startWait)
    {
        yield return new WaitForSeconds(startWait);
        anim.SetTrigger("start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void EndGame()
    {
        StartCoroutine(EndGameCoroutine(2f));
    }

    IEnumerator EndGameCoroutine(float startWait)
    {
        yield return new WaitForSeconds(startWait);
        anim.SetTrigger("start");
        yield return new WaitForSeconds(.5f);
        print("quitting");
        Application.Quit();
    }
}
