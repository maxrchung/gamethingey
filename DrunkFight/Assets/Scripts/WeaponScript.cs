using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class WeaponScript : NetworkBehaviour
{

    public float fistDelay;
    public float vomitDelay;
    public float poopDelay;
    public float flameDelay;
	public int currentWeapon;
    private float timer = 0;
    public int charges;
    public GameObject vomit;
    public GameObject fire;
    public GameObject poop;
    public GameObject fist;
    public GameObject fireSpawnPoint;

    void Start() {
        foreach(Transform child in gameObject.transform) {
            if(child.transform.tag == "FIREBOMB") {
                child.GetComponent<AttackHitScript>().setPlayerId(netId.ToString());
            }
            else if(child.transform.tag == "FISTOFFAME") {
                child.GetComponent<AttackHitScript>().setPlayerId(netId.ToString());
            }
        }  
    }

    public void getWeapon(int weapon)
    {
        // Not using healing or extra things atm
        if (weapon >= 4)
        {
            weapon = 0;
			charges = 999;
        }
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
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && Time.time > timer)
        {
            timer = Time.time + vomitDelay;
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
			charges = 999;
        }
        return charge;
    }

    [Command]
    void CmdPunch()
    {
        RpcPunch(gameObject);
    }

    [ClientRpc]
    void RpcPunch(GameObject ugh) {
        foreach(Transform child in ugh.transform) {
            if(child.transform.tag == "FISTOFFAME") {
                child.transform.gameObject.SetActive(true);
            }
        }    
    }

    [Command]
    void CmdFireFire()
    {
        RpcFireFire(gameObject);
    }

    [ClientRpc]
    void RpcFireFire(GameObject ugh) {
        foreach(Transform child in ugh.transform) {
            if(child.transform.tag == "FIREBOMB") {
                child.transform.gameObject.SetActive(true);
            }
        }   
    }

    [Command]
    void CmdFireVomit()
    {
        GameObject vomitObj = (GameObject)Instantiate(vomit, transform.position, transform.rotation);
        vomitObj.GetComponent<AttackHitScript>().setPlayerId(netId.ToString());
        NetworkServer.Spawn(vomitObj);
    }

    [Command]
    void CmdDropPoop()
    {
        GameObject dropObj = (GameObject)Instantiate(poop, transform.position, transform.rotation);
        dropObj.GetComponent<AttackHitScript>().setPlayerId(netId.ToString());
        NetworkServer.Spawn(dropObj);
    }
}