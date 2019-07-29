using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerEscenas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LoadScence(int indexScene)
    {
        StartCoroutine(LoadYourAsyncScene(indexScene));
    }

    IEnumerator LoadYourAsyncScene(int indexScene)
    {
        StartCoroutine(WaitToLoad());
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(indexScene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    IEnumerator WaitToLoad()
    {
        yield return new WaitForSeconds(0.2f);
    }

}
