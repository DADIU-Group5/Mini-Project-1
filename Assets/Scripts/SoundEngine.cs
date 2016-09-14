using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Used to update the UI. Is a singleton, access using UIController._instance.METHOD
/// </summary>
public class SoundEngine : MonoBehaviour {

    public static SoundEngine _instance;

    private IList<string> groups;
    private GameObject musicObject;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;

            groups = new List<string>();
            groups.Add("bass");
            groups.Add("bongo1");
            groups.Add("bongo2");
            groups.Add("drums");
            groups.Add("guitar1");
            groups.Add("guitar2");
            groups.Add("horns1");
            groups.Add("horns2");
            groups.Add("horns3");
        }
        else
        {
            Debug.LogError("There should not be 2 SoundEngines, destroys the newly created SoundEngine");
            Destroy(gameObject);
        }
    }

    public void MoveMusicToNextLevel(int speedLevel)
    {

        speedLevel++;
        switch (speedLevel)
        {
            case 0:
                AkSoundEngine.SetSwitch(groups[0], "silent", musicObject);
                AkSoundEngine.SetSwitch(groups[1], "rhythm1", musicObject);
                AkSoundEngine.SetSwitch(groups[2], "rhythm1", musicObject);
                AkSoundEngine.SetSwitch(groups[3], "silent", musicObject);
                AkSoundEngine.SetSwitch(groups[4], "silent", musicObject);
                AkSoundEngine.SetSwitch(groups[5], "silent", musicObject);
                AkSoundEngine.SetSwitch(groups[6], "silent", musicObject);
                AkSoundEngine.SetSwitch(groups[7], "silent", musicObject);
                AkSoundEngine.SetSwitch(groups[8], "silent", musicObject);
                break;
            case 1:
                AkSoundEngine.SetSwitch(groups[0], "silent", musicObject);
                AkSoundEngine.SetSwitch(groups[1], "rhythm2", musicObject);
                AkSoundEngine.SetSwitch(groups[2], "rhythm1", musicObject);
                AkSoundEngine.SetSwitch(groups[3], "silent", musicObject);
                AkSoundEngine.SetSwitch(groups[4], "silent", musicObject);
                AkSoundEngine.SetSwitch(groups[5], "silent", musicObject);
                AkSoundEngine.SetSwitch(groups[6], "silent", musicObject);
                AkSoundEngine.SetSwitch(groups[7], "silent", musicObject);
                AkSoundEngine.SetSwitch(groups[8], "silent", musicObject);
                break;
            case 2:
                AkSoundEngine.SetSwitch(groups[0], "riff2", musicObject);
                AkSoundEngine.SetSwitch(groups[1], "rhythm2", musicObject);
                AkSoundEngine.SetSwitch(groups[2], "rhythm1", musicObject);
                AkSoundEngine.SetSwitch(groups[3], "beat1", musicObject);
                AkSoundEngine.SetSwitch(groups[4], "silent", musicObject);
                AkSoundEngine.SetSwitch(groups[5], "silent", musicObject);
                AkSoundEngine.SetSwitch(groups[6], "silent", musicObject);
                AkSoundEngine.SetSwitch(groups[7], "silent", musicObject);
                AkSoundEngine.SetSwitch(groups[8], "silent", musicObject);
                break;
            case 3:
                AkSoundEngine.SetSwitch(groups[0], "riff1", musicObject);
                AkSoundEngine.SetSwitch(groups[1], "rhythm2", musicObject);
                AkSoundEngine.SetSwitch(groups[2], "rhythm1", musicObject);
                AkSoundEngine.SetSwitch(groups[3], "beat1", musicObject);
                AkSoundEngine.SetSwitch(groups[4], "silent", musicObject);
                AkSoundEngine.SetSwitch(groups[5], "riff2", musicObject);
                AkSoundEngine.SetSwitch(groups[6], "silent", musicObject);
                AkSoundEngine.SetSwitch(groups[7], "segment1", musicObject);
                AkSoundEngine.SetSwitch(groups[8], "silent", musicObject);
                break;
            case 4:
                AkSoundEngine.SetSwitch(groups[0], "riff1", musicObject);
                AkSoundEngine.SetSwitch(groups[1], "rhythm2", musicObject);
                AkSoundEngine.SetSwitch(groups[2], "rhythm1", musicObject);
                AkSoundEngine.SetSwitch(groups[3], "beat2", musicObject);
                AkSoundEngine.SetSwitch(groups[4], "riff2", musicObject);
                AkSoundEngine.SetSwitch(groups[5], "silent", musicObject);
                AkSoundEngine.SetSwitch(groups[6], "silent", musicObject);
                AkSoundEngine.SetSwitch(groups[7], "segment1", musicObject);
                AkSoundEngine.SetSwitch(groups[8], "segment1", musicObject);
                break;
            case 5:
                AkSoundEngine.SetSwitch(groups[0], "riff1", musicObject);
                AkSoundEngine.SetSwitch(groups[1], "rhythm2", musicObject);
                AkSoundEngine.SetSwitch(groups[2], "rhythm1", musicObject);
                AkSoundEngine.SetSwitch(groups[3], "beat2", musicObject);
                AkSoundEngine.SetSwitch(groups[4], "silent", musicObject);
                AkSoundEngine.SetSwitch(groups[5], "riff2", musicObject);
                AkSoundEngine.SetSwitch(groups[6], "segment1", musicObject);
                AkSoundEngine.SetSwitch(groups[7], "silence", musicObject);
                AkSoundEngine.SetSwitch(groups[8], "segment1", musicObject);
                break;
            case 6:
                AkSoundEngine.SetSwitch(groups[0], "riff3", musicObject);
                AkSoundEngine.SetSwitch(groups[1], "rhythm2", musicObject);
                AkSoundEngine.SetSwitch(groups[2], "rhythm1", musicObject);
                AkSoundEngine.SetSwitch(groups[3], "beat2", musicObject);
                AkSoundEngine.SetSwitch(groups[4], "silent", musicObject);
                AkSoundEngine.SetSwitch(groups[5], "riff2", musicObject);
                AkSoundEngine.SetSwitch(groups[6], "segment1", musicObject);
                AkSoundEngine.SetSwitch(groups[7], "silent", musicObject);
                AkSoundEngine.SetSwitch(groups[8], "silent", musicObject);
                break;
            case 7:
                AkSoundEngine.SetSwitch(groups[0], "riff2", musicObject);
                AkSoundEngine.SetSwitch(groups[1], "rhythm2", musicObject);
                AkSoundEngine.SetSwitch(groups[2], "rhythm1", musicObject);
                AkSoundEngine.SetSwitch(groups[3], "beat2", musicObject);
                AkSoundEngine.SetSwitch(groups[4], "silent", musicObject);
                AkSoundEngine.SetSwitch(groups[5], "riff2", musicObject);
                AkSoundEngine.SetSwitch(groups[6], "segment1", musicObject);
                AkSoundEngine.SetSwitch(groups[7], "segment2", musicObject);
                AkSoundEngine.SetSwitch(groups[8], "silent", musicObject);
                break;
            case 8:
                AkSoundEngine.SetSwitch(groups[0], "riff1", musicObject);
                AkSoundEngine.SetSwitch(groups[1], "rhythm2", musicObject);
                AkSoundEngine.SetSwitch(groups[2], "rhythm1", musicObject);
                AkSoundEngine.SetSwitch(groups[3], "beat2", musicObject);
                AkSoundEngine.SetSwitch(groups[4], "riff2", musicObject);
                AkSoundEngine.SetSwitch(groups[5], "riff2", musicObject);
                AkSoundEngine.SetSwitch(groups[6], "segment1", musicObject);
                AkSoundEngine.SetSwitch(groups[7], "segment1", musicObject);
                AkSoundEngine.SetSwitch(groups[8], "segment2", musicObject);
                break;
            case 9:
                AkSoundEngine.SetSwitch(groups[0], "riff1", musicObject);
                AkSoundEngine.SetSwitch(groups[1], "rhythm2", musicObject);
                AkSoundEngine.SetSwitch(groups[2], "rhythm1", musicObject);
                AkSoundEngine.SetSwitch(groups[3], "beat3", musicObject);
                AkSoundEngine.SetSwitch(groups[4], "riff2", musicObject);
                AkSoundEngine.SetSwitch(groups[5], "riff2", musicObject);
                AkSoundEngine.SetSwitch(groups[6], "segment1", musicObject);
                AkSoundEngine.SetSwitch(groups[7], "segment1", musicObject);
                AkSoundEngine.SetSwitch(groups[8], "segment2", musicObject);
                break;
            case 10:
                AkSoundEngine.SetSwitch(groups[0], "riff1", musicObject);
                AkSoundEngine.SetSwitch(groups[1], "rhythm2", musicObject);
                AkSoundEngine.SetSwitch(groups[2], "rhythm1", musicObject);
                AkSoundEngine.SetSwitch(groups[3], "beat3", musicObject);
                AkSoundEngine.SetSwitch(groups[4], "lead", musicObject);
                AkSoundEngine.SetSwitch(groups[5], "riff2", musicObject);
                AkSoundEngine.SetSwitch(groups[6], "segment1", musicObject);
                AkSoundEngine.SetSwitch(groups[7], "segment1", musicObject);
                AkSoundEngine.SetSwitch(groups[8], "segment1", musicObject);
                break;
            default:
                AkSoundEngine.SetSwitch(groups[0], "riff1", musicObject);
                AkSoundEngine.SetSwitch(groups[1], "rhythm2", musicObject);
                AkSoundEngine.SetSwitch(groups[2], "rhythm1", musicObject);
                AkSoundEngine.SetSwitch(groups[3], "beat3", musicObject);
                AkSoundEngine.SetSwitch(groups[4], "lead", musicObject);
                AkSoundEngine.SetSwitch(groups[5], "riff2", musicObject);
                AkSoundEngine.SetSwitch(groups[6], "segment1", musicObject);
                AkSoundEngine.SetSwitch(groups[7], "segment1", musicObject);
                AkSoundEngine.SetSwitch(groups[8], "segment1", musicObject);
                break;
        }

    }

    private void SilenceAll()
    {
        foreach(var item in groups)
        {
            AkSoundEngine.SetSwitch(item, "silent", musicObject);
        }
    }

    public void Pause()
    {
    }

    public void Unpuase()
    {
    }

    // Use this for initialization
    void Start () {
        musicObject = GameObject.Find("MusicEmitter");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
