using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AdjustBoardPosition : MonoBehaviour
{
	private void Awake()
	{
		if (transform.position.x % 1 != 0.5f)
		{
			transform.position = new Vector3((float)Math.Round(transform.position.x) + 0.5f, transform.position.y, transform.position.z);
		}
	}
}
