using UnityEngine;
using System.Collections;

public class VomitScript : MonoBehaviour {

     public float vomitSpeed;
     void Update()
     {
          transform.position += transform.up * Time.deltaTime * vomitSpeed;
     }
}
