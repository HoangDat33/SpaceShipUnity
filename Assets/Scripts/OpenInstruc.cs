using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenInstruc : MonoBehaviour
{
    public GameObject nextLevelCanvas;
    public void loadInstruct()
    {
        SceneManager.LoadScene(1);
    }
    public void NextGame()
    {
        if (nextLevelCanvas != null)
        {
            nextLevelCanvas.SetActive(false); // Tắt Canvas
            Time.timeScale = 1f; // Tiếp tục game
        }
    }

    public void BackMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
