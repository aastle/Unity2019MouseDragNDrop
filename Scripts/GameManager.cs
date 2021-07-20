using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public GameObject winText;

    [SerializeField]
    public GameObject titleText;

    [SerializeField]
    public GameObject nextButton;

    [SerializeField]
    public GameObject replayButton;

    [SerializeField]
    public AudioClip winClip;

    private AudioSource soundSource;

    public static bool win;

    public static bool hit;

    public static bool title;


    private Scene scene;


    public bool playMusic;

    private bool coroutineAllowed;

    private static ObjectMouseDrag[] planetsList;

    private ObjectMouseDrag mouseDragScript;

    private static List<string> planetNamesList;


    private static GameManager _i;

    public static GameManager i
    {
        get
        {
            return _i;
        }
    }

    private void Awake()
    {
        if (_i != null && _i != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _i = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();

        soundSource = gameObject.AddComponent<AudioSource>();


        win = false;
        hit = false;


        planetsList = FindObjectsOfType<ObjectMouseDrag>();

        planetNamesList = new List<string>();
        

        Debug.Log("planetList count of objects of type ObjectMouseDrag " + planetsList.Count().ToString());

        if (scene.name == "TitleScene")
        {
            winText.SetActive(false);
            // titleText.SetActive(true);
        }
        if (scene.name == "SampleScene" || scene.name == "ScenePlanetNames")
        {
            // titleText.SetActive(false);
           // nextButton.SetActive(false);
            i.winText.SetActive(false);
        }

        coroutineAllowed = true;
    }


    // Update is called once per frame
    void Update()
    {
       

    }


    public static void ManagerListener(string tag)
    {

        i.winText.SetActive(false);

        Debug.Log("gameObjecct " + tag + ", sent GameManager.ManagerListener an event");


        planetNamesList.Add(tag);
        planetNamesList.ForEach(n=> Debug.Log("Hi from " + n.ToString()));

        

        if (planetsList.All(n => n.locked))
        {
            Debug.Log("All Locked!");
            i.winText.SetActive(true);

            i.StartCoroutine("WaitForAudioCoroutine");

        }
    }

    IEnumerator WaitForAudioCoroutine()
    {
        yield return new WaitForSeconds(2);

        i.soundSource.PlayOneShot(i.winClip, 1f);
    }

    IEnumerator ShowReplayButtonCoroutine()
    {
        coroutineAllowed = false;

        yield return new WaitForSeconds(5);

        replayButton.SetActive(true);

        coroutineAllowed = true;

    }


    public void MuteMusic()
    {
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        AudioSource[] audioSourcesList = mainCamera.GetComponents<AudioSource>();

        GameObject toggle = GameObject.FindGameObjectWithTag("mute");
        Toggle toggleer = toggle.GetComponent<Toggle>();
        bool isOn = toggleer.isOn;

        if (!isOn)
        {
            foreach (AudioSource source in audioSourcesList)
            {
                source.Stop();
            }
        }

        if (isOn)
        {
            foreach (AudioSource source in audioSourcesList)
            {
                source.Play();
            }
        }
    }



    public void StartGame()
    {
        titleText.SetActive(false);
        SceneManager.LoadScene("ScenePlanetNames");

    }



   


    public void Replay()
    {

        //titleText.SetActive(false);
        SceneManager.LoadScene("SampleScene");
    }


    public SoundAudioClip[] soundAudioClipArray;

    [System.Serializable]
    public class SoundAudioClip
    {
        public AudioManager.Sound sound;
        public AudioClip audioClip;
    }


    public MyAlerts ShowableAlerts;

    [System.Serializable]
    public class MyAlerts
    {
        public GameObject myWinText;

    }

}
