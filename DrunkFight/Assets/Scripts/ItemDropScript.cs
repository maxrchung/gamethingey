using UnityEngine;
using System.Collections;

public class ItemDropScript : MonoBehaviour {

     public int currentItem;
     private float dropTime;
     public Sprite spawn;
     public Sprite item1;
     private SpriteRenderer spriteKun;
     public WeaponScript weaponScript;
     private ParticleSystem part;
     private ParticleEmitter partE;
     private float partTime;

	// Use this for initialization
	void Start () {
          spriteKun = GetComponent<SpriteRenderer>();
          currentItem = (int)(UnityEngine.Random.value*100) % 4 + 1;
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
          if (currentItem == 0)
          {
               spriteKun.sprite = spawn;
          }
          else
          {
               spriteKun.sprite = item1;
          }
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
