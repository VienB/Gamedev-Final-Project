using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Essential items for Lane movement, and collision reaction
[System.Serializable]
public enum LANE { Left = -3, Mid = 0, Right = 3 }
public enum HitX { Left, Mid, Right, None }
public enum HitY { Up, Mid, Down, Low, None }
public enum HitZ { Forward, Mid, Backward, None }
public class PlayerController : MonoBehaviour
{
    // Script communication
    public OnCollision onCollision;
    private Buttons buttons;
    private NPCController npcController;
    private Audios audios;

    public LANE mLane = LANE.Mid;
    private LANE lastLane;

    // For Coin sound
    public static PlayerController instance;

    // CharacterController and Animator
    private CharacterController charControl;
    public Animator playerAnimator;
    private Animator npcAnimator;

    // For Coin sound
    public AudioSource coinSoundSource;
    public AudioClip coinSound;


    // Bool Variables
    [HideInInspector]
    public bool goRight, goLeft, goUp, goDown;
    public bool gameOver;
    public bool inJump;
    public bool inRoll;

    // Float Variables
    public float npcYPos = 1.5f;
    private float npcZPos = -1.2f;
    public float SpeedDodge = 10f;
    public float jumpForce = 7f;
    public float forwardSpeed = 7.0f;
    private float y;
    private float x;
    private float ColliderHeight;
    private float ColliderCenterY;

    // Collision Variables
    public HitX hit_x = HitX.None;
    public HitY hit_y = HitY.None;
    public HitZ hit_z = HitZ.None;

    // Particle system game object
    private GameObject hitParticle;

    // For coin sound
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Starting the game
        StartCoroutine(StartGame());

        // Finding component references
        audios = GameObject.FindGameObjectWithTag("Audios").GetComponent<Audios>();
        npcAnimator = GameObject.Find("Enemy").GetComponent<Animator>();
        npcController = GameObject.Find("Enemy").GetComponent<NPCController>();
        buttons = GameObject.Find("BUTTONS").GetComponent<Buttons>();
        hitParticle = GameObject.FindGameObjectWithTag("ObstacleColliderParticle");
        charControl = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
        coinSoundSource = GetComponent<AudioSource>();

        // Setting values
        ColliderHeight = charControl.height;
        ColliderCenterY = charControl.center.y;
        transform.position = Vector3.zero;

        // Setting values for SetFloat and SetActive
        playerAnimator.SetFloat("Blend", 1.0f);
        hitParticle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Input by user logic
        if (gameOver == false && buttons.gamePause == false)
        {
            goRight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
            goLeft = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
            goUp = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
            goDown = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);

            if (goRight)
            {
                if (mLane == LANE.Mid)
                {
                    lastLane = mLane;
                    mLane = LANE.Right;
                    playerAnimator.Play("DodgeLeft");
                }
                else if (mLane == LANE.Left)
                {
                    lastLane = mLane;
                    mLane = LANE.Mid;
                    playerAnimator.Play("DodgeLeft");
                }
                else
                {
                    lastLane = mLane;
                    playerAnimator.Play("Stumble_R");
                }
            }
            else if (goLeft)
            {
                if (mLane == LANE.Mid)
                {
                    lastLane = mLane;
                    mLane = LANE.Left;
                    playerAnimator.Play("DodgeRight");
                }
                else if (mLane == LANE.Right)
                {
                    lastLane = mLane;
                    mLane = LANE.Mid;
                    playerAnimator.Play("DodgeRight");
                }
                else
                {
                    lastLane = mLane;
                    playerAnimator.Play("Stumble_L");
                }
            }
        }
        // Movement of player logic
        if (gameOver == false && buttons.gamePause == false)
        {
            x = Mathf.Lerp(x, (int)mLane, Time.deltaTime * SpeedDodge);
            Vector3 moveVector = new Vector3(x - transform.position.x, y * Time.deltaTime, forwardSpeed * Time.deltaTime);
            charControl.Move(moveVector);
            playerJump();
            playerRoll();
        }
    }
    // Delay for setting the game over to false
    public IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1.75f);
        gameOver = false;
    }
    // Setting the game over to true
    public void GameOver()
    {
        gameOver = true;
    }
    // Jump control
    public void playerJump()
    {

        if (charControl.isGrounded)
        {
            if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Falling"))
            {
                playerAnimator.Play("Landing");
                inJump = false;
            }
            if (goUp)
            {
                y = jumpForce;
                playerAnimator.CrossFadeInFixedTime("Jump", 0.1f);
                inJump = true;
            }
            if(goUp && onCollision.kitePowerUp == true)
            {
                audios.KiteSound();
                y = jumpForce;
                playerAnimator.CrossFadeInFixedTime("Jump", 0.1f);
                inJump = true;
            }
        }
        else
        {
            y -= jumpForce * 2 * Time.deltaTime;
            if (playerAnimator.velocity.y < -0.1f)
                playerAnimator.Play("Falling");

        }
    }
    internal float RollCounter;
    // Roll control
    public void playerRoll()
    {
        RollCounter -= Time.deltaTime;

        if (RollCounter <= 0f)
        {
            RollCounter = 0f;
            charControl.center = new Vector3(0, ColliderCenterY, 0);
            charControl.height = ColliderHeight;
            inRoll = false;
        }
        if (goDown && inRoll == false)
        {
            y = -10.0f;

            RollCounter = 0.2f;
            charControl.center = new Vector3(0, ColliderCenterY / 2f, 0);
            charControl.height = ColliderHeight / 2f;
            playerAnimator.CrossFadeInFixedTime("Roll", 0.5f);

            inRoll = true;
            inJump = false;
        }
    }
    // Player to Obstacles Collision detector
    public void CharacterColliding(Collider col)
    {
        hit_x = GetHitX(col);
        hit_y = GetHitY(col);
        hit_z = GetHitZ(col);
        if (gameOver == false)
        {
            if (hit_z == HitZ.Forward && hit_x == HitX.Mid)
            {
                if (npcController.offset.z < npcController.nearPlayer.z)
                {
                    if (hit_y == HitY.Low)
                    {
                        playerAnimator.Play("Stumle_M");
                        audios.BumpSound();
                        StartCoroutine(HitColliderParticle());
                        ResettingCollision();
                        npcAnimator.Play("Rolling");
                        npcController.offset = new Vector3(0.0f, npcYPos, npcZPos);
                    }
                }
                else
                {
                    playerAnimator.Play("DeathMid");
                    ResettingCollision();
                    gameOver = true;
                    npcAnimator.Play("Death");
                    audios.DeathSound();
                    npcController.comparisonOffset = new Vector3(0.0f, npcYPos, npcZPos);
                    npcController.offset = new Vector3(0.0f, npcYPos, npcZPos);
                }

                if (hit_y == HitY.Down)
                {
                    playerAnimator.Play("DeathMid");
                    ResettingCollision();
                    gameOver = true;
                    npcAnimator.Play("Death");
                    audios.DeathSound();
                    npcController.comparisonOffset = new Vector3(0.0f, npcYPos, npcZPos);
                    npcController.offset = new Vector3(0.0f, npcYPos, npcZPos);
                }
                else if (hit_y == HitY.Mid)
                {
                    playerAnimator.Play("DeathMid");
                    ResettingCollision();
                    gameOver = true;
                    npcAnimator.Play("Death");
                    audios.DeathSound();
                    npcController.comparisonOffset = new Vector3(0.0f, npcYPos, npcZPos);
                    npcController.offset = new Vector3(0.0f, npcYPos, npcZPos);
                }
                else if (hit_y == HitY.Up)
                {
                    playerAnimator.Play("DeathMid");
                    ResettingCollision();
                    gameOver = true;
                    npcAnimator.Play("Death");
                    audios.DeathSound();
                    npcController.comparisonOffset = new Vector3(0.0f, npcYPos, npcZPos);
                    npcController.offset = new Vector3(0.0f, npcYPos, npcZPos);
                }
            }
            else if (hit_z == HitZ.Mid)
            {
                if (npcController.offset.z < npcController.nearPlayer.z)
                {
                    if (hit_x == HitX.Right)
                    {
                        mLane = lastLane;
                        playerAnimator.Play("Stumble_R_Mid");
                        audios.BumpSound();
                        StartCoroutine(HitColliderParticle());
                        ResettingCollision();
                        npcAnimator.Play("Rolling");
                        npcController.offset = new Vector3(0.0f, npcYPos, npcZPos);
                    }
                    else if (hit_x == HitX.Left)
                    {
                        mLane = lastLane;
                        playerAnimator.Play("Stumble_L_Mid");
                        audios.BumpSound();
                        StartCoroutine(HitColliderParticle());
                        ResettingCollision();
                        npcAnimator.Play("Rolling");
                        npcController.offset = new Vector3(0.0f, npcYPos, npcZPos);
                    }
                }
                else if (npcController.offset.z == npcController.nearPlayer.z)
                {
                    if (hit_x == HitX.Right)
                    {
                        playerAnimator.Play("DeathRight");
                        ResettingCollision();
                        gameOver = true;
                        npcAnimator.Play("Death");
                        audios.DeathSound();
                        npcController.comparisonOffset = new Vector3(0.0f, npcYPos, npcZPos);
                        npcController.offset = new Vector3(0.0f, npcYPos, npcZPos);
                    }
                    else if (hit_x == HitX.Left)
                    {
                        playerAnimator.Play("DeathLeft");
                        ResettingCollision();
                        gameOver = true;
                        npcAnimator.Play("Death");
                        audios.DeathSound();
                        npcController.comparisonOffset = new Vector3(0.0f, npcYPos, npcZPos);
                        npcController.offset = new Vector3(0.0f, npcYPos, npcZPos);
                    }
                }
            }
        }
    }
    // Resetting the value of collision
    private void ResettingCollision()
    {
        hit_x = HitX.None;
        hit_y = HitY.None;
        hit_z = HitZ.None;
    }
    // Getting the HitX
    public HitX GetHitX(Collider col)
    {
        Bounds playerBounds = charControl.bounds;
        Bounds colliderBounds = col.bounds;
        float minimumX = Mathf.Max(colliderBounds.min.x, playerBounds.min.x);
        float maximumX = Mathf.Min(colliderBounds.max.x, playerBounds.max.x);
        float averageOfBoth = (minimumX + maximumX) / 2f - colliderBounds.min.x;
        HitX hit;
        if (averageOfBoth > colliderBounds.size.x - 0.33f)
        {
            hit = HitX.Right;
        }
        else if (averageOfBoth < 0.33f)
        {
            hit = HitX.Left;
        }
        else
        {
            hit = HitX.Mid;
        }
        return hit;
    }
    // Getting the HitY
    public HitY GetHitY(Collider col)
    {
        Bounds playerBounds = charControl.bounds;
        Bounds colliderBounds = col.bounds;
        float minimumY = Mathf.Max(colliderBounds.min.y, playerBounds.min.y);
        float maximumY = Mathf.Min(colliderBounds.max.y, playerBounds.max.y);
        float averageOfBoth = ((minimumY + maximumY) / 2f - playerBounds.min.y) / playerBounds.size.y;
        HitY hit;
        if (averageOfBoth < 0.17f)
            hit = HitY.Low;
        else if (averageOfBoth < 0.33f)
            hit = HitY.Down;
        else if (averageOfBoth < 0.66f)
            hit = HitY.Mid;
        else
            hit = HitY.Up;
        return hit;
    }
    // Getting the HitZ
    public HitZ GetHitZ(Collider col)
    {
        Bounds playerBounds = charControl.bounds;
        Bounds colliderBounds = col.bounds;
        float minimumZ = Mathf.Max(colliderBounds.min.z, playerBounds.min.z);
        float maximumZ = Mathf.Min(colliderBounds.max.z, playerBounds.max.z);
        float averageOfBoth = ((minimumZ + maximumZ) / 2f - playerBounds.min.z) / playerBounds.size.z;
        HitZ hit;
        if (averageOfBoth < 0.33f)
            hit = HitZ.Backward;
        else if (averageOfBoth < 0.66f)
            hit = HitZ.Mid;
        else
            hit = HitZ.Forward;
        return hit;
    }
    // When character collides with an obstacle method will play particles
    public IEnumerator HitColliderParticle()
    {
        hitParticle.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        hitParticle.SetActive(false);
    }
}