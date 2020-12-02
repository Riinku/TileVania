using UnityEngine;
using UnityEngine.SceneManagement;


public class Credits : MonoBehaviour
{
    private void Awake() 
    {
        FindObjectOfType<GameSession>().DestroyGameSession();
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

}
