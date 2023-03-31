using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu"); 
    }
}
