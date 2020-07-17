using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class rotate : MonoBehaviour
{
    float rotationSpeed;
	bool swipe = false;
	float startXPos = 0;

	void Start()
    {
		rotationSpeed = settings.instance.rotationSpeed;
	}

    // Update is called once per frame
    void Update()
    {
		Rotate();
	}
	void Rotate()
	{
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
		{

			if (Input.touchCount == 1)
			{
				if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Stationary)
				{
					startXPos = Input.GetAxis("Mouse X");
					swipe = true;
				}
				else if (Input.GetTouch(0).phase == TouchPhase.Ended)
				{
					swipe = false;
				}

				if (swipe)
				{
					transform.Rotate(new Vector3(0, (Input.GetAxis("Mouse X") - startXPos) * -1, 0) * rotationSpeed * Time.deltaTime);
				}
			}


		}
		else
		{
			if (Input.GetMouseButton(0))
			{
				transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * -1, 0) * rotationSpeed * Time.deltaTime);
			}
		}
	}
	
}
