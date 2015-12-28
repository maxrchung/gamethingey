using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour
{

     public float fistDelay;
     public float vomitDelay;
     public float poopDelay;
     public float flameDelay;
     public int currentWeapon;
     public float timer = 0;
     public GameObject vomit;
     public GameObject poop;

     public void getWeapon(int weapon)
     {
          currentWeapon = weapon;
     }

     void Update()
     {
          if (Input.GetMouseButtonDown(0) && Time.time > timer)
          {

               if (currentWeapon <= 0)
               {
                    
               }
               else if (currentWeapon == 1)
               {   
                    fireVomit();
               }
               else if (currentWeapon == 2)
               {
                    dropPoop();
               }
               else if (currentWeapon >= 3)
               {

               }
               Debug.Log("click");
          }  
     }

     void fireVomit()
     {
          timer = Time.time + vomitDelay;
          Instantiate(vomit, transform.position, transform.rotation);
          Debug.Log("vomit");
     }

     void dropPoop()
     {
          timer = Time.time + vomitDelay;
          Instantiate(poop, transform.position, transform.rotation);
          Debug.Log("poop");
     }




}