using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{
    public GameObject controlsScreen;

    public int playerLives;

    void Start()
    {
        ClearSingletons();
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("PlayerCurrentLives", playerLives);

        PlayerPrefs.SetInt("CurrentPlayerScore", 0);

        GetComponent<LevelLoader>().LoadLevel();
    }

    public void QuitGame()
    {
        Debug.LogWarning("Game Exited");
        Application.Quit();
    }

    private void ClearSingletons()
    {
       Timer timerScript = FindObjectOfType<Timer>();
		timerScript.resetTimer ();

    }

}
