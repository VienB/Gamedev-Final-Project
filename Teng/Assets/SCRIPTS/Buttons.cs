using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    // Button Variables
    public Button playButton;
    public Button optionsButton;
    public Button exitButton;
    public Button pauseButton;
    public Button resumeButton;

    // Int Variables
    public int howToPointer = 2;

    // Bool Variables
    public bool restartMode = false;
    public bool gamePause = false;

    // GameObject Variables
    public GameObject mainMenuMusic;
    public GameObject gameOverMenu;
    public GameObject backgroundMusic;
    public GameObject scoreBoard;
    public GameObject scoreBoardImage;
    public GameObject coinBoard;
    public GameObject player;
    public GameObject enemy;
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject logo;
    public GameObject settings;
    public GameObject cutSceneAudio;

    // AudioSource Variable
    public AudioSource audioSource;

    // Script communication
    private PowerupSpawner powerupSpawner;
    private CoinBoard coinBoardScore;
    private ScoreScript scoreScriptReference;
    private SpawnManager spawnManager;
    private PlayerController playerController;
    private CutScene cutScene;
    private Audios audios;
    // Start is called before the first frame update
    void Start()
    {
        // Getting Component references
        audioSource = GetComponent<AudioSource>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        cutScene = GameObject.Find("CutSceneScriptReceiver").GetComponent<CutScene>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        coinBoardScore = GameObject.Find("CoinBoard").GetComponent<CoinBoard>();
        scoreScriptReference = GameObject.Find("ScoreBoard").GetComponent<ScoreScript>();
        powerupSpawner = GameObject.Find("PowerupSpawner").GetComponent<PowerupSpawner>();
        audios = GameObject.FindGameObjectWithTag("Audios").GetComponent<Audios>();

        // Getting GameObject references
        mainMenuMusic = GameObject.Find("MenuMusic");
        gameOverMenu = GameObject.Find("GAME OVER MENU");
        backgroundMusic = GameObject.Find("BackgroundMusic");
        scoreBoard = GameObject.Find("ScoreBoard");
        coinBoard = GameObject.Find("CoinImage");
        scoreBoardImage = GameObject.Find("ScoreImage");
        player = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy");
        mainMenu = GameObject.Find("MAIN MENU");
        pauseMenu = GameObject.Find("PAUSE MENU");
        logo = GameObject.Find("LOGO");
        settings = GameObject.Find("SETTINGS");
        cutSceneAudio = GameObject.FindGameObjectWithTag("AudioCutScene");

        // Setting the main menu How To Play Pointer
        howToPointer = 4;

        // Setting boolean values
        gamePause = false;
        settings.SetActive(false);
        mainMenu.gameObject.SetActive(true);
        gameOverMenu.gameObject.SetActive(false);
        backgroundMusic.gameObject.SetActive(false);
        scoreBoard.gameObject.SetActive(false);
        scoreBoardImage.gameObject.SetActive(false);
        coinBoard.gameObject.SetActive(false);
        player.gameObject.SetActive(false);
        enemy.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        cutSceneAudio.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Creating the How To Play logic
        if (howToPointer == 4)
        {
            mainMenu.transform.GetChild(5).gameObject.SetActive(false);
            mainMenu.transform.GetChild(6).gameObject.SetActive(false);
            mainMenu.transform.GetChild(7).gameObject.SetActive(false);
            mainMenu.transform.GetChild(8).gameObject.SetActive(false);
            mainMenu.transform.GetChild(9).gameObject.SetActive(false);
            mainMenu.transform.GetChild(10).gameObject.SetActive(false);
        }
        if (howToPointer == 5)
        {
            mainMenu.transform.GetChild(5).gameObject.SetActive(true);
            mainMenu.transform.GetChild(6).gameObject.SetActive(false);
            mainMenu.transform.GetChild(7).gameObject.SetActive(false);
            mainMenu.transform.GetChild(8).gameObject.SetActive(false);
            mainMenu.transform.GetChild(9).gameObject.SetActive(false);
            mainMenu.transform.GetChild(10).gameObject.SetActive(false);
        }
        if (howToPointer == 6)
        {
            mainMenu.transform.GetChild(5).gameObject.SetActive(false);
            mainMenu.transform.GetChild(6).gameObject.SetActive(true);
            mainMenu.transform.GetChild(7).gameObject.SetActive(false);
            mainMenu.transform.GetChild(8).gameObject.SetActive(false);
            mainMenu.transform.GetChild(9).gameObject.SetActive(false);
            mainMenu.transform.GetChild(10).gameObject.SetActive(false);
        }
        if (howToPointer == 7)
        {
            mainMenu.transform.GetChild(5).gameObject.SetActive(false);
            mainMenu.transform.GetChild(6).gameObject.SetActive(false);
            mainMenu.transform.GetChild(7).gameObject.SetActive(true);
            mainMenu.transform.GetChild(8).gameObject.SetActive(false);
            mainMenu.transform.GetChild(9).gameObject.SetActive(false);
            mainMenu.transform.GetChild(10).gameObject.SetActive(false);
        }
        if (howToPointer == 8)
        {
            mainMenu.transform.GetChild(5).gameObject.SetActive(false);
            mainMenu.transform.GetChild(6).gameObject.SetActive(false);
            mainMenu.transform.GetChild(7).gameObject.SetActive(false);
            mainMenu.transform.GetChild(8).gameObject.SetActive(true);
            mainMenu.transform.GetChild(9).gameObject.SetActive(false);
            mainMenu.transform.GetChild(10).gameObject.SetActive(false);
        }
        if (howToPointer == 9)
        {
            mainMenu.transform.GetChild(5).gameObject.SetActive(false);
            mainMenu.transform.GetChild(6).gameObject.SetActive(false);
            mainMenu.transform.GetChild(7).gameObject.SetActive(false);
            mainMenu.transform.GetChild(8).gameObject.SetActive(false);
            mainMenu.transform.GetChild(9).gameObject.SetActive(true);
            mainMenu.transform.GetChild(10).gameObject.SetActive(false);
        }
        if (howToPointer == 10)
        {
            mainMenu.transform.GetChild(5).gameObject.SetActive(false);
            mainMenu.transform.GetChild(6).gameObject.SetActive(false);
            mainMenu.transform.GetChild(7).gameObject.SetActive(false);
            mainMenu.transform.GetChild(8).gameObject.SetActive(false);
            mainMenu.transform.GetChild(9).gameObject.SetActive(false);
            mainMenu.transform.GetChild(10).gameObject.SetActive(true);
        }
        // If the Game is over, we will set game objects to setactive false
        if (playerController.gameOver == true)
        {
            coinBoard.gameObject.SetActive(false);
            scoreBoard.gameObject.SetActive(false);
            scoreBoardImage.gameObject.SetActive(false);
            StartCoroutine(GameIsOver());
        }
    }

    // This is for when the user clicks the Play button
    public void PlayButtonClicker()
    {
       
        audioSource.Play();
        mainMenu.gameObject.SetActive(false);
        logo.gameObject.SetActive(false);
        cutScene.StartCutsceneTransition();
        Debug.Log(playButton.gameObject.name + " was clicked");
        StartCoroutine(InterfaceShow());
        StartCoroutine(CallingEssentialObjects());
        CallingBackgroundMusic();
        playerController.transform.rotation = new Quaternion(playerController.transform.rotation.x, 0, playerController.transform.rotation.z, 0);
    }
    // This is for calling required objects to be in the scene, and activating important processes
    public IEnumerator CallingEssentialObjects()
    {
        yield return new WaitForSeconds(2);
        player.gameObject.SetActive(true);
        enemy.gameObject.SetActive(true);
        spawnManager.GettingComponents();
        powerupSpawner.GettingComponents();
    }
    // This is for calling the User interface needed while playing the game
    public IEnumerator InterfaceShow()
    {
        yield return new WaitForSeconds(4);
        pauseButton.gameObject.SetActive(true);
        scoreBoard.gameObject.SetActive(true);
        scoreBoardImage.gameObject.SetActive(true);
        coinBoard.gameObject.SetActive(true);
    }
    // This is for when the user clicks the pause button
    public void PauseButtonClicker()
    {
        audioSource.Play();
        pauseMenu.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        gamePause = true;
        playerController.playerAnimator.SetFloat("Blend", 0.0f);
    }
    // This is for when the user click the resume button
    public void ResumeButtonClicker()
    {
        audioSource.Play();
        pauseMenu.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        gamePause = false;
        playerController.playerAnimator.SetFloat("Blend", 1.0f);
    }
    // This is for switching music from main menu to gameplay
    public void CallingBackgroundMusic()
    {
        mainMenuMusic.gameObject.SetActive(false);
        backgroundMusic.gameObject.SetActive(true);
    }
    // This is for a certain delay before showing game over screen
    public IEnumerator GameIsOver()
    {
        pauseButton.gameObject.SetActive(false);
        yield return new WaitForSeconds(3.0f);
        gameOverMenu.gameObject.SetActive(true);
    }
    // This is for when the user clicks the restart button
    public void RestartingGame()
    {
        
        audioSource.Play();
        coinBoardScore.ResettingCoin();
        scoreScriptReference.ResettingScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // This is for the how to play interface
    public void HowToPlayClicker()
    {
       
        audioSource.Play();
        howToPointer = 5;
    }
    // This makes the pointer go to the next item
    public void HowToPlayNext()
    {
       
        audioSource.Play();
        howToPointer += 1;
    }
    // This makes the pointer go back to the previous item
    public void HowToPlayBack()
    {
        
        audioSource.Play();
        howToPointer -= 1;
    }
    // This is for exiting the how to play interface
    public void HowToPlayExit()
    {
        
        audioSource.Play();
        howToPointer = 4;
    }
    public void SettingsClicker()
    {
        audioSource.Play();
        settings.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void ExitSettingsClicker()
    {
        audioSource.Play();
        settings.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    
}
