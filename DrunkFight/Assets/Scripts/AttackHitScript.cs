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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 knockbackForce = (other.transform.position - transform.position).normalized * knockback;
            other.gameObject.GetComponent<Movement>().ApplyHit(playerId, damage, knockbackForce, slow);
        }
    }
}
