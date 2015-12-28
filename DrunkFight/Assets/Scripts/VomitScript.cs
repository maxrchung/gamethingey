using UnityEngine;
using System.Collections;

public class VomitScript : MonoBehaviour {

     public float vomitSpeed;
     private float time;

     void Start()
     {
          time = Time.time;
     }

     void Update()
     {
          transform.position += transform.up * Time.deltaTime * vomitSpeed;
          if (Time.time - time > 5)
          {
               GameObject.Destroy(gameObject);
          }
     }
}
