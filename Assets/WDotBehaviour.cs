using UnityEngine;
using System.Collections;

public class WDotBehaviour : MonoBehaviour {
    public Sprite b_dot;
    public Sprite w_dot;
    SpriteRenderer sRend;
    public Color black;
    public Color white;
	public Color red;
    public GameObject particle;
    public static GameObject first_position;
    public static GameObject p1, p2;
    public GameObject w_line;
    public static int last_position = 0;
	public PhysicsMaterial2D big_bounce;
	public int dot_index { get; set; }
	public int row { get; set; }
	public int column { get; set; }
    public bool is_neighbour = false;

	// Use this for initialization
	void Start () {
        sRend = gameObject.GetComponent<SpriteRenderer>();
		
	}
	
	// Update is called once per frame

    void OnMouseDown()
    {
		CheckTriangle();
       // print("Row:" + row + " Column:" + column);
        if (last_position == 0)
        {
            CheckDrawing();
            ChangeDot();
            CheckNeighbours();
        }
       // print("Tijana voli Nikolu puno mnogo malkice najvelkice");

    }


    void CheckDrawing()
    {
        //ako je nula setuj na 1, pa onda menjaj na 1,2,1,2,1,2 po svakom pozivu
        if (last_position == 0)
        {
            last_position++;
            p1 = gameObject;
        }
        else
        {
            if (last_position == 1)
            {
                last_position++;
                p2 = gameObject;
                DrawLine(p2,p1);
                CheckNeighbours();
            }
            else if (last_position == 2)
            {
                last_position--;
                p1 = gameObject;
                DrawLine(p1, p2);
                CheckNeighbours();
            }
        }

    }

    void DrawLine(GameObject endDot, GameObject startDot)
    {
      //  print("DrawLine:" + endDot.name + startDot.name);
        int smaller_index = startDot.GetComponent<WDotBehaviour>().dot_index;
        int bigger_index = endDot.GetComponent<WDotBehaviour>().dot_index;
        if (smaller_index > bigger_index)
        {
            int temp = smaller_index;
            smaller_index = bigger_index;
            bigger_index = temp;
        }
        string line_name = "w_line_" + smaller_index + "_" + bigger_index;
        if (GameObject.Find(line_name))
        {
            Destroy(GameObject.Find(line_name));
        }
        else
        {

            if (endDot != startDot)
            {
                GameObject the_line = Instantiate(w_line, endDot.transform.position, Quaternion.identity) as GameObject;
                the_line.name = "w_line_" + smaller_index + "_" + bigger_index;
                SpriteRenderer spr = the_line.GetComponent<SpriteRenderer>();
                spr.sortingLayerName = "Default";
                spr.sortingOrder = -30;
                float lineWidth = spr.sprite.bounds.size.x;
                Vector3 dir = startDot.transform.position - endDot.transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                the_line.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                the_line.transform.localScale = new Vector3((dir.magnitude / lineWidth), 1, 1);
            }
        }
    }
    void OnMouseEnter()
    {
        if (Input.GetMouseButton(0) && is_neighbour)
        {
            
            CheckDrawing();
            ChangeDot();
        }
        transform.localScale += (Vector3.one * 0.2f);
        
    }

    void OnMouseExit()
    {
        transform.localScale -= (Vector3.one * 0.2f);

    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            last_position = 0;
        }

        if (Input.GetMouseButtonDown(1))
        {
			//pobeli sve tacke
            foreach (GameObject dot in GameObject.FindGameObjectsWithTag("dot"))
            {
                SpriteRenderer spr = dot.GetComponent<SpriteRenderer>();
                spr.sprite = dot.GetComponent<WDotBehaviour>().w_dot;
            }
			//unisti sve linije
            foreach (GameObject line in GameObject.FindGameObjectsWithTag("line"))
            {
                Destroy(line);
            }

			//unisti sve loptice
			foreach (GameObject ball in GameObject.FindGameObjectsWithTag("ball"))
			{
				Destroy(ball);
			}
        }
    }

    void ChangeDot()
    {
        Instantiate(particle, transform.position, Quaternion.identity);
        if (sRend.sprite == w_dot)
        {
            sRend.sprite = b_dot;
        }
        else if (sRend.sprite == b_dot)
        {
            sRend.sprite = w_dot;
        }
    }
    void CheckNeighbours()
    {
        GameObject[] all_dots = GameObject.FindGameObjectsWithTag("dot");
        foreach (GameObject dot in all_dots)
        {
            WDotBehaviour refDot = dot.GetComponent<WDotBehaviour>();

            refDot.is_neighbour = false;
           // dot.transform.localScale = Vector3.one;
            int ref_column = refDot.column;
            int ref_row = refDot.row;

            int row_distance = Mathf.Abs(row - ref_row);
            int column_distance = Mathf.Abs(column - ref_column);

            if ((row_distance + column_distance) == 1 ||  (row_distance == 1 && column_distance == 1))
            {
                refDot.is_neighbour = true;
                //dot.transform.localScale *= 0.7f;
            }
        }
    }

	void CheckTriangle()
	{

		//troguao iz kvadranta 1
		
		//desna tacka
		int right_dot_index = dot_index + 1;
		string right_dot_name = "w_dot_" + right_dot_index;

		//gornja tacka
		int up_dot_index = dot_index - GenerateMatrix.MatrixWidth;
		string up_dot_name = "w_dot_" + up_dot_index;

		//ako postoje te tacke
		if (GameObject.Find(right_dot_name) && GameObject.Find(up_dot_name))
		{	
			//setovanje stringova linija
			string right_line_name = "w_line_" + dot_index + "_" + right_dot_index;
			string up_line_name = "w_line_" + up_dot_index + "_" + dot_index;
			string diagonal_line_name = "w_line_" + up_dot_index + "_"	+ right_dot_index;

			//provera dal postoje sve tri linije
			if (GameObject.Find(right_line_name) &&
				GameObject.Find(up_line_name) &&
				GameObject.Find (diagonal_line_name)
				)
			{
				//stuff ako postoji trougao
				//DO
				print("Trougao napravljen izmedju:" + dot_index + "_" + right_dot_index + "_" + up_dot_index);
				
				//oboj tacke
				GameObject.Find(right_dot_name).GetComponent<SpriteRenderer>().color = red;
				GameObject.Find(up_dot_name).GetComponent<SpriteRenderer>().color = red;
				sRend.color = red;

				//promeni materijal na dijagonali
				GameObject.Find(diagonal_line_name).GetComponent<BoxCollider2D>().sharedMaterial = big_bounce;
			}
		}


	}


}
