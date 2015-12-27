using UnityEngine;
using System.Collections;

public class ItemDropScript : MonoBehaviour {

     public int currentItem;
     private float dropTime;
     public Sprite spawn;
     public Sprite item1;
     private SpriteRenderer spriteKun;

	// Use this for initialization
	void Start () {
          spriteKun = GetComponent<SpriteRenderer>();
          currentItem = (int)(UnityEngine.Random.value*100) % 4 + 1;
	}
	
	// Update is called once per frame
	void Update () {
          if (currentItem == 0 && Time.time - dropTime > 3)
          {
               currentItem = (int)(UnityEngine.Random.value * 100) % 4 + 1;
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

     void OnTriggerEnter2D(Collider2D other)
     {
          if (currentItem != 0)
          {
               Debug.Log("dolan"); 
               takeItem();
          }
     }

     void takeItem()
     {
          currentItem = 0;
          dropTime = Time.time;
     }
}
