using System.Collections;
using System.Numerics;
using JetBrains.Annotations;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    public CinemachineCamera Level1Cam;
    Animator Knight;
    [SerializeField] GameObject displayPanel;
    [SerializeField] GameObject MenuPanel;
    [SerializeField] GameObject ErrorPanel;
    [SerializeField] GameObject EscapePanel;
    [SerializeField] GameObject StatBoostPanel;
    [SerializeField] GameObject CharacterPanel;
    [SerializeField] GameObject EndPanel;
    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject DialoguePanel;
    public bool InPanels = true;
    public int PlayerHealth=200;
    public int character = 1;
    public bool Level1Loaded = false;
    public GameObject LightBandit;
    public GameObject HeavyBandit;
    public GameObject HeroKnight;
    public Transform LightBanditPosition;
    public Transform HeavyBanditPosition;
    public Transform KnightPosition;
    public bool invincible = false;
    public bool dead = false;
    public bool SpawnEnemies = false;
    public bool hasSpawned = false;
    public bool dialoguecomplete = false;
    public int coinBalance=0;
    private IEnumerator Error() {
        ErrorPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        ErrorPanel.SetActive(false);
    }
    public static GameManager Instance{get; private set;}
    public void Level1()
{
    StartCoroutine(LoadLevel1());
}
private IEnumerator LoadLevel1()
{
    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Level 1", LoadSceneMode.Additive);
    
    // Wait until the scene is fully loaded
    while (!asyncLoad.isDone)
    {
        yield return null;
    }
    Debug.Log("Level 1 Loaded");
    // Now set the active scene
    SceneManager.SetActiveScene(SceneManager.GetSceneByName("Level 1"));

    // Unload the previous scene
    SceneManager.UnloadSceneAsync("GameStart");

    // Now safely reference objects
    LightBandit = information.Reference.LightBandit;
    HeavyBandit = information.Reference.HeavyBandit;
    HeroKnight = information.Reference.HeroKnight;
    Level1Cam = information.Reference.cam;
    GetComponent<TargetSwitch>().cam = Level1Cam;
    Knight = HeroKnight.GetComponent<Animator>();
    KnightPosition = HeroKnight.GetComponent<Transform>();
    LightBanditPosition = LightBandit.GetComponent<Transform>();
    HeavyBanditPosition = HeavyBandit.GetComponent<Transform>();
    Level1Loaded = true;
}

    public void GameOver() {
        Debug.Log("Game Over");
        dead = true;
        if(coinBalance>=2000) {
            InPanels = true;
            displayPanel.SetActive(false);
            CharacterPanel.SetActive(true);
        }
        else{
            InPanels = true;
            displayPanel.SetActive(false);
            EndPanel.SetActive(true);
        }
    }
    public void OnApplicationQuit()
    {
        Application.Quit();
    }
    public void Play() {
        MenuPanel.SetActive(false);
        InPanels = false;
        displayPanel.SetActive(true);
        StartCoroutine(LoadLevel());
    }
    public void Revived() {
        dead = false;
        PlayerHealth = 200;
    }
    public void GameStart() {
        SceneManager.LoadScene("GameStart");
    }
    private IEnumerator LoadLevel() {
        yield return new WaitForSeconds(0.1f);
        Level1();
    }
    public void Statboostpanel() {
        InPanels = true;
        Time.timeScale = 0f;
        displayPanel.SetActive(false);
        StatBoostPanel.SetActive(true);
    }
    public void Dialogue() {
        if(dialoguecomplete) {
            return;
        }
        StartCoroutine(DialogueWait());
    }
    IEnumerator DialogueWait() {
        InPanels = true;
        Time.timeScale = 0f;
        // displayPanel.SetActive(false);
        DialoguePanel.SetActive(true);
        dialoguecomplete = true;
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1f;
        DialoguePanel.SetActive(false);
        // displayPanel.SetActive(true);
        Time.timeScale = 1f;
        InPanels = false;
    }
    public void ExitDialogue() {
        InPanels = false;
        Time.timeScale = 1f;
        DialoguePanel.SetActive(false);
        displayPanel.SetActive(true);
    }
    public void ExitStatboostpanel() {
        InPanels = false;
        Time.timeScale = 1f;
        StatBoostPanel.SetActive(false);
        displayPanel.SetActive(true);
    }
    public void AddCoins(int a) {
        coinBalance += a;
    }
    public void Character1() {
        if(coinBalance<2000) {
            StartCoroutine(Error());
            return;
        }
        else {
        coinBalance-=2000;
        Revived();
        if(character==3)
        {
            LightBanditPosition.position = HeroKnight.transform.position;
        }
        else if(character==2)
        {
            LightBanditPosition.position = HeavyBandit.transform.position;
        }
        LightBandit.SetActive(true);
        HeavyBandit.SetActive(false);
        HeroKnight.SetActive(false);
        InPanels = false;
        CharacterPanel.SetActive(false);
        displayPanel.SetActive(true);
        character = 1;
        StartCoroutine(Invinc());
        }
    }
    public void Character2() {
        if(coinBalance<5000) {
            StartCoroutine(Error());
            return;
        }
        else {
        coinBalance-=5000;
        Revived();
        if(character==1) {
            HeavyBanditPosition.position = LightBanditPosition.position;
        }
        else if(character==3) {
            HeavyBanditPosition.position = HeroKnight.transform.position;
        }
        HeavyBandit.SetActive(true);
        LightBandit.SetActive(false);
        HeroKnight.SetActive(false);
        InPanels = false;
        CharacterPanel.SetActive(false);
        displayPanel.SetActive(true);
        character=2;
        StartCoroutine(Invinc());
        }
    }
    public void Character3() {
        if(coinBalance<10000) {
            StartCoroutine(Error());
            return;
        }
        else {
        coinBalance-=10000;
        Revived();
        if(character==1) {
            KnightPosition.position = LightBanditPosition.position;
        }
        else if(character==2) {
            KnightPosition.position = HeavyBanditPosition.position;
        }
        HeroKnight.SetActive(true);
        LightBandit.SetActive(false);
        HeavyBandit.SetActive(false);
        InPanels = false;
        CharacterPanel.SetActive(false);
        displayPanel.SetActive(true);
        character = 3;
        StartCoroutine(Invinc());
        }
    }
    IEnumerator Invinc() {
            invincible = true;
            yield return new WaitForSeconds(3f);
            invincible = false;
        }
    void Awake()
    {
        if (Instance == null) // If no instance exists, assign this as the instance
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep GameManager across scenes
        }
        else
        {
            Destroy(gameObject); // If an instance already exists, destroy this duplicate
        }
        displayPanel.SetActive(false);
        MenuPanel.SetActive(true);
        PlayerHealth = 200;
        dialoguecomplete = false;
    }
    void Start()
    {
        Debug.Log("Game Manager Started");
        GameStart();
        displayPanel.SetActive(false);
        MenuPanel.SetActive(true);
        InPanels = true;
        StatBoostPanel.SetActive(false);
        CharacterPanel.SetActive(false);
        SceneManager.UnloadSceneAsync("Level 1");
        hasSpawned = false;
    }
    void Update()
    {
        if(coinBalance>=20000) {
            InPanels = true;
            displayPanel.SetActive(false);
            WinPanel.SetActive(true);
        }
        else if(Level1Loaded&&coinBalance<20000&&!hasSpawned) {
            SpawnEnemies = true;
            EnemySpawn.Instance.Spawn();
            hasSpawned = true;
        }
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(InPanels) {
                InPanels = false;
                Time.timeScale = 1f;
                displayPanel.SetActive(true);
                EscapePanel.SetActive(false);
            }
            else {
                InPanels = true;
                Time.timeScale = 0f;
                displayPanel.SetActive(false);
                EscapePanel.SetActive(true);
            }
        }
    }
}
