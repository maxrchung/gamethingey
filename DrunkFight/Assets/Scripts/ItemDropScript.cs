﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ItemDropScript : NetworkBehaviour
{
    [SyncVar]
    public int currentItem;

    private float dropTime;
    public WeaponScript weaponScript;
    private Animator animKun;
    private int lastItem;

    // Use this for initialization
    void Start()
    {
        animKun = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentItem == 0 && Time.time - dropTime > 6.9)
        {
            if (isServer)
            {
                currentItem = (int)(UnityEngine.Random.value * 100) % 4 + 1;
                while (currentItem == lastItem)
                {
                    currentItem = (int)(UnityEngine.Random.value * 100) % 4 + 1;
                }
                lastItem = currentItem;
            }
        }
    }

    void LateUpdate()
    {
        if (currentItem == 0) // empty
        {
            animKun.SetInteger("currentItem", 0);
        }
        else if (currentItem == 1)
        { // vomit
            animKun.SetInteger("currentItem", 1);
        }
        else if (currentItem == 2)
        { // poop
            animKun.SetInteger("currentItem", 2);
        }
        else if (currentItem == 3)
        { // flame
            animKun.SetInteger("currentItem", 3);
        }
        else if (currentItem == 4)
        { // heal
            animKun.SetInteger("currentItem", 4);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (currentItem != 0)
            {
                GameObject.FindWithTag("AudioMaster").GetComponent<AudioMasterScript>().PlayItemSound();
				Debug.Log ("FUCKIOGFNIWGOINWEINOGWENIO");
                weaponScript = other.GetComponent<WeaponScript>();
                weaponScript.getWeapon(currentItem);
                currentItem = 0;
                dropTime = Time.time;
            }
        }
    }
}
