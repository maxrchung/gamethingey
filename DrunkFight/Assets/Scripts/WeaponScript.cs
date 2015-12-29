using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class WeaponScript : NetworkBehaviour
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
        if (currentWeapon == 1)
        {
            charges = 10;
        }
        if (currentWeapon == 2)
        {
            charges = 2;
        }
        else if (currentWeapon == 3)
        {
            charges = 5;
        }
    }
    
    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetMouseButtonDown(0) && Time.time > timer)
        {
            if (currentWeapon <= 0)
            {
                CmdPunch();
            }
            else if (currentWeapon == 1)
            {
                CmdFireVomit();
                charges = decreaseCharge(charges);
            }
            else if (currentWeapon == 2)
            {
                CmdDropPoop();
                charges = decreaseCharge(charges);
            }
            else if (currentWeapon == 3)
            {
                CmdFireFire();
                charges = decreaseCharge(charges);
            }
            else if (currentWeapon >= 4)
            {
                //fireFire();
            }
        }
    }

    int decreaseCharge(int charge)
    {
        charge -= 1;
        if (charge <= 0)
        {
            currentWeapon = 0;
        }
        return charge;
    }

    [Command]
    void CmdPunch()
    {
        timer = Time.time + vomitDelay;
        GameObject punchObj = (GameObject)Instantiate(fist);
        punchObj.transform.SetParent(transform, true);
        NetworkServer.Spawn(punchObj);
    }

    [Command]
    void CmdFireFire()
    {
        timer = Time.time + vomitDelay;
        GameObject fireObj = (GameObject)Instantiate(fire, fireSpawnPoint.transform.position, fireSpawnPoint.transform.rotation);
        fireObj.transform.SetParent(transform, true);
        NetworkServer.Spawn(fireObj);
    }

    [Command]
    void CmdFireVomit()
    {
        timer = Time.time + vomitDelay;
        GameObject vomitObj = (GameObject)Instantiate(vomit, transform.position, transform.rotation);
        NetworkServer.Spawn(vomitObj);
    }

    [Command]
    void CmdDropPoop()
    {
        timer = Time.time + vomitDelay;
        GameObject dropObj = (GameObject)Instantiate(poop, transform.position, transform.rotation);
        NetworkServer.Spawn(dropObj);
    }
}