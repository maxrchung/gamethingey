using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class AudioMasterScript : NetworkBehaviour {

    public AudioSource duckmusic;
    public AudioSource music;
    public AudioSource death;
    public AudioSource fireHit;
    public AudioSource fireShoot;
    public AudioSource poopShoot;
    public AudioSource poopVomitHit;
    public AudioSource punchHit;
    public AudioSource vomitShoot;
    public AudioSource pickUp;

	// Use this for initialization
	void Start ()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        bool allDucks = true;
        foreach (GameObject player in players)
        {
            if (player.GetComponent<Movement>().characterIndex != 2)
                allDucks = false;
        }
        if (allDucks)
            duckmusic.Play();
        else
            music.Play();
	}

    public void PlayHitSound(int attackIndex)
    {
        if (attackIndex == 0)
            punchHit.Play();
        else if (attackIndex == 1 || attackIndex == 2)
            poopVomitHit.Play();
        else if (attackIndex == 3)
            fireHit.Play();
    }

    public void PlayDeathSound()
    {
        death.Play();
    }

    public void PlayShootSound(int attackIndex)
    {
        if (attackIndex == 1)
            vomitShoot.Play();
        else if (attackIndex == 2)
            poopShoot.Play();
        else if (attackIndex == 3)
            fireShoot.Play();
    }

    public void PlayItemSound()
    {
        pickUp.Play();
    }
}
