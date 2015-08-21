using UnityEngine;
using System.Collections;

public class GenerateMatrix : MonoBehaviour {
    public GameObject w_dot;
    public float matrix_spacing = 1f;
    public int matrix_width = 4;
    public int matrix_height = 6;
	// Use this for initialization
	void Awake () {
        for (int i=0; i < matrix_height;i++) {
            for (int j = 0; j < matrix_width; j++)
            {
                //setting coordinates for creating
                float x = transform.position.x + (j * matrix_spacing);
                float y = transform.position.y - (i * matrix_spacing);
                float z = transform.position.z;
                Vector3 matrix_position = new Vector3(x, y, z);

                //creating
                GameObject instantiation = Instantiate(w_dot, matrix_position, Quaternion.identity) as GameObject;
                
                int index = i*matrix_width + j;
                instantiation.name = "w_dot_" + index;
                instantiation.GetComponent<WDotBehaviour>().dot_index = index;
                instantiation.GetComponent<WDotBehaviour>().row = i;
                instantiation.GetComponent<WDotBehaviour>().column = j;
            }
        }
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
