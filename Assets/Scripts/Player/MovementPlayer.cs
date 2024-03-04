using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] public float speed;
    [SerializeField] public float runSpeed;
    [SerializeField] private float jetPackSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] public PlayerNumber PlayerNbr;
    [SerializeField] private Energy energy;
    DetectedBall detectedBall;
    [SerializeField] float coolDownDuration;
    private bool isCooldown = false;
    [SerializeField] float gravityScale;
    [SerializeField]private Camera playerCamera;
    [SerializeField] float normalFOV = 60f;
    [SerializeField] float sprintFOV;
    public enum PlayerNumber {
        One,
        Two
    }

    void Start()
    {
        
        detectedBall = GetComponent<DetectedBall>();
        rb = GetComponent<Rigidbody>();
        runSpeed = speed * 2;
        
    }

    void FixedUpdate()
    {
        if (PlayerNbr == PlayerNumber.One)
        {
            float translation = Input.GetAxis("VerticalPlayerOne") * speed;
            float rotation = Input.GetAxis("HorizontalPlayerOne") * rotationSpeed;
            
            if (Input.GetKey(KeyCode.LeftShift) && energy.getEnergy() >=1 && !isCooldown)
            {
                translation = Input.GetAxis("VerticalPlayerOne") * runSpeed;
                if(playerCamera.fieldOfView < sprintFOV) playerCamera.fieldOfView++;
                energy.decrementEnergy();
                regenrateEnergy();
            }
            else if (Input.GetKey(KeyCode.LeftControl) && energy.getEnergy() >=  1 && !isCooldown)
            {
                jetPack();
                regenrateEnergy();
            }
            else
            {
                if(playerCamera.fieldOfView > normalFOV) playerCamera.fieldOfView--; 
                energy.incrementEnergy();
            }
            rb.AddForce(transform.forward * translation);
            rb.MoveRotation(transform.rotation * Quaternion.Euler(0, 0.1f * rotation, 0));
            if (!isGrounded())
            {
                rb.AddForce(Physics.gravity * (gravityScale - 1), ForceMode.Acceleration);
            }

        }
        else if (PlayerNbr == PlayerNumber.Two) 
        {
            float translation = Input.GetAxis("VerticalPlayerTwo") * speed;
            float rotation = Input.GetAxis("HorizontalPlayerTwo") * rotationSpeed;
            if (Input.GetKey(KeyCode.RightShift)&& energy.getEnergy() >= 1 && !isCooldown)
            {
                translation = Input.GetAxis("VerticalPlayerTwo") * runSpeed;
                if (playerCamera.fieldOfView < sprintFOV) playerCamera.fieldOfView++;

                energy.decrementEnergy();
                regenrateEnergy();

            }
            else if (Input.GetKey(KeyCode.RightControl) && energy.getEnergy() >= 1 && !isCooldown)
            {
                jetPack();
                regenrateEnergy();
            }
            else 
            {
                if (playerCamera.fieldOfView > normalFOV) playerCamera.fieldOfView--;

                energy.incrementEnergy();
            }
            rb.AddForce(transform.forward * translation);
            rb.MoveRotation(transform.rotation * Quaternion.Euler(0, 0.1f * rotation, 0));
            if (!isGrounded())
            {
                rb.AddForce(Physics.gravity * (gravityScale - 1), ForceMode.Acceleration);
            }
        }
        interactionBall();
    }

    IEnumerator CoolDown()
    {
        
        isCooldown = true;
        float timer = 0;
        while (timer < coolDownDuration)
        {
            timer += Time.deltaTime;
            yield return null;
            energy.incrementEnergy();

        }
        yield return new WaitForSeconds(coolDownDuration);
        isCooldown = false;
    }

    bool isGrounded()
    {
        float raycastDistance = 0.2f;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance))
        {
            return true;
        }
        return false;
    }
    void interactionBall()
    {
        if (detectedBall.isDetected)
        {
            if(PlayerNbr == PlayerNumber.One)
            {
                if(Input.GetKey(KeyCode.R))
                {
                    detectedBall.Amplify();
                    energy.setEnergy(0);
                    StartCoroutine(CoolDown());
                }
            }
            if(PlayerNbr == PlayerNumber.Two)
            {
                if(Input.GetKey(KeyCode.Keypad1))
                {
                    detectedBall.Amplify();
                    energy.setEnergy(0);
                    StartCoroutine(CoolDown());
                }
            }

            
        }
    }

    void jetPack()
    {
        rb.AddForce(Vector3.up * jetPackSpeed, ForceMode.Impulse);
        energy.decrementEnergy();
    }
    void regenrateEnergy()
    {
        if (energy.getEnergy() <= 1)
        {
            playerCamera.fieldOfView = normalFOV;

            StartCoroutine(CoolDown());
        }
        
    }
}
