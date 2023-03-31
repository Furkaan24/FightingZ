using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemy;
    public GameObject p1;
    public float minX, maxX, minY, maxY;

    public float index = 0;

    private void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        if (p1 == null)
        {
           p1 =  PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate(enemy.name, randomPosition, Quaternion.identity);
        }
        
    }
}
