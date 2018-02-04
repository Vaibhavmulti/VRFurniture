using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeInput : MonoBehaviour {
    private GameObject previousGaze;
    private GameObject currentSelected;
    private RaycastHit lastHit;

    public GameObject reticle;
    public Color initialColor = Color.grey;
    public Color colorChange = Color.green;
	// Use this for initialization
	void Start () {
        ChangeColor(initialColor);
	}
	
	// Update is called once per frame
	void Update () {
        HeadGaze();
        CheckForInput(lastHit);
	}

    void HeadGaze()
    {
        Ray headGaze = new Ray(transform.position, transform.forward);
        RaycastHit hitted;
        Debug.DrawRay(transform.position,transform.forward*100);
        if(Physics.Raycast(headGaze,out hitted))
        {
            GameObject currentGameObject= hitted.transform.gameObject;
            lastHit = hitted;
            if (currentGameObject.GetComponent<GazableObject>())
            {
                if (previousGaze == null)
                {
                    previousGaze = currentGameObject;
                    ChangeColor(colorChange);
                    currentGameObject.GetComponent<GazableObject>().OnGazeEnter(hitted);
                }
                if (previousGaze == currentGameObject)
                {
                    currentGameObject.GetComponent<GazableObject>().OnGazeStay(hitted);
                }
                if(previousGaze!=currentGameObject)
                {
                    previousGaze.GetComponent<GazableObject>().OnGazeLeave();
                    previousGaze = currentGameObject;
                    currentGameObject.GetComponent<GazableObject>().OnGazeEnter(hitted);
                }
            }
            else
            {
                if (previousGaze != null)
                    previousGaze.GetComponent<GazableObject>().OnGazeLeave();
                ClearGaze();
            }  
        }
        else
            ClearGaze();
    }

    void CheckForInput(RaycastHit ray)
    {
        if(Input.GetButtonDown("Fire1") && previousGaze!=null)
        {
            previousGaze.GetComponent<GazableObject>().OnButtonPress(ray);
            currentSelected = previousGaze;
        }
        if (Input.GetButton("Fire1") && currentSelected != null)
        {
            currentSelected.GetComponent<GazableObject>().OnButtonHold(ray);
        }
        if (Input.GetButtonUp("Fire1") && currentSelected != null)
        {
            currentSelected.GetComponent<GazableObject>().OnButtonRelease(ray);
 
        }

    }

    void ClearGaze()
    {
        previousGaze = null;
        ChangeColor(initialColor);
    }

    void ChangeColor(Color colour)
    {
        reticle.GetComponent<MeshRenderer>().material.color = colour;
    }
}
