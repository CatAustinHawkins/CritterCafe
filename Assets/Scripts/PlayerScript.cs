using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb2d;
    public GameObject DialogueBox;

    public GameObject playersetupscript;

    public PlayerSetup playersetup;

    public string PlayerName;

    public Sprite Player1;
    public Sprite Player2;
    public Sprite Player3;
    public Sprite Player4;

    public SpriteRenderer spriterenderer;

    public TextMeshProUGUI PlayerNameIntro;


    void Start()
    {

        playersetupscript = GameObject.FindWithTag("Setup");
        playersetup = playersetupscript.GetComponent<PlayerSetup>();


        rb2d = GetComponent<Rigidbody2D>();

        if(playersetup.Choice1Selected)
        {
            spriterenderer.sprite = Player1;
        }

        if (playersetup.Choice2Selected)
        {
            spriterenderer.sprite = Player2;
        }

        if (playersetup.Choice3Selected)
        {
            spriterenderer.sprite = Player3;
        }

        if (playersetup.Choice4Selected)
        {
            spriterenderer.sprite = Player4;
        }

        PlayerName = playersetup.PlayerName;

        PlayerNameIntro.text = "Hi " + PlayerName + "!";
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rb2d.velocity = new Vector2(moveHorizontal * speed, moveVertical * speed);

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "CustomerTest")
        {
            DialogueBox.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "CustomerTest")
        {
            DialogueBox.SetActive(false);
        }
    }
}
