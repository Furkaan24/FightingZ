using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    private Vector3 rotation;

    HealthSystemForDummies healthSystem;
    private bool isGrounded = false;
    public float jumpHeight = 8f;
    public float movementSpeed = 4f;
    private float dazedTime;
    public float startDazedTime;
    bool facingRight = true;
    
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

    void Update()
    {
    	if (!view.IsMine) {
            return;
        }
        
        if(dazedTime <= 0)
        {
            movementSpeed = 4f;
        }
        else
        {
            movementSpeed = 0f;
            dazedTime -= Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (!facingRight)
            {
                Flip();
            }
            transform.eulerAngles = rotation;
            animator.SetBool("IsRunning", true);
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (facingRight)
            {
                Flip();
            }
            transform.eulerAngles = rotation - new Vector3(0, 180, 0);
            animator.SetBool("IsRunning", true);
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && isGrounded == true)
        {
            animator.SetTrigger("lightattack");
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && isGrounded == false)
        {
            animator.SetTrigger("jumpattack");
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
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
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
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

    void Flip()
    {
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

        facingRight = !facingRight;
    }


}
