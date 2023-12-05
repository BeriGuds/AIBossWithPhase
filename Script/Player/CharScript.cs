using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
[RequireComponent(typeof(CharacterController))]
public class CharScript : MonoBehaviour
{
    public Camera playerCamera;
    
    [Header("CharControllerValues")]
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float lookSpeed = 2.0f;
    public float lookYlimit;
    public float lookXlimit;

    [Header("Player Stats")]
    public float playerHealth = 100;
    public int playerDamage = 25;

    [Header("PlayerIK")]
    [SerializeField] private LayerMask aimColliderMask = new LayerMask();
    [SerializeField] private Transform debugTransform;

    [Header("Animator")]

    [Header("Particle Effects")]
    public GameObject longAttackParticle;

    //[Header("Player Items")]
    //public GameObject flashlight;
   
    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [Header("What enemy is being Damaged")]
    public bool doingDamageToDimensionalCrack;
    public bool doingDamageToConstructionBoss;
    public bool doingDamageToEnemy;

    [Header("What Skill is Active")]
    public bool isShieldSkillActive;

    public bool canMove = true;
    private bool canRun;
    private bool isFlashlightActive = false;
    private bool attackOverheat;
    private float attackTime;

    //For blended Anim
    Animator animator;
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    public float acceleration = 2.0f;
    public float Deceleration = 2.0f;
    public float maximumWalkVelocity = 0.5f;
    public float maximumRunVelocity = 2.0f;
    int VelocityZHash;
    int VelocityXHash;

    int WalkingHash;
    int RunningHash;
    int CrouchingHash;
    int CrouchWalkingHash;

    bool CrouchState = false;

    public static Vector3 playerPos;

    //testing for flash skill
    public bool FlashSkill = false;
    public bool playerHide = false; // test for hiding lockers

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        StartCoroutine(TrackPlayer());
        VelocityZHash = Animator.StringToHash("Velocity Z");
        VelocityXHash = Animator.StringToHash("Velocity X");

        WalkingHash = Animator.StringToHash("isWalking");
        RunningHash = Animator.StringToHash("isRunning");
        CrouchingHash = Animator.StringToHash("isCrouching");
        CrouchWalkingHash = Animator.StringToHash("isCrouchWalking");
    }

    void Update()
    {
        bool isLongAttackActive = Input.GetKey(KeyCode.Mouse1);
        bool isSkillActive = Input.GetKeyDown(KeyCode.Q);
        bool runningPressed = Input.GetKey(KeyCode.LeftShift);
        bool walkingPressed = Input.GetKey(KeyCode.W);
        bool crouchingPressed = Input.GetKeyDown(KeyCode.C);
        bool FlashlightPressed = Input.GetKeyUp(KeyCode.F); //testing for flashlight blind
        
        //Movement(walkingPressed, runningPressed, crouchingPressed);

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float curSpeedX = canMove ?  (runningPressed ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ?  (runningPressed ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        //FOR TORSO IK 
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        CanMove();
    }

    void CanMove()
    {
        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookYlimit, lookXlimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
        }
    }

    IEnumerator TrackPlayer()
    {
        while (true) { 
        
            playerPos = gameObject.transform.position;
            yield return null;
        }
    }

    //void BlindSkill(bool flashActivate)
    //{
    //    Vector3 direction = EnemyAI.enemyPos - transform.position;

    //    float angle = Vector3.Angle(direction, transform.forward);

    //    bool isFlashlightOn = false;
    //    if (!isFlashlightOn)
    //    {
    //        isFlashlightOn = flashActivate;
    //    }

    //    if (isFlashlightOn)
    //    {
    //        test for 45angle forward
    //        if (angle < 45f * 0.5f)
    //        {
    //            RaycastHit hit;
    //            Debug.Log("Detecting Angles");
    //            Debug.DrawRay(transform.position, direction.normalized, Color.yellow, 45f);
    //            test 5f distance
    //            if (Physics.Raycast(transform.position, direction.normalized, out hit, 45f))
    //            {
    //                Debug.Log("Initializing Ray");
    //                if (hit.collider.tag == "Enemy")
    //                {
    //                    schoolBoss.BlindState();
    //                    Debug.Log(hit.collider.gameObject);
    //                }
    //            }
    //        }
    //    }
    //}

    //void Movement(bool WalkingPressed, bool RunningPressed, bool CrouchingPressed)
    //{
    //    //walking
    //    if (WalkingPressed && canMove && !RunningPressed)
    //    {
    //        animator.SetBool(WalkingHash, WalkingPressed);
    //    }
    //    else if (RunningPressed && canMove && WalkingPressed)
    //    {
    //        animator.SetBool(RunningHash, RunningPressed);
    //    }
    //    else if (CrouchingPressed && canMove && !WalkingPressed && !RunningPressed)
    //    {
    //        animator.SetBool(CrouchingHash, CrouchingPressed);
    //        if (WalkingPressed && CrouchingPressed == true)
    //        {
    //            walkingSpeed = 4f;
    //            animator.SetBool(CrouchWalkingHash, true);
    //        }
    //    }
    //}

    //void Walking(bool WalkingPressed, bool RunningPressed, bool CrouchPressed)
    //{
    //    if (WalkingPressed && canMove && !RunningPressed && !CrouchPressed)
    //    {
    //        animator.SetBool(WalkingHash, WalkingPressed);
    //    }
    //    else
    //    {
    //        animator.SetBool(WalkingHash, WalkingPressed);
    //    }
    //}

    //void Running(bool RunningPressed, bool WalkingPressed, bool CrouchPressed)
    //{
    //    if (RunningPressed && canMove && WalkingPressed && !CrouchPressed)
    //    {
    //        animator.SetBool(RunningHash, RunningPressed);
    //    }
    //    else
    //    {
    //        animator.SetBool(RunningHash, RunningPressed);
    //    }
    //}

    //void Crouch(bool CrouchingPressed)
    //{
    //    CrouchState = true;
    //    animator.SetBool(CrouchingHash, CrouchingPressed);
    //}

    //void CrouchWalk(bool walkingPressed)
    //{
    //    if (CrouchState)
    //    {
    //        if (walkingPressed)
    //        {
    //            walkingSpeed = 2f;
    //            animator.SetBool(CrouchWalkingHash, true);
    //        }
    //        else
    //        {
    //            animator.SetBool(CrouchWalkingHash, false);
    //        }
    //    }
    //}

}

