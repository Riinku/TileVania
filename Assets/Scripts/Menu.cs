using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{

    [SerializeField] GameObject helpButton = null;
    [SerializeField] GameObject helpText = null;

    private void Start() 
    {
        if(helpButton != null && helpText != null)
        {
            helpText.SetActive(false);
            helpButton.SetActive(true);
        }
    }

    public void StartGameButton()
    {
        SceneManager.LoadScene(1);
    }

    public void HelpGameButton()
    {
        helpButton.SetActive(false);
        helpText.SetActive(true);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

}
