using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCollect : MonoBehaviour
{
	private void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("Player"))
		{
			GameManager.instance.MoneyCollect();
		}
	}
}
