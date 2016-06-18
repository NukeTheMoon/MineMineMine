using PigeonCoopToolkit.Effects.Trails;
using ReptilianCabal.MineMineMine;
using UnityEngine;

public class DestroyAfterTrailLifetime : MonoBehaviour
{
	private Trail _trail;


	private void Start()
	{
		_trail = GetComponent<Trail>();
		StartCoroutine(gameObject.DestroyAfterTime(2));

	}
}
