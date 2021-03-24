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


    public static bool win;

    public static bool hit;

    public static bool title;


    private Scene scene;

    private static GameManager _i;

    public bool playMusic;

    private bool coroutineAllowed;

    private ObjectMouseDrag[] planetsList;

    private ObjectMouseDrag mouseDragScript;



    public static GameManager i
    {
        get
        {
            if (_i == null) _i = Instantiate(Resources.Load<GameManager>("GameManager"));
            return _i;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();



        win = false;
        hit = false;


        planetsList = FindObjectsOfType<ObjectMouseDrag>();
        

        Debug.Log("planetList count of objects of type ObjectMouseDrag " + planetsList.Count().ToString());

        if (scene.name == "TitleScene")
        {
            winText.SetActive(false);
            titleText.SetActive(true);
        }
        if (scene.name == "SampleScene" || scene.name == "ScenePlay02")
        {
            //titleText.SetActive(false);
            nextButton.SetActive(false);
        }

        coroutineAllowed = true;
    }


    // Update is called once per frame
    void Update()
    {

        if (scene.name == "ScenePlanetNames")
        {
            bool allLocked = planetsList.All(n => n.locked);

            if (allLocked)
            {
                Debug.Log("All Locked!");
                

            }
        }


        if (scene.name == "ScenePlay02")
        {
            if (BreadMouseDrag.locked && CandyMouseDrag.locked && CoconutMouseDrag.locked)
            {

                winText.SetActive(true);

                AudioManager.PlaySound(AudioManager.Sound.playWinSound);

                BreadMouseDrag.locked = false;
                CandyMouseDrag.locked = false;
                CoconutMouseDrag.locked = false;

                if (coroutineAllowed)
                { StartCoroutine(ShowReplayButtonCoroutine()); }
            }
        }
    }


    IEnumerator ShowNextgButtonCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        nextButton.SetActive(true);

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
        SceneManager.LoadScene("SampleScene");

    }



    public void NextScene()
    {
       // titleText.SetActive(false);
        SceneManager.LoadScene("ScenePlay02");
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
