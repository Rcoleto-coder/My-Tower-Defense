using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public SceneFader sceneFader;
    public string menuSceneName = "MainMenu";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        // Toggle the active state of the Pause UI
        pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);
        if(pauseMenuUI.activeSelf)
        {
            // Pause the game
            Time.timeScale = 0.0f;
        }
        else
        {
            // Resume the game
            Time.timeScale = 1.0f;
        }
    }
   

    public void Retry()
    {
        Toggle();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }
    public void Menu()
    {
        Toggle();
        sceneFader.FadeTo(menuSceneName);
    }

}
