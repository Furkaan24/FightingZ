using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxer : MonoBehaviour
{

    Animator anim;
//�Use�this�for�initialization��
void Start()
    {
        anim = GetComponent<Animator>();
    }

    //�Update�is�called�once�per�frame��
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            anim.Play("walkBack");
            transform.Translate(Vector3.left * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            anim.Play("Walk");
            transform.Translate(Vector3.right * Time.deltaTime);
        }
        if (!Input.anyKey)
        {
            anim.Play("Boxer");
        }
        if (Input.GetKey(KeyCode.P))
        {
            anim.Play("PunchRight");
        }
        if (Input.GetKey(KeyCode.U))
        {
            anim.Play("uppercut");
        }
    }
}
