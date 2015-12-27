using UnityEngine;
using System.Collections;

public class ItemDropScript : MonoBehaviour {

     public int currentItem;
     private float dropTime;

	// Use this for initialization
	void Start () {
          currentItem = (int)(UnityEngine.Random.value*100)%4;
	}
	
	// Update is called once per frame
	void Update () {
          if (currentItem == 0 && Time.time - dropTime > 20)
          {
               currentItem = (int)(UnityEngine.Random.value * 100) % 4;
          }
	}

     void takeItem()
     {
          currentItem = 0;
          dropTime = Time.time;
     }
}
