using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float timeToWait = 1f;
    [SerializeField] float spinSpeed = 1f;
    [SerializeField] GameObject levelComplete = null;
    
    private void Awake() 
    {
        levelComplete.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D otherCollider) 
    {
        if(otherCollider.gameObject.GetComponent<Player>())
        {
            levelComplete.SetActive(true);
            StartCoroutine(LoadNextScene());
        }     
    }


    private void Update() 
    {
        transform.Rotate(0,0,spinSpeed);    
    }



    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(timeToWait);
        FindObjectOfType<GameSession>().ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);       // load next scene in the build index
        levelComplete.SetActive(false);
    }






}
