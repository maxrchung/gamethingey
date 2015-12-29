using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour
{

     public float fistDelay;
     public float vomitDelay;
     public float poopDelay;
     public float flameDelay;
     private int currentWeapon;
     private float timer = 0;
	public int charges;
     public GameObject vomit;
     public GameObject fire;
     public GameObject poop;
	public GameObject fist;
     public GameObject fireSpawnPoint;
	 

     public void getWeapon(int weapon)
     {
          currentWeapon = weapon;
          if(currentWeapon == 1) {
               charges = 10;
          }
          if (currentWeapon == 2)
          {
               charges = 2;
          }
          else if(currentWeapon == 3) {
               charges =  5;
          }
     }

     void Update()
     {
          if (Input.GetMouseButtonDown(0) && Time.time > timer)
          {

               if (currentWeapon <= 0)
               {
					punch ();
               }
               else if (currentWeapon == 1)
               {   
                    fireVomit();
                    charges = decreaseCharge(charges);
               }
               else if (currentWeapon == 2)
               {
                    dropPoop();
                    charges = decreaseCharge(charges);
               }
               else if(currentWeapon == 3) {
                    fireFire();
                    charges = decreaseCharge(charges);
               }
               else if (currentWeapon >= 4)
               {
                    //fireFire();
               }
          }  
     }

     int decreaseCharge(int charge) {
          charge -= 1;
          if(charge <= 0) {
               currentWeapon = 0;
          }
          return charge;
     }

	void punch(){
		timer = Time.time + vomitDelay;
		GameObject punchObj = (GameObject) Instantiate(fist);
		punchObj.transform.SetParent(transform,true);
	}
     void fireFire() {
          timer = Time.time + vomitDelay;
          GameObject fireObj = (GameObject) Instantiate(fire, fireSpawnPoint.transform.position, fireSpawnPoint.transform.rotation);
          fireObj.transform.SetParent(transform, true);
     }

     void fireVomit()
     {
          timer = Time.time + vomitDelay;
          Instantiate(vomit, transform.position, transform.rotation);
     }

     void dropPoop()
     {
          timer = Time.time + vomitDelay;
          Instantiate(poop, transform.position, transform.rotation);
     }




}