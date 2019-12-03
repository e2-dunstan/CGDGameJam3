using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientAudio : MonoBehaviour
{
    FMOD.Studio.EventInstance windEvent;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        windEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(PlayerManager.Instance().players[0].gameObject));

        if (!isPlaying(windEvent))
        {
            string windSound = "event:/Ambience/Wind";
            var windRandomValue = Random.Range(1, 3);
            windSound = windSound.Insert(windSound.Length, windRandomValue.ToString());
            windEvent = FMODUnity.RuntimeManager.CreateInstance(windSound);
            windEvent.start();
            print(windEvent +windSound);

        }
    }

    bool isPlaying(FMOD.Studio.EventInstance _event)
    {
        FMOD.Studio.PLAYBACK_STATE playbackState;
        _event.getPlaybackState(out playbackState);

        return playbackState != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }
}
