using UnityEngine;

public class AttackHitScript : MonoBehaviour
{
    public float damage;
    public float knockback;
    public float slow;

    private string playerId;

    public void setPlayerId (string playerId)
    {
        this.playerId = playerId;
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        Debug.Log(other.gameObject.transform.tag);
        if (other.tag == "Player")
        {
            Vector3 knockbackForce = (other.transform.position - transform.position).normalized * knockback;
            other.GetComponent<Movement>().ApplyHit(playerId, damage, knockbackForce, slow);
        }
    }
}
