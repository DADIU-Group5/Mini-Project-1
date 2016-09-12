using UnityEngine;
using System.Collections;


/// <summary>
/// Used to update the UI. Is a singleton, access using UIController._instance.METHOD
/// </summary>
public class SoundEngine : MonoBehaviour {

    public static SoundEngine _instance;

    private AudioSource source;

    private string clip;

    [Range(0, 100)]
    public float soundEffectsVolume = 50.0f;

    [Range(0, 100)]
    public float musicVolume = 50.0f;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;

            source = GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogError("There should not be 2 SoundEngines, destroys the newly created SoundEngine");
            Destroy(gameObject);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        
    }

    public void Unpuase()
    {
        
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
