using System.Collections;
using UnityEngine;

namespace ReptilianCabal.MineMineMine
{
	public static class GameObjectExtensions
	{
		public static IEnumerator DestroyAfterTime(this GameObject target, float time)
		{
			yield return new WaitForSeconds(time);
			Object.Destroy(target);
		}
	}
}
