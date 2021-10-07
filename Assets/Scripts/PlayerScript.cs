using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    private int scoreValue = 0;
    private int Lives = 3;
    public Text LivesText;
    public static int targetFrameRate;
    public Text WinText;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    private bool facingRight = false;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
        WinText.text = " ";
        Lives = 3;
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        LivesText.text = Lives.ToString();
        targetFrameRate = 30;
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
        anim = GetComponent<Animator>();
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement, vertMovement));
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
            
                }
           
        
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
        


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue = scoreValue + 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if (scoreValue == 4)
            {
                transform.position = new Vector2(73F, 1.5F);

            }
            if (scoreValue == 8)
            {
                WinText.text = "You Win, Made by Brendan";
                musicSource.clip = musicClipTwo;
                musicSource.Play();
            }
        }
        else if (collision.collider.tag == "Enemy")
        {
            Destroy(collision.collider.gameObject);
            Lives = Lives - 1;
            LivesText.text = Lives.ToString();

            if (Lives == 0)
            {
                this.gameObject.SetActive(false);
                WinText.text = "You Lose";

            }
            

        }
        


        
       
        
        
        

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
            

        }
       
        
        
    }


    void Update()
    {
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);
        
        
        if (isOnGround == false)
        {

            anim.SetInteger("State", 2);
        }
        if (isOnGround == true)
        {
            if (Input.GetKey(KeyCode.D))
            {
                anim.SetInteger("State", 1);
            }
            else
            {
                anim.SetInteger("State", 0);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                anim.SetInteger("State", 0);
            }
            if (Input.GetKey(KeyCode.A))
            {
                anim.SetInteger("State", 1);
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                anim.SetInteger("State", 0);
            }
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
}


