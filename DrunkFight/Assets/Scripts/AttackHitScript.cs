using UnityEngine;
using System.Collections.Generic;

public class AttackHitScript : MonoBehaviour
{
    public float damage;
    public float knockback;
    public float slow;
    public float slowDuration;
	public string destroys;

    private string playerId;
    public int attackIndex;

    public float armDelay;
    public bool diesOnHit;
    public bool hitsSelf;

    HashSet<string> hitPlayers = new HashSet<string>();

    private AudioMasterScript audioMaster;
    private bool playedSound;

    private void FixedUpdate()
    {
        if (audioMaster == null)
        {
            audioMaster = GameObject.FindWithTag("AudioMaster").GetComponent<AudioMasterScript>();
            audioMaster.PlayShootSound(attackIndex);
        }
        if (armDelay > 0)
        {
            armDelay -= Time.fixedDeltaTime;
        }
    }

    private void OnEnable()
    {
        audioMaster.PlayShootSound(attackIndex);
    }

    public void setPlayerId(string playerId)
    {
        this.playerId = playerId;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log(other.gameObject.transform.tag);
        if (armDelay <= 0 && other.tag == "Player")
        {
            string hitPlayerId = other.GetComponent<Movement>().playerId;
            if (!hitPlayers.Contains(hitPlayerId) &&
                (hitPlayerId != playerId || hitsSelf) && 
                !other.GetComponent<Movement>().isDead)
            {
                //Debug.Log("Hit! " + hitPlayerId + " " + playerId);
                hitPlayers.Add(hitPlayerId);
                Vector3 knockbackForce = (other.transform.position - transform.position).normalized * knockback;
                other.GetComponent<Movement>().ApplyHit(playerId, damage, knockbackForce, slow, slowDuration);
                audioMaster.PlayHitSound(attackIndex);
                if (diesOnHit)
                {
                    Destroy(gameObject);
                }
            }
        }
		if (other.tag == destroys) {
			Destroy (other.gameObject);
		}
    }

    public void ClearHitPlayers()
    {
        hitPlayers.Clear();
    }
}
