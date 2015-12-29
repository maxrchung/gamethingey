using UnityEngine;
using System.Collections.Generic;

public class AttackHitScript : MonoBehaviour
{
    public float damage;
    public float knockback;
    public float slow;

    private string playerId;

    public float armDelay;
    public bool diesOnHit;
    public bool hitsSelf;

    HashSet<string> hitPlayers = new HashSet<string>();

    private void FixedUpdate()
    {
        if (armDelay > 0)
        {
            armDelay -= Time.fixedDeltaTime;
        }
    }

    public void setPlayerId (string playerId)
    {
        this.playerId = playerId;
    }

    private void OnTriggerStay2D (Collider2D other)
    {
        //Debug.Log(other.gameObject.transform.tag);
        if (armDelay <= 0 && other.tag == "Player")
        {
            string hitPlayerId = other.GetComponent<Movement>().playerId;
            if (!hitPlayers.Contains(hitPlayerId) && 
                (hitPlayerId != playerId || hitsSelf))
            {
                hitPlayers.Add(hitPlayerId);
                Vector3 knockbackForce = (other.transform.position - transform.position).normalized * knockback;
                other.GetComponent<Movement>().ApplyHit(playerId, damage, knockbackForce, slow);
                if (diesOnHit)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public void ClearHitPlayers()
    {
        hitPlayers.Clear();
    }
}
