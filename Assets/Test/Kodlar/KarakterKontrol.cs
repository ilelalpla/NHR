using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarakterKontrol : MonoBehaviour
{
    public Rigidbody2D rbKarakter;
    public float karakterHiz;
    public float karakterZiplama;
    public SpriteRenderer karakterSprite;
    public bool ziplayabilir = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        float yatayGiris = Input.GetAxis("Horizontal");
        Debug.Log(yatayGiris);

        transform.Translate(karakterHiz * yatayGiris * Time.deltaTime, 0, 0);
        if (yatayGiris < 0)
        {
            karakterSprite.flipX = false;
        }
        if (yatayGiris > 0)
        {
            karakterSprite.flipX = true;
        }

        if(ziplayabilir == true && Input.GetKeyDown(KeyCode.Space))
        {
            Zipla();

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("zemin"))
        {
            ziplayabilir = true;

        }


    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("zemin"))
        {
            ziplayabilir = false;
        }

    }
    private void Zipla()
    {
        // Yatay hýzý koruyarak sadece dikey hýzý ayarla
        rbKarakter.velocity = new Vector2(rbKarakter.velocity.x, karakterZiplama);
    }
}
