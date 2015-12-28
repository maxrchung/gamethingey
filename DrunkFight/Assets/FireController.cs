using UnityEngine;
using System.Collections;

public class FireController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		AnimatorStateInfo asi = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);

		if(!asi.IsName("FireStream") || asi.normalizedTime >= 1) {
			Destroy(gameObject);
		}
	}
}
