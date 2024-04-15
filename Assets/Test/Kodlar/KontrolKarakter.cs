using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class KontrolKarakter : MonoBehaviour
{

    public Rigidbody2D karakterRb;
    public float karakterHiz;
    public Animator benimAnimator;
    public SpriteRenderer benimSprite;

    public LayerMask zeminKatman;
    public bool yerdeMi = false;
    public float zemineUzaklik = 0.8f;
    public float ziplamaKuvveti = 15f;
    public float yerCekimi = 1f;
    public float dusmeCarpani = 5f;
    public float cizgiselKuvvet;
    public float ziplamaGecikme = 0.25f;
    private float ziplamaZamanlayici;
    public Vector3 colliderOffset1;
    public Vector3 colliderOffset2;
    public Vector3 colliderOffset3;
    private float coyoteTime = 0.2f;
    private float ziplayabilir;


    // Start is called before the first frame update
    void Start()
    {

        float yatayGiris = Input.GetAxis("Horizontal");



    }

    // Update is called once per frame
    void Update()
    {
        if(yerdeMi == true)
        {
            ziplayabilir = coyoteTime;

        }
        else
            ziplayabilir -= Time.deltaTime;

        yerdeMi = Physics2D.Raycast(transform.position + colliderOffset1, Vector2.down, zemineUzaklik, zeminKatman) || Physics2D.Raycast(transform.position - colliderOffset2, Vector2.down, zemineUzaklik, zeminKatman) || Physics2D.Raycast(transform.position - colliderOffset3, Vector2.down, zemineUzaklik, zeminKatman);

        if (Input.GetButtonDown("Jump"))
        {
            ziplamaZamanlayici = Time.time + ziplamaGecikme;
        }

        if (yerdeMi == false)
        {
            benimAnimator.SetBool("Havada", true);
        }

        else
            benimAnimator.SetBool("Havada", false);
        if (yerdeMi == true && karakterRb.velocity.y != 0)
        {
            benimAnimator.SetBool("Dustu", true);
        }
        else
            benimAnimator.SetBool("Dustu", false);
    }

    void FixedUpdate()
    {
        Hareket();
        fizikDuzenleme();
        if (ziplamaZamanlayici > Time.time && ziplayabilir >0)
        {
           
            Zipla();

        }
    }

    void Zipla()
    {
        karakterRb.velocity = new Vector2(karakterRb.velocity.x, 0);
        karakterRb.AddForce(Vector2.up * ziplamaKuvveti, ForceMode2D.Impulse);
        ziplayabilir = 0f;
    }

    void fizikDuzenleme()
    {
        if (yerdeMi)
        {
            karakterRb.gravityScale = 0;
        }

        else
        {
            karakterRb.gravityScale = yerCekimi;
            karakterRb.drag = cizgiselKuvvet * 0.15f;
            if (karakterRb.velocity.y < 0)
            {
                karakterRb.gravityScale = yerCekimi * dusmeCarpani;
            } else if (karakterRb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                karakterRb.gravityScale = yerCekimi * (dusmeCarpani / 2);
            }
        }
    }

    void Hareket()
    {
        float yatayGiris = Input.GetAxisRaw("Horizontal");
        karakterRb.transform.Translate(yatayGiris * karakterHiz * Time.deltaTime, 0, 0);
        if (yatayGiris > 0)
        {
            benimSprite.flipX = true;
        }
        else if (yatayGiris < 0)
        {
            benimSprite.flipX = false;
        }


        if (yatayGiris != 0)
        {
            benimAnimator.SetBool("Kosuyor", true);
        }
        else
            benimAnimator.SetBool("Kosuyor", false);

    }

   

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position + colliderOffset1, transform.position + colliderOffset1 + Vector3.down * zemineUzaklik);
        Gizmos.DrawLine(transform.position - colliderOffset2, transform.position - colliderOffset2 + Vector3.down * zemineUzaklik);
        Gizmos.DrawLine(transform.position - colliderOffset3, transform.position - colliderOffset3 + Vector3.down * zemineUzaklik);
    }

    

}
