using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	private Transform transform;


	private void Start()
	{
		transform = GetComponent<Transform>();
	}


	private void Update()
	{
		
		Vector3 movementVector = new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal"));
		transform.Translate(movementVector * Time.deltaTime * 5);
	}
}
