using UnityEngine;
using System.Collections;

public class ItemDropScript : MonoBehaviour {

     public int currentItem;
     private float dropTime;
     public WeaponScript weaponScript;
     private Animator animKun;
     private int lastItem;
     private ParticleSystem part;
     private ParticleEmitter partE;
     private float partTime;

	// Use this for initialization
	void Start () {
          currentItem = (int)(UnityEngine.Random.value*100) % 4 + 1;
          animKun = GetComponent<Animator>();
          part = gameObject.GetComponent<ParticleSystem>();
          partE = gameObject.GetComponent<ParticleEmitter>();
          part.Play();
	}
	
	// Update is called once per frame
	void Update () {
          if (currentItem == 0 && Time.time - dropTime > 3)
          {
               currentItem = (int)(UnityEngine.Random.value * 100) % 4 + 1;
          }
          if (Time.time - partTime >= 0.3f)
          {
               part.Stop();
               part.Clear();
          }
	}

     void LateUpdate()
     {
          if(currentItem != lastItem) {
               if (currentItem == 0) // empty
               {
                    animKun.SetInteger("currentItem", 0);
               }
               else if(currentItem == 1) { // vomit
                    animKun.SetInteger("currentItem", 1);
               }
               else if(currentItem == 2) { // poop
                    animKun.SetInteger("currentItem", 2);
               }
               else if(currentItem == 3) { // flame
                    animKun.SetInteger("currentItem", 3);
               }
               else if(currentItem == 4) { // heal
                    animKun.SetInteger("currentItem", 4);
               }
          }
          lastItem = currentItem;
     }

     void OnTriggerStay2D(Collider2D other)
     {
          if (other.gameObject.tag == "Player")
          {
               if (currentItem != 0)
               {
                    //part.Simulate(0.0f, true, true);
                    //Debug.Log(part.IsAlive());
                    weaponScript = other.GetComponent<WeaponScript>();
                    weaponScript.getWeapon(currentItem);
                    currentItem = 0;
                    part.Play();
                    Debug.Log(part.isPlaying);
                    dropTime = Time.time;
                    partTime = Time.time;
               }
          }
     }

     void takeItem()
     {
     }
}
