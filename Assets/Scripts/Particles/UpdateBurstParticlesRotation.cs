using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateBurstParticlesRotation : MonoBehaviour
{
    private Transform burstParticle;
	private void Awake()
	{
		burstParticle = transform.GetChild(0);
	}
	void Update()
    {
       burstParticle.Rotate(transform.rotation.eulerAngles + new Vector3(90, 0, 0));
    }
}
