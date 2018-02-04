using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazableObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnGazeEnter(RaycastHit hit)
    {
        Debug.Log("GazeEntered"+hit.transform.name);
    }

    public void OnGazeStay(RaycastHit hit)
    {
        Debug.Log("GazeStay"+ hit.transform.name);
    }

    public void OnGazeLeave()
    {
        Debug.Log("GazeLeft");
    }

    public void OnButtonPress(RaycastHit hit)
    {
        Debug.Log("button pressed ");
    }

    public void OnButtonHold(RaycastHit hit)
    {
        Debug.Log("button Holded ");
    }

    public void OnButtonRelease(RaycastHit hit)
    {
        Debug.Log("button released ");
    }
}
