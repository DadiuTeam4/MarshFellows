//Author: Emil Villumsen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class Follower : MonoBehaviour {

    private EventManager eventManager;
    private IkHandler ikHandler;
    private Animator animator;
    private GameObject O;

    void Start () {
        eventManager = EventManager.GetInstance();
        ikHandler = GetComponentInChildren<IkHandler>();
        animator = GetComponentInChildren<Animator>();

        eventManager.AddListener(CustomEvent.ScenarioInteracted, LookAtHunters);
        O = GameObject.FindGameObjectWithTag("O");

    }

    void LookAtHunters(EventArgument args)
    {
        if (args.stringComponent != "Ritual")
        {
            return;
        }
        if (!ikHandler)
        {
            Debug.LogError("Couldnt find IKHandler script on followers in ritual scene");
        }

        transform.LookAt(O.transform);
        animator.SetBool("followerStare", true);
        ikHandler.LookAtTarget(O.transform);
    }


}
