using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
  
    [SerializeField]
    private GameObject[] characters;

    private int _charIndex;

    public int CharIndex
    {
        get { return _charIndex; }
        set { _charIndex = value; }
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }


    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {  
           if(scene.name == "InGame")
        {
            Vector2 position = new Vector2(-8, -3);

            if (CharIndex == 0)
            {
                position = new Vector2(-8, -3);
            }
            else
            {
                position = new Vector2(8, -3);
            }
            PhotonNetwork.Instantiate(characters[CharIndex].name, position, Quaternion.identity);
        }
    }


}
