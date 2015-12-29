using UnityEngine;

public class AttackHitScript : MonoBehaviour
{

    public float damage;
    public float knockback;
    public float slow;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 knockbackForce = (other.transform.position - transform.position).normalized * knockback;
            other.gameObject.GetComponent<Movement>().ApplyHit(damage, knockbackForce, slow);
        }
    }
}
