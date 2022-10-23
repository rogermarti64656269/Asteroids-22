using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    public float speed = 10;
    public float rotationSpeed = 10;
    public GameObject bala;
    public GameObject boquilla;
    // Start is called before the first frame update


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        if (vertical > 0)
        {
            rb.AddForce(transform.up * vertical * speed * Time.deltaTime);
            anim.SetBool("Impulsing", true);
        }
        else
        {
            anim.SetBool("Impulsing", false);
        }

        float horizontal = Input.GetAxis("Horizontal");
        transform.eulerAngles = transform.eulerAngles + new Vector3(0, 0, horizontal * rotationSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump"))
        {
            GameObject temp = Instantiate(bala, boquilla.transform.position, transform.rotation);
            Destroy(temp, 2.5f);
        }
    }

    public void Muerte()
    {
        //Destroy(gameObject);
    }
}