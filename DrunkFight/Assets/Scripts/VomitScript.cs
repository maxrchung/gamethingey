using UnityEngine;
using System.Collections;

public class VomitScript : MonoBehaviour {

     public float vomitSpeed;
     private float time;
     public bool danger;

     void Start()
     {
          time = Time.time;
          danger = false;
     }

     void Update()
     {
          transform.position += transform.up * Time.deltaTime * vomitSpeed;
          if (Time.time - time > 5)
          {
               GameObject.Destroy(gameObject);
          }
          if (Time.time - time > 0.5)
          {
               danger = true;
          }
     }

     void OnTriggerEnter2D(Collider2D other)
     {
          if (other.tag == "Wall-Chan")
          {
               Destroy(gameObject);
          }

          Debug.Log("dolan");
          Debug.Log(other.tag);
     }
}
