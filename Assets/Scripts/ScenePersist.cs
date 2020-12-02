using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{
    static ScenePersist instance = null;
 
    int startingSceneIndex;
 
    void Start()
    {
        if (!instance)              // if the instance has not already been set
        {
            instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
            startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
 
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (startingSceneIndex != SceneManager.GetActiveScene().buildIndex)
        {
            instance = null;
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy(gameObject);
        }
    }
}








    // int startingBuildIndex = 0;


    // private void Awake() 
    // {   

    //     int numOfScenePresists = FindObjectsOfType<ScenePersist>().Length;

    //     if(numOfScenePresists >1)
    //     {
    //         Destroy(gameObject);
    //     }
    //     else
    //     {
    //         DontDestroyOnLoad(gameObject);
    //     }          
    // }


    // // Start is called before the first frame update
    // void Start()
    // {
    //     startingBuildIndex = SceneManager.GetActiveScene().buildIndex;
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     if(startingBuildIndex != SceneManager.GetActiveScene().buildIndex)
    //     {
    //         Destroy(gameObject);
    //     }
    // }

