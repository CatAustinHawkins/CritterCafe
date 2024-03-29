using UnityEngine;
using TMPro;
using System.Collections;
//new dialogue script that works the dialogue button

public class Dialogue : MonoBehaviour
{
    public bool AbletoTalk;

    public bool PandaInRange; //if pandas in range

    public GameObject DialogueBox; //dialogue box

    public bool TimerRunning;

    public bool DialogueOpen;

    public TextMeshProUGUI DialogueTextTMP;
    public TextMeshProUGUI NameTextTMP;
    public Tutorial TutorialScript;

    public GameObject ThinkBubble;
    public GameObject MeatSmoothieThought;
    public GameObject BambooHotdogThought;

    public bool MealTime;

    public PlayerScript Player; //To access the players script

    public GameObject BambooUIUnused;
    public GameObject BreadUIUnused;
    public GameObject MeatUIUnused;

    public GameObject BambooUI;
    public GameObject BreadUI;
    public GameObject MeatUI;

    public GameObject TickButtonGO;
    public GameObject CrossButtonGO;

    public AudioSource PandaMS;
    public AudioSource PandaBH;
    public AudioSource PandaYes;
    public AudioSource PandaNo;

    public void Update()
    {


        if(AbletoTalk)
        {
            if (Input.GetKey(KeyCode.E) && !TimerRunning)
            {
                DialogueButton();
                Debug.Log("Test1");
                StartCoroutine(DelayTime());
            }

            if (Input.GetKey(KeyCode.Y) && !TimerRunning)
            {
                TickButton();
                StartCoroutine(DelayTime());
            }

            if (Input.GetKey(KeyCode.N) && !TimerRunning)
            {
                CrossButton();
                StartCoroutine(DelayTime());
            }
        }

        if (Input.GetKey(KeyCode.E) && DialogueOpen && !TimerRunning && !AbletoTalk)
        {
            Debug.Log("Test2");

            DialogueBox.SetActive(false);
            DialogueOpen = false;
            if (TutorialScript.TutorialImages == 9)
            {
                TutorialScript.NextTutorial();
            }
        }
    }


    //same as interact script
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "PandaCustomer")
        {
            PandaInRange = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "PandaCustomer")
        {
            PandaInRange = false;
            DialogueBox.SetActive(false);
            DialogueOpen = false;
        }
    }

    public void DialogueButton()
    {

        Player.target = Player.transform.position;

        if (DialogueOpen)
        {
            Debug.Log("Test2");

            DialogueBox.SetActive(false);
            DialogueOpen = false;
            if (TutorialScript.TutorialImages == 9)
            {
                TutorialScript.NextTutorial();
            }
        }
        if (AbletoTalk)
        {
            if (!DialogueOpen)
            {
                if (PandaInRange)
                {
                    if (TutorialScript.TutorialImages == 7)
                    {
                        TutorialScript.NextTutorial();
                        PandaMS.Play();
                    }
                    DialogueBox.SetActive(true); //enable the panda dialogue asking for the bamboo hotdog
                    DialogueOpen = true;

                    if (MealTime)
                    {
                        PandaBH.Play();
                        DialogueTextTMP.text = "Can I get a Bamboo Hotdog?";
                    }
                    TickButtonGO.SetActive(true);
                    CrossButtonGO.SetActive(true);
                }
            }
            else
            {
                DialogueBox.SetActive(false);
                DialogueOpen = false;
                if (TutorialScript.TutorialImages == 9)
                {
                    TutorialScript.NextTutorial();
                }

                
            }
        }

    }

    //controls the tick button
    public void TickButton()
    {
        Player.target = Player.transform.position;


        if (DialogueOpen)
        {
            PandaMS.Pause();
            PandaBH.Pause();
            PandaYes.Play();
            if (PandaInRange)
            {
                if (TutorialScript.TutorialImages == 8)
                {
                    TutorialScript.NextTutorial();
                }
                ThinkBubble.SetActive(false);
                DialogueTextTMP.text = "Yay!!!";
                CrossButtonGO.SetActive(false);
                TickButtonGO.SetActive(false);
            }

            if(MealTime)
            {
                Player.CurrentOrder = 2;
                BambooUIUnused.SetActive(false);
                BreadUIUnused.SetActive(false);

                BambooUI.SetActive(true);
                BreadUI.SetActive(true);
                CrossButtonGO.SetActive(false);
                TickButtonGO.SetActive(false);
                BambooHotdogThought.SetActive(false);
                ThinkBubble.SetActive(false);
                AbletoTalk = false;
                if (TutorialScript.TutorialImages == 21)
                {
                    TutorialScript.NextTutorial();
                }
            }
            else
            {
                Player.CurrentOrder = 1;
                MeatUIUnused.SetActive(false);
                MeatUI.SetActive(true);
                CrossButtonGO.SetActive(false);
                TickButtonGO.SetActive(false);
                MeatSmoothieThought.SetActive(false);
                ThinkBubble.SetActive(false);
                AbletoTalk = false;


            }
        }

    }

    //controls the cross button
    public void CrossButton()
    {
        Player.target = Player.transform.position;

        if (DialogueOpen)
        {
            PandaMS.Pause();
            PandaBH.Pause();
            PandaNo.Play();
            if (PandaInRange)
            {
                DialogueTextTMP.text = "Oh... :(";
            }
        }
    }

    IEnumerator DelayTime()
    {
        TimerRunning = true;
        yield return new WaitForSeconds(0.25f);
        TimerRunning = false;
    }
}
