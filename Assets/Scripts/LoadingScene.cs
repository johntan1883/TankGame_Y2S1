using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private GameObject sceneOne;
    [SerializeField] private GameObject sceneTwo;

    private void Start()
    {
        StartCoroutine(StartLoadingScene());
    }
    private IEnumerator StartLoadingScene()
    {
        sceneOne.SetActive(true);

        yield return new WaitForSeconds(2.5f);

        sceneOne.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        sceneTwo.SetActive(true);

        yield return new WaitForSeconds(2.5f);

        SceneManager.LoadScene("Level");
    }
}
