using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Lexicons;

public class ShotManager : MonoBehaviour {

    public static Lexicon<Guid, int> PairedShots = new Lexicon<Guid, int>();
    public static Dictionary<int, GameObject> MissileIdReference = new Dictionary<int, GameObject>();
    public static List<int> ProtectedMissileIds = new List<int>();

    public static void RegisterShot(Guid shotGuid, GameObject missile)
    {
        var missileId = missile.GetInstanceID();
        PairedShots.Add(shotGuid, missileId);
        MissileIdReference[missileId] = missile;
    }

    public static void DestroyMissilesFromSameShot(GameObject destroyedMissile)
    {
        var destroyedMissileId = destroyedMissile.GetInstanceID();
        if (!ProtectedMissileIds.Contains(destroyedMissileId))
        {
            Guid shotGuid = new Guid();
            foreach (KeyValuePair<Guid, int> kip in PairedShots.FindKeyIndexPairs(destroyedMissileId))
            {
                shotGuid = kip.Key;
                break;
            }
            List<int> idsOfMissilesToDestroy;
            PairedShots.TryGetValueList(shotGuid, out idsOfMissilesToDestroy);

            for (var i = 0; i < idsOfMissilesToDestroy.Count; ++i)
            {
                var id = idsOfMissilesToDestroy[i];
                if (id != destroyedMissileId)
                {
                    var missile = MissileIdReference[id];
                    ProtectedMissileIds.Add(id);
                    Destroy(missile);
                }
            }
        }
    }

}
