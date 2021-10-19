using UnityEngine;
using System.Collections;

public class Megamancontrol : MonoBehaviour {

    public float speed = 5f;
    Rigidbody2D rigid;
    Animator anim;
    bool olhandoParaDir = true;
    public float jumpForce = 100;
    public bool onGround;
    public bool pulando;
    public float maxJump = 8;
    public GameObject tiro;
    public float charge;
    public GameObject superTiro;
    

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator> ();
        charge = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        float moveX = Input.GetAxis("Horizontal");
        if(moveX > 0 && !olhandoParaDir)
        {
            Flip();
        } else if(moveX < 0 && olhandoParaDir)
        {
            Flip();
        }
        anim.SetFloat("speed", Mathf.Abs(moveX));
        rigid.velocity = new Vector2(moveX * speed, rigid.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            pulando = true;
            rigid.AddForce(new Vector2(0, jumpForce));
        }
        if (Input.GetKey (KeyCode.Space) && pulando)
        {
            rigid.AddForce(new Vector2(0, jumpForce));
            if(rigid.velocity.y >= maxJump)
            {
                pulando = false;
            }
        }
        if (Input.GetKeyUp (KeyCode.Space) && pulando)
        {
            pulando = false;
        }
        anim.SetBool("jump", !onGround);

        if (Input.GetKey(KeyCode.Z) || Input.GetMouseButton(0))
        { 
            charge = charge + 0.2f;
        }

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("shoot");
            GameObject esseTiro = (GameObject) Instantiate (tiro, transform.position, Quaternion.identity);
            if (olhandoParaDir)
            {
                esseTiro.GetComponent<Rigidbody2D>().AddForce(new Vector2(600, 0));
                esseTiro.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                esseTiro.GetComponent<Rigidbody2D>().AddForce(new Vector2(-600, 0));
                esseTiro.GetComponent<SpriteRenderer>().flipX = false;
            }
            charge = 0f;
        }
        if ((Input.GetKeyUp(KeyCode.Z) || Input.GetMouseButtonUp(0)) && charge > 10f)
        {
            anim.SetTrigger("shoot");
            GameObject esseTiro = (GameObject)Instantiate(superTiro, transform.position, Quaternion.identity);
            if (olhandoParaDir)
            {
                esseTiro.GetComponent<Rigidbody2D>().AddForce(new Vector2(600, 0));
                esseTiro.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                esseTiro.GetComponent<Rigidbody2D>().AddForce(new Vector2(-600, 0));
                esseTiro.GetComponent<SpriteRenderer>().flipX = false;
            }
            charge = 0f;
        }
        
    }
    void Flip()
    {
        olhandoParaDir = !olhandoParaDir;
        Vector3 novaEscala = transform.localScale;
        novaEscala.x *= -1;
        transform.localScale = novaEscala;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.tag == "Ground")
        {
            onGround = true;
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.tag == "Ground")
        {
            onGround = false;
        }
    }
}
