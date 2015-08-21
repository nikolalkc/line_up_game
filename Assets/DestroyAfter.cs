using UnityEngine;
using System.Collections;

public class DestroyAfter : MonoBehaviour {
    public float time;
	// Use this for initialization
	void Start () {
        Invoke("DestroySelf", time);
	}
	
	// Update is called once per frame
	void DestroySelf () {
        Destroy(gameObject);
	}
}
