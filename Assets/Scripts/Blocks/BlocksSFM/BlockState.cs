using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BlockState
{
	public virtual void Start(BlockFSMBase b) { }
	public virtual void Update(BlockFSMBase b) { }
	public virtual void FixedUpdate(BlockFSMBase b) { }
	public virtual void Exit(BlockFSMBase b) { }
	public virtual void OnDestroy(BlockFSMBase b) { }
	public virtual void OnCollisionEnter(UnityEngine.Collision collision) { }
}