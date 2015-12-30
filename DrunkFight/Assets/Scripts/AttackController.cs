using UnityEngine;
using System.Collections;

public class AttackController : MonoBehaviour {
	public string animName;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		AnimatorStateInfo asi = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);

		if(!asi.IsName(animName) || asi.normalizedTime >= 1) {
            gameObject.SetActive(false);
		}
	}
}
