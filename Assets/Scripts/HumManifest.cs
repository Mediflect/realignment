using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hum Manifest")]
public class HumManifest : ScriptableObject
{
    [System.Serializable]
    public struct HumPair
    {
        public AudioClip tension;
        public AudioClip resolution;
    }

    public List<HumPair> hums = new List<HumPair>();

    public void SetUpRandom(DualLoopFader loopFader)
    {
        HumPair pair = hums[Random.Range(0, hums.Count)];
        loopFader.tension.clip = pair.tension;
        loopFader.resolution.clip = pair.resolution;
    }

}
