using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Enemy2Attack : MonoBehaviour
{
    private float timeBtwAttack;
    private float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRangeLight = 2f;
    public float attackRangeHeavy = 2.4f;
    public int lightDamage = 10;
    public int heavyDamage = 20;
    //https://www.youtube.com/watch?v=sPiVz1k-fEs
    
    PhotonView view;
    
    void Start()
    {
    	view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
    if (view.IsMine)
    {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)) 
            {
                view.RPC("LightAttack", RpcTarget.All);
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                // Heavy attack
		view.RPC("HeavyAttack", RpcTarget.All);
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }
    }
    
    [PunRPC]
    void LightAttack()
    {
        // Light attack
        Collider2D[] enemyToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRangeLight, whatIsEnemies);
        for (int i = 0; i < enemyToDamage.Length; i++)
        {
            enemyToDamage[i].GetComponent<SwordNinja>().TakeDamage(lightDamage);
        }
    }

    [PunRPC]
    void HeavyAttack()
    {
        // Heavy attack
        Collider2D[] enemyToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRangeHeavy, whatIsEnemies);
        for (int i = 0; i < enemyToDamage.Length; i++)
        {
            enemyToDamage[i].GetComponent<SwordNinja>().TakeDamage(heavyDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRangeLight);
        Gizmos.DrawWireSphere(attackPos.position, attackRangeHeavy);
    }
}
