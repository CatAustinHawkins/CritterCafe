using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//This script is on the customer, controlling their movement and happiness
/// <summary>
/// Pony Customer
/// Day 2
/// 2 Meals
/// </summary>
public class PonyCustomer : MonoBehaviour
{
    //Speed and location variables
    private float speed = 8.0f; //customer speed
    public GameObject FoHtarget; //front of house desk target
    public Vector2 position; //the customers position
    public GameObject DeskTarget; //the table target
    public GameObject LeavingArea; //the leaving area

    //Bools which control where the customer goes
    public bool movingtofoh;
    public bool movingtotable;

    //Their thought bubble
    public GameObject ThoughtBubble;

    //Bools to check what they are doing
    public bool MovingtoDesk;
    public bool WaitingatTable;

    //Happiness variables
    public Slider OverallHappiness;

    //If the customers leaving
    public bool Leaving;

    public GameObject GameFinished; //game finished gameobject

    public GameObject DirtyPlate; //dirty plate

    public GameObject StrawberryandAppleSmoothie; //REPLACE WITH FOOD
    public GameObject HaySalad;

    public GameObject Coins; //coins

    public Dialogue DialogueScript; //dialogue script

    public GameObject HedgehogCustomer; //REPLACE WITH SPAWNED CUSTOMER

    public AudioSource PlateDrop;
    public AudioSource AnimalEating;

    public GameObject GiraffeCustomer;

    void Start()
    {
        position = gameObject.transform.position; //Get the customers position
        StartCoroutine(HappinessValue());
        StartCoroutine(HedgehogSpawn());

        movingtofoh = true; //start moving to the front of house desk

        DialogueScript.PonyDrink = true;
    }

    void Update()
    {
        if (OverallHappiness.value < 0) //if the overall happiness reaches 0
        {
            GameFinished.SetActive(true); //end the game
        }

        float step = speed * Time.deltaTime;

        if (movingtofoh) //if the player is moving to the front of house
        {
            // move sprite towards the target location
            transform.position = Vector2.MoveTowards(transform.position, FoHtarget.transform.position, step);

            if (!MovingtoDesk)
            {
                StartCoroutine(DeskWait());
                MovingtoDesk = true;
            }
        }

        if (movingtotable) //if the player is moving towards the table
        {
            // move sprite towards the target location
            transform.position = Vector2.MoveTowards(transform.position, DeskTarget.transform.position, step);

            if (!WaitingatTable)
            {
                StartCoroutine(ThoughtWait());
                WaitingatTable = true;
            }
        }

        if (Leaving) //if the customer has been fed and is leaving
        {
            transform.position = Vector2.MoveTowards(transform.position, LeavingArea.transform.position, step);

            StartCoroutine(PonyLeaving());

            if (transform.position == LeavingArea.transform.position)
            {
                gameObject.SetActive(false);
            }
        }
    }
    IEnumerator PonyLeaving() //waiting at the front of house desk - then they can move to the table
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false); //destroy the customer
        GiraffeCustomer.SetActive(true);
    }

    IEnumerator DeskWait() //waiting at the front of house desk - then they can move to the table
    {
        yield return new WaitForSeconds(5);
        movingtofoh = false;
        movingtotable = true;
    }

    IEnumerator ThoughtWait()//waiting at the table - then the thought bubble appears
    {
        yield return new WaitForSeconds(6);
        movingtotable = false;
        ThoughtBubble.SetActive(true);
        DialogueScript.AbletoTalk = true;
    }

    IEnumerator HappinessValue()
    {
        yield return new WaitForSeconds(1f);
        OverallHappiness.value = OverallHappiness.value - 0.0016f;
        StartCoroutine(HappinessValue());
    }

    public void FedSmoothie()
    {
        StrawberryandAppleSmoothie.SetActive(true);
        DialogueScript.PonyDrink = false;
        StartCoroutine(Eating1());
    }

    public void FedMeal() //if the customer has been fed
    {
        HaySalad.SetActive(true);
        DialogueScript.PonyMeal = false;
        StartCoroutine(Eating2());
        DialogueScript.PonyDone = true;
        DialogueScript.HedgehogDrink = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "LeavingArea") //when the customer collides with the leaving area (an off screen zone)
        {
            gameObject.SetActive(false); //destroy the customer
        }
    }

    IEnumerator Eating1()
    {
        AnimalEating.Play();
        yield return new WaitForSeconds(6f);
        DirtyPlate.SetActive(true);
        Coins.SetActive(true);
        StrawberryandAppleSmoothie.SetActive(false);
        DialogueScript.ThoughtIcons[7].SetActive(true); //enable next thought bubble and icon
        DialogueScript.ThinkBubble[4].SetActive(true);
        DialogueScript.PonyMeal = true;
        DialogueScript.AbletoTalk = true; //set able to talk to true, so the panda can order bamboo hotdog
        PlateDrop.Play();
    }

    IEnumerator Eating2() //second eating coroutine
    {
        AnimalEating.Play();
        yield return new WaitForSeconds(6f); //wait 6 seconds
        DirtyPlate.SetActive(true);
        HaySalad.SetActive(false);
        Coins.SetActive(true);
        PlateDrop.Play();
        Leaving = true; //make the customer leave
        DialogueScript.AbletoTalk = true;
    }

    IEnumerator HedgehogSpawn()
    {
        yield return new WaitForSeconds(30f);
        HedgehogCustomer.SetActive(true);
    }
}
 