using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour {


	public Transform R_move;
	public Transform L_move;
	public Transform U_move;
	public Transform D_move;
	public Transform F_move;
	public Transform B_move;
	public Transform M_move;
	public Transform E_move;
	public Transform[] cubes;

	public float turnsPerSecond;

	private IDictionary<string,Transform> movements;
	private bool isTurning = false;

    // variables to define rotation
    private Transform rotatingside; 
	private bool reversed = false;
    private string rotatingaxis = "z";
    private Quaternion originalrotation;
	private Vector3 targetEulerrotation;
    private Quaternion targetrotation;
    float progress;


    private bool active;

	// Use this for initialization
	void Start () {
		movements = new Dictionary<string, Transform> ();
		movements.Add ("R", R_move);
		movements.Add ("L", L_move);
		movements.Add ("U", U_move);
		movements.Add ("D", D_move);
		movements.Add ("F", F_move);
		movements.Add ("B", B_move);
		movements.Add ("M", M_move);
		movements.Add ("E", E_move);

		active = true;
	}

    // Update is called once per frame
    void Update()
    {

        if (GetActive())
        {
            if (isTurning == false)
            {
                if (DefineRotation())
                {
                    Rotate(rotatingside, reversed, rotatingaxis);
                }

            }
            else {
                Rotate(rotatingside, reversed, rotatingaxis);
            }
            
        }


    }

    public bool GetActive()
    {
        return active;
    }

    public void SetActive(bool value)
    {
        active = value;
    }

    bool DefineRotation()
	{
		bool hit = false;

		if (Input.GetKeyDown (KeyCode.R) && Input.GetKey (KeyCode.LeftShift)) {
			rotatingside = movements ["R"];
			reversed = true; hit = true; rotatingaxis = "z";
		} else if (Input.GetKeyDown (KeyCode.R)) {
			rotatingside = movements ["R"];
			reversed = false; hit = true; rotatingaxis = "z";
        } else if (Input.GetKeyDown (KeyCode.L) && Input.GetKey (KeyCode.LeftShift)) {
			rotatingside = movements ["L"];
			reversed = true; hit = true; rotatingaxis = "z";
        } else if (Input.GetKeyDown (KeyCode.L)) {
			rotatingside = movements ["L"];
			reversed = false; hit = true; rotatingaxis = "z";
        } else if (Input.GetKeyDown (KeyCode.U) && Input.GetKey (KeyCode.LeftShift)) {
			rotatingside = movements ["U"];
			reversed = false; hit = true; rotatingaxis = "y";
        } else if (Input.GetKeyDown (KeyCode.U)) {
			rotatingside = movements ["U"];
			reversed = true; hit = true; rotatingaxis = "y";
        } else if (Input.GetKeyDown (KeyCode.D) && Input.GetKey (KeyCode.LeftShift)) {
			rotatingside = movements ["D"];
			reversed = true; hit = true; rotatingaxis = "y";
        } else if (Input.GetKeyDown (KeyCode.D)) {
			rotatingside = movements ["D"]; rotatingaxis = "y";
            reversed = false; hit = true;
		} else if (Input.GetKeyDown (KeyCode.F) && Input.GetKey (KeyCode.LeftShift)) {
			rotatingside = movements ["F"]; rotatingaxis = "z";
            reversed = true; hit = true;
		} else if (Input.GetKeyDown (KeyCode.F)) {
			rotatingside = movements ["F"];
			reversed = false; hit = true; rotatingaxis = "z";
        } else if (Input.GetKeyDown (KeyCode.B) && Input.GetKey (KeyCode.LeftShift)) {
			rotatingside = movements ["B"];
			reversed = true; hit = true; rotatingaxis = "z";
        } else if (Input.GetKeyDown (KeyCode.B)) {
			rotatingside = movements ["B"];
			reversed = false; hit = true; rotatingaxis = "z";
        } else if (Input.GetKeyDown (KeyCode.M) && Input.GetKey (KeyCode.LeftShift)) {
			rotatingside = movements ["M"]; rotatingaxis = "z";
            reversed = true; hit = true;
		} else if (Input.GetKeyDown (KeyCode.M)) {
			rotatingside = movements ["M"];
			reversed = false; hit = true; rotatingaxis = "z";
        } else if (Input.GetKeyDown (KeyCode.E) && Input.GetKey (KeyCode.LeftShift)) {
			rotatingside = movements ["E"];
			reversed = false; hit = true; rotatingaxis = "y";
        } else if (Input.GetKeyDown (KeyCode.E)) {
			rotatingside = movements ["E"];
			reversed = true; hit = true; rotatingaxis = "y";
        }

		if (hit) {
		
			UpdateParents(rotatingside);
		}

		return hit;
	}

    private void Rotate (Transform side, bool changedirection, string rotatingaxis){
      
        if (!isTurning)
        {
            if (changedirection)
            {
                originalrotation = side.localRotation;
                targetrotation = side.localRotation * Quaternion.AngleAxis(-90, Vector3.forward);
                Debug.Log(originalrotation.eulerAngles + " " + targetrotation.eulerAngles);
            }
            else {
                originalrotation = side.localRotation;
                targetrotation = side.localRotation * Quaternion.AngleAxis(90, Vector3.forward);
            }

        }

		isTurning = true;
        progress += turnsPerSecond * Time.deltaTime;
        progress = Mathf.Clamp01(progress);
        side.rotation = Quaternion.Lerp(originalrotation, targetrotation, progress);
        

        if (rotatingaxis == "x")
        {
            if (Mathf.Abs(side.rotation.eulerAngles.x - targetrotation.eulerAngles.x) < 0.01f)
            {
                progress = 0f;
                isTurning = false;
                side.rotation = targetrotation;
            }
        }
        else if (rotatingaxis == "y")
        {
            if (Mathf.Abs(side.rotation.eulerAngles.y - targetrotation.eulerAngles.y) < 0.01f)
            {
                progress = 0f;
                isTurning = false;
                side.rotation = targetrotation;
            }
        }
        else 
        {
            if (Mathf.Abs(side.rotation.eulerAngles.z - targetrotation.eulerAngles.z) < 0.01f)
            {
                progress = 0f;
                isTurning = false;
                side.rotation = targetrotation;
            }
        }



    }

	private void UpdateParents(Transform overlapCheck){

		BoxCollider collider = overlapCheck.GetComponent<BoxCollider> ();
	
		foreach (Transform t in cubes) {
			BoxCollider cubecollider = t.GetComponent<BoxCollider> ();
			if (collider.bounds.Intersects (cubecollider.bounds))
				t.parent = overlapCheck.transform;
		}
	
	}

}
