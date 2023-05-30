using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextBlockDisplayer : MonoBehaviour
{
	[SerializeField][Range(0, 4)] private int listIndexToDisplay = 0;
	private GameObject displayedBlock;
	private void Awake()
	{
		EventManager.Instance.OnNextBlocksChange.AddListener(DisplayBlock);
	}
	private void DisplayBlock(List<GameObject> blocks)
	{
		if (displayedBlock != null)
		{
			Destroy(displayedBlock);
			displayedBlock = null;
		}
		if (blocks.Count - 1 < listIndexToDisplay) return;

		displayedBlock = Instantiate(blocks[listIndexToDisplay]);
		var b = displayedBlock.GetComponent<BlockFSMBase>();
		b.StartBlock(b.DisabledState);
		displayedBlock.transform.position = transform.position;
	}
}
