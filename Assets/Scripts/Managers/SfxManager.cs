using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SfxManager", menuName = "ScriptableObjects/SfxManager")]
public class SfxManager : ScriptableObject
{
	[SerializeField] private AudioClip _chargeSound;
	[SerializeField] private AudioClip _lineBlowSound;

	public AudioClip ChargeSound { get => _chargeSound; private set => _chargeSound = value; }
	public AudioClip LineBlowSound { get => _lineBlowSound; private set => _lineBlowSound = value; }
}
