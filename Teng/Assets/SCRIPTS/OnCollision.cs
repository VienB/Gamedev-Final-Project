using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OnCollision : MonoBehaviour
{
    //Script Communication
    private PlayerController characterController;
    private Audios audios;

    //Capsule Collider Variable
    private CapsuleCollider playerCapsuleCollider;

    // Getting the object with ParticleSystem
    private GameObject sharkParticle;
    private GameObject pigParticle;
    private GameObject kiteParticle;
    private GameObject deathParticle;
    private GameObject powerUpCollider;
    private GameObject coinDetector;
    private GameObject timerUI;

    // Bool variables
    public bool sharkPowerUp = false;
    public bool kitePowerUp = false;
    public bool pigPowerUp = false;

    public TextMeshProUGUI timer;
    public float timerLimit = 8.0f;

    private void Start()
    {
        // Getting the CapsulCollider reference
        playerCapsuleCollider = GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<CapsuleCollider>();
        // Getting Gameobject references
        sharkParticle = GameObject.FindGameObjectWithTag("SharkParticle");
        pigParticle = GameObject.FindGameObjectWithTag("PigParticle");
        kiteParticle = GameObject.FindGameObjectWithTag("KiteParticle");
        deathParticle = GameObject.FindGameObjectWithTag("DeathParticle");
        coinDetector = GameObject.FindGameObjectWithTag("CoinDetector");
        powerUpCollider = GameObject.FindGameObjectWithTag("PowerUpCollider");
        timerUI = GameObject.FindGameObjectWithTag("Timer");
        // Setting necessary objects to be disabled at the start
        sharkParticle.SetActive(false);
        pigParticle.SetActive(false);
        kiteParticle.SetActive(false);
        deathParticle.SetActive(false);
        coinDetector.SetActive(false);
        powerUpCollider.SetActive(false);
        timerUI.SetActive(false);

        audios = GameObject.FindGameObjectWithTag("Audios").GetComponent<Audios>();

        
    }
    void Update()
    {
        // Getting the PlayerController Script reference
        characterController = GameObject.Find("Player").GetComponent<PlayerController>();
        //If game is over play death particle
        if(characterController.gameOver == true && deathParticle.gameObject != null)
        {
            deathParticle.SetActive(true);
        }
        while (kitePowerUp == true)
        {

            timerLimit -= 1.0f * Time.deltaTime;
            timer.text = timerLimit.ToString("0");
            if (timerLimit < 0)
            {
                timerLimit = 8.0f;
                timerUI.SetActive(false);
            }
            break;
        }

        while (sharkPowerUp == true)
        {

            timerLimit -= 1.0f * Time.deltaTime;
            timer.text = timerLimit.ToString("0");
            if (timerLimit < 0)
            {
                timerLimit = 8.0f;
                timerUI.SetActive(false);
            }
            break;
        }

        while (pigPowerUp == true)
        {

            timerLimit -= 1.0f * Time.deltaTime;
            timer.text = timerLimit.ToString("0");
            if (timerLimit < 0)
            {
                timerLimit = 8.0f;
                timerUI.SetActive(false);
            }
            break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the player collides call the CharacterColliding() method
        if (collision.transform.tag == "Player")
            return;
        characterController.CharacterColliding(collision.collider);
    }

    // Powerup Triggers
    public void OnTriggerEnter(Collider other)
    {
        // If player collides with Kite Powerup
        if (other.CompareTag("Kite"))
        {
            if (kiteParticle != null)
            {
                kiteParticle.SetActive(true);
                StartCoroutine(PowerUpDuration());
            }
            kitePowerUp = true;
            timerUI.SetActive(true);
            Destroy(other.transform.GetChild(0).gameObject);
            characterController.forwardSpeed += 5.0f;
            characterController.jumpForce = 15.0f;
            StartCoroutine(KiteDuration());
        }
        // If player collides with Shark Powerup
        else if (other.CompareTag("Shark"))
        {
            if(sharkParticle != null)
            {
                audios.SharkSound();
                sharkParticle.SetActive(true);
                StartCoroutine(PowerUpDuration());
            }
            sharkPowerUp = true;
            timerUI.SetActive(true);
            playerCapsuleCollider.isTrigger = true;
            Destroy(other.transform.GetChild(0).gameObject);
            characterController.forwardSpeed += 10.0f;
            StartCoroutine(SharkDuration());
        }
        else if (other.CompareTag("Pig"))
        {
            // If player collides with Pig Powerup
            if (pigParticle != null)
            {
                audios.PigSound();
                pigParticle.SetActive(true);
                StartCoroutine(PowerUpDuration());
            }
            pigPowerUp = true;
            timerUI.SetActive(true);
            coinDetector.SetActive(true);
            StartCoroutine(PigDuration());
            Destroy(other.transform.GetChild(0).gameObject);
            Debug.Log("Pig");
        }
    }

    // Kite Powerup duration
    public IEnumerator KiteDuration()
    {
        yield return new WaitForSeconds(8);
        characterController.jumpForce = 10.0f;
        characterController.forwardSpeed -= 5.0f;
        kitePowerUp = false;
        kiteParticle.SetActive(false);
    }
    // Shark Powerup duration
    public IEnumerator SharkDuration()
    {
        yield return new WaitForSeconds(8);
        characterController.forwardSpeed -= 10.0f;
        sharkParticle.SetActive(false);
        playerCapsuleCollider.isTrigger = false;
        sharkPowerUp = false;
    }
    // Pig Powerup duration
    public IEnumerator PigDuration()
    {
        yield return new WaitForSeconds(8);
        pigParticle.SetActive(false);
        coinDetector.SetActive(false);
        pigPowerUp = false;
    }
    // When player picken up power up
    public IEnumerator PowerUpDuration()
    {
        powerUpCollider.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        powerUpCollider.SetActive(false);
    }
}