using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using _Scripts;
using OpenCover.Framework.Model;
using UnityEngine;
using File = System.IO.File;

public class Finish : MonoBehaviour
{
	[SerializeField] private int[] _testRanking;

	public Vector3[] positions = new Vector3[4];

	private void Start()
	{
		int[] ranking;
		
		if (God.instance.ranking[0] == -1)
		{
			ranking = _testRanking;
		}
		else ranking = God.instance.ranking;

		for (int i = 0; i < ranking.Length; i++)
		{
			GameObject player = Instantiate(God.instance.animatedPlayers[ranking[i]]);
			player.transform.position = positions[i];
			player.transform.parent = transform;
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		for (int i = 0; i < positions.Length; i++)
		{
			Gizmos.DrawSphere(positions[i], 0.3f);
		}
	}
}
