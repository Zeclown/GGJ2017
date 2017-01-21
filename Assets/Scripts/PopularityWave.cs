using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PopularityWave")]
public class PopularityWave : ScriptableObject {

    public AnimationCurve frequencyWave;
    public Genre genreName;
    public bool active;                 //Set by gamestate.
    private float activatedSyncOffset;   //Set on spawn.
    private float randomStartOffset;     //Set on spawn.

    public float ReadFromCurve(float time) {
        if (!active)
            return 0;
        return frequencyWave.Evaluate(time + randomStartOffset + activatedSyncOffset);
    }

    public bool Activate(float time) {
        if (active)
            return false;
        active = true;
        randomStartOffset = UnityEngine.Random.Range(-5f, 0f);
        return true;
    }
}
