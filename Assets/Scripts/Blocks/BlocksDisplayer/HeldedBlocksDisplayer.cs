using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldedBlocksDisplayer : MonoBehaviour
{ 
	private GameObject displayedBlock;
	private void Awake()
	{
		EventManager.Instance.OnHeldedBlocksChange.AddListener(DisplayBlock);
	}
	private void DisplayBlock(GameObject block)
	{
		if (displayedBlock != null)
		{
			Destroy(displayedBlock);
			displayedBlock = null;
		}

		displayedBlock = Instantiate(block);
		var b = displayedBlock.GetComponent<BlockFSMBase>();
		b.StartBlock(b.DisabledState);
		displayedBlock.transform.position = transform.position;
	}
}
