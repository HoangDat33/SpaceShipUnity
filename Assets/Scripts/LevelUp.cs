using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUp : MonoBehaviour
{
    public void playAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void nextGame()
    {
        SceneManager.LoadScene(3);
    }
}
