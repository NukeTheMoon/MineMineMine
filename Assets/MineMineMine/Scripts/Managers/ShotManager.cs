using Lexicons;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.MineMineMine.Scripts.Managers
{
	public class ShotManager : MonoBehaviour
	{

		public Lexicon<Guid, int> PairedShots = new Lexicon<Guid, int>();
		public Dictionary<int, GameObject> MissileIdReference = new Dictionary<int, GameObject>();
		public List<int> ProtectedMissileIds = new List<int>();

		public void Awake()
		{
			RegisterWithSceneReference();
		}

		private void RegisterWithSceneReference()
		{
			SceneReference.ShotManager = this;
		}

		public void RegisterShot(Guid shotGuid, GameObject missile)
		{
			int missileId = missile.GetInstanceID();
			PairedShots.Add(shotGuid, missileId);
			MissileIdReference[missileId] = missile;
		}

		// TODO: refactor messy method
		public void DestroyMissilesFromSameShot(GameObject destroyedMissile)
		{
			int destroyedMissileId = destroyedMissile.GetInstanceID();
			if (ProtectedMissileIds.Contains(destroyedMissileId)) return;
			Guid shotGuid = new Guid();
			foreach (KeyValuePair<Guid, int> kip in PairedShots.FindKeyIndexPairs(destroyedMissileId))
			{
				shotGuid = kip.Key;
				break;
			}
			List<int> idsOfMissilesToDestroy;
			PairedShots.TryGetValueList(shotGuid, out idsOfMissilesToDestroy);
			if (idsOfMissilesToDestroy == null) return;
			for (var i = 0; i < idsOfMissilesToDestroy.Count; ++i)
			{
				var id = idsOfMissilesToDestroy[i];
				if (id == destroyedMissileId) continue;
				GameObject missile = MissileIdReference[id];

				if (missile == null) continue;

				ProtectedMissileIds.Add(id);

				DelayedDestruction _destruction = missile.GetComponent<DelayedDestruction>();

				if (_destruction != null)
				{
					_destruction.InitiateDestruction();
				}
				else
				{
					Destroy(missile);
				}
			}
		}

	}
}
