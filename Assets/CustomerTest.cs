using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerTest : MonoBehaviour
{

    //customer gets spawned by a customer manager
    //go to front of house desk --
    //wait 2 seconds --
    //check if a table is free
    //move to empty table
    //wait 2 seconds
    //thought bubble appears
    //player can then interact

    private float speed = 8.0f;
    public Vector3 FoHtarget;
    public Vector3 DeskTarget;
    private Vector2 position;

    public bool TableFree;
    public bool movingtofoh;
    public bool movingtotable;
    public bool stoppedmoving;

    public GameObject ThoughtBubble;

    void Start()
    {
        position = gameObject.transform.position;
        movingtofoh = true;
    }

    void Update()
    {
        float step = speed * Time.deltaTime;

        if(movingtofoh)
        {
            // move sprite towards the target location
            transform.position = Vector2.MoveTowards(transform.position, FoHtarget, step);

            StartCoroutine(DeskWait());
        }

        if(movingtotable)
        {
            // move sprite towards the target location
            transform.position = Vector2.MoveTowards(transform.position, DeskTarget, step);

            StartCoroutine(ThoughtWait());
        }
    }


    IEnumerator DeskWait()
    {
        Debug.Log("DeskWait");
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(4);
        movingtofoh = false;
        TableFree = true;
        Debug.Log("DeskWaitDone");
        movingtotable = true;

    }

    IEnumerator ThoughtWait()
    {
        Debug.Log("ThoughtWait");
        yield return new WaitForSeconds(7);
        movingtotable = false;
        TableFree = false;
        ThoughtBubble.SetActive(true);
        Debug.Log("ThoughtWaitDone");


    }
}
