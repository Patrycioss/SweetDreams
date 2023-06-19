using _Scripts;
using UnityEngine;
public class Finish : MonoBehaviour
{
	[SerializeField] private int[] _testRanking;

	public Vector3[] positions = new Vector3[4];

	public void PlayAgain()
	{
		God.instance.SwapScene("UserInterface");
	}

	private void Start()
	{
		int[] ranking;
		
		foreach (int i in God.instance.ranking)
		{
			Debug.Log(i);
		}
		
		if (God.instance.ranking[0] == -1)
		{
			Debug.Log("yes");
			ranking = _testRanking;
		}
		else ranking = God.instance.ranking;

		for (int i = 0; i < ranking.Length; i++)
		{
			if (ranking[i] == -1) continue;
			GameObject player = Instantiate(God.instance.animatedPlayers[ranking[i]]);
			player.transform.position = positions[i];
			player.transform.parent = transform;
			Animator animator = player.GetComponent<Animator>();
			if(i == 0)
				animator.SetTrigger("SelectRepeat");
			else
				animator.SetTrigger("Sulk");
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
