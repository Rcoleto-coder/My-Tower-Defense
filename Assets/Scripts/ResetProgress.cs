using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetProgress : MonoBehaviour
{
    public void ResetProg()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("LevelSelectScreen");
    }
}
