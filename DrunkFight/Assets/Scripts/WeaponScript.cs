using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour
{

     public float fistDelay;
     public float vomitDelay;
     public float poopDelay;
     public float flameDelay;
     public int currentWeapon = 0;
     public float timer = 0;
     public GameObject vomit;

     public void getWeapon(int weapon)
     {
          currentWeapon = weapon;
     }

     void Update()
     {
          if (Input.GetMouseButtonDown(0) && Time.time > timer)
          {
               fireVomit(); 
               /*
               if (currentWeapon >= 0)
               {

               }
               else if (currentWeapon == 1)
               {

               }
               else if (currentWeapon == 2)
               {

               }
               else if (currentWeapon <= 3)
               {

               }
                */
          }
     }

     void fireVomit()
     {
          timer = Time.time + vomitDelay;
          Instantiate(vomit, transform.position, transform.rotation);  
     }




}