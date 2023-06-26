using _Scripts;
using UnityEngine;
public class Finish : MonoBehaviour
{
	[SerializeField] private int[] _testRanking;
	public Transform[] playerPositions = new Transform[4];

	public void PlayAgain()
	{
		God.instance.SwapScene("UserInterface");
	}

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
			if (ranking[i] == -1) continue;
			GameObject player = Instantiate(God.instance.animatedPlayers[ranking[i]]);
			player.transform.position = playerPositions[i].position;
			player.transform.parent = transform;
			Animator animator = player.GetComponent<Animator>();
			if(i == 0)
				animator.SetTrigger("SelectRepeat");
			else
				animator.SetTrigger("Sulk");
		}
	}
}
