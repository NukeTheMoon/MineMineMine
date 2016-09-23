using System.Collections;
using UnityEngine;

namespace ReptilianCabal.MineMineMine
{
    public static class GameObjectExtensions
    {
        public static IEnumerator DestroyAfterSeconds(this GameObject target, float time)
        {
            yield return new WaitForSeconds(time);
            Object.Destroy(target);
        }

        public static string GetPath(this GameObject current)
        {
            if (current.transform.parent == null)
                return "/" + current.name;
            return current.transform.parent.gameObject.GetPath() + "/" + current.name;
        }
    }
}
