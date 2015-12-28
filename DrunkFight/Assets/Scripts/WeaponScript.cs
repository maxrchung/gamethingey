using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour
{

     private int currentWeapon = 0;

     public void getWeapon(int weapon)
     {
          currentWeapon = weapon;
     }

}