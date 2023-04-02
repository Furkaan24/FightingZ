using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class SwordNinja : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    private Vector3 rotation;

    HealthSystemForDummies healthSystem; //https://pacific-barber-b62.notion.site/Health-System-for-Dummies-Guide-2a3db422154248948085a128a392b6ad
    private bool isGrounded = false;
    public float jumpHeight = 8f;
    public float movementSpeed = 4f;
    private float dazedTime;
    public float startDazedTime;

    PhotonView view;



    void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rotation = transform.eulerAngles;
        healthSystem = GetComponent<HealthSystemForDummies>();
        view = GetComponent<PhotonView>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    [PunRPC]
    void Update()
    {
    	if (view.IsMine) {   
            if (dazedTime <= 0)
            {
                movementSpeed = 4f;
            }
            else
            {
                movementSpeed = 0f;
                dazedTime -= Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A))
            {
                animator.SetBool("IsRunning", true);
                transform.eulerAngles = new Vector3(0, 180, 0); // facing left
                transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                animator.SetBool("IsRunning", true);
                transform.eulerAngles = new Vector3(0, 0, 0); // facing right
                transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("IsRunning", false);
            }

            if (Input.GetKeyDown(KeyCode.J) && isGrounded == true)
            {
                animator.SetTrigger("lightattack");
            }
            if (Input.GetKeyDown(KeyCode.J) && isGrounded == false)
            {
                animator.SetTrigger("jumpattack");
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                animator.SetTrigger("strike");
            }
            if (isGrounded == false)
            {
                animator.SetBool("IsJumping", true);
            }
            else
            {
                animator.SetBool("IsJumping", false);
            }
            if (Input.GetKeyDown(KeyCode.W) && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                isGrounded = false;
            }
            if(healthSystem.GetCurrentHealth() <= 0)
            {
            	healthSystem.Kill();
            	PhotonNetwork.LoadLevel("GameOver");
            	PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    [PunRPC]
    public void UpdateHealth(float newHealth)
    {
        healthSystem.SetCurrentHealth((int)newHealth);
    }

    public void TakeDamage(int damage)
    {
    	if (!view.IsMine)
    	{
           return;
    	}
        dazedTime = startDazedTime;
        animator.SetTrigger("hit");
        healthSystem.AddToCurrentHealth(-damage);

        view.RPC("UpdateHealth", RpcTarget.All, (float)healthSystem.GetCurrentHealth());
    }

}
