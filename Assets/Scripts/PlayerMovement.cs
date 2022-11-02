using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    CapsuleCollider2D collider;
    SpriteRenderer sprite;
    public float speed = 10;
    public float rotationSpeed = 10;
    public GameObject bala;
    public GameObject boquilla;
    public GameObject particulasMuerte;
    // Start is called before the first frame update


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        if (vertical > 0)
        {
            rb.AddForce(transform.up * vertical * speed * Time.deltaTime);
            anim.SetBool("Impulsing", true);
        }
        else
        {
            anim.SetBool("Impulsing", false);
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        transform.eulerAngles = transform.eulerAngles + new Vector3(0, 0, horizontal * rotationSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump"))
        {
            GameObject temp = Instantiate(bala, boquilla.transform.position, transform.rotation);
            Destroy(temp, 2.5f);
        }
    }

    public void Muerte()
    {
        GameObject temp = Instantiate(particulasMuerte, transform.position, transform.rotation);
        Destroy(temp, 2.5f);

        if (GameManager.instance.vidas <= 0)
        {
            Destroy(gameObject);
        }
        else
        { 
            StartCoroutine(Respawn_Corountine());
        }
    }

    IEnumerator Respawn_Corountine()
    {
        collider.enabled = false;
        sprite.enabled = false;
        yield return new WaitForSeconds(2);
        collider.enabled = true;
        sprite.enabled = true;

        GameManager.instance.vidas -= 1;
        transform.position = new Vector3(0, 0, 0);
        rb.velocity = new Vector2(0, 0);
 
    }
}