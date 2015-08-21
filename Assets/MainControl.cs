using UnityEngine;
using System.Collections;

public class MainControl : MonoBehaviour {
	public GameObject real_ball;
	public GameObject ball_position;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (2)) {
			GameObject ball = Instantiate(real_ball) as GameObject;
			Vector3 starting_ball_position = ball_position.transform.position;
			ball.transform.position = starting_ball_position + new Vector3 (0.5f,0f,0f);
		}
	}
}
