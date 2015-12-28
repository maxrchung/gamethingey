using UnityEngine;
using System.Collections;

public class ItemDropScript : MonoBehaviour {

     public int currentItem;
     private float dropTime;
     public Sprite spawn;
     public Sprite item1;
     private SpriteRenderer spriteKun;
     public WeaponScript weaponScript;

	// Use this for initialization
	void Start () {
          spriteKun = GetComponent<SpriteRenderer>();
          currentItem = (int)(UnityEngine.Random.value*100) % 4;
	}
	
	// Update is called once per frame
	void Update () {
          if (currentItem == 0 && Time.time - dropTime > 3)
          {
               currentItem = (int)(UnityEngine.Random.value * 100) % 4;
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
                    weaponScript = other.GetComponent<WeaponScript>();
                    weaponScript.getWeapon(currentItem);
                    takeItem();
               }
          }
     }

     void takeItem()
     {
          currentItem = 0;
          dropTime = Time.time;
     }
}
