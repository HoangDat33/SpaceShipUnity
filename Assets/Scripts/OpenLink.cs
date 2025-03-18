using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenLink : MonoBehaviour
{
    public void OpenFaceBook()
    {
        Application.OpenURL("https://www.facebook.com/tiendat206903");
    }
    public void OpenInstagram()
    {
        Application.OpenURL("https://www.instagram.com/tdh_yat/");
    }
    public void OpenGithub()
    {
        Application.OpenURL("https://github.com/HoangDat33/");
    }
    public void backMenu()
    {
        SceneManager.LoadScene(0);
    }
}
