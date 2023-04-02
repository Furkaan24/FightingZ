using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameOverController : MonoBehaviour
{
    public void BackToMainMenu()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Menu"); 
    }
}
