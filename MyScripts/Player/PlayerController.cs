using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Menus;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using GameManager;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Modified script from - BRACKEYS 2D PLAYER CONTROLLER SCRIPT
    //[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

    private Interactables interactable;
    private GameObject interactingItem;
    [HideInInspector] public Animator anim;

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    private bool m_canInteract;
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
                                        //    private Vector3 m_Velocity = Vector3.zero;


    [SerializeField] GameObject m_canInteractNotification;

    public Vector2 m_spawnPoint;
    public static Vector2 s_RecordedPlayerPos = new Vector2(-31.4f, -2.48f);

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }


    void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        if (SceneManager.GetActiveScene().name == "MainMap_Day" || SceneManager.GetActiveScene().name == "MainMap_Night")
        {
            gameObject.transform.position = s_RecordedPlayerPos;
        }

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        m_canInteractNotification.SetActive(false);

        GameManager_Data.Instance.UpdatePlayerLevel(ref anim);
    }




    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            OpenPeriod openPeriod = other.GetComponent<Interaction>().interactableItem.openPeriod;

            if (GameManager_Data.Instance.IsDayTime)
            {
                if (openPeriod == OpenPeriod.Day || openPeriod == OpenPeriod.AlwaysOpen)
                {
                    m_canInteractNotification.SetActive(true);
                    m_canInteractNotification.GetComponentInChildren<Text>().text = GameManager_Data.Instance.keys["InteractKey"].ToString();
                    m_canInteract = true;
                    interactingItem = other.gameObject;
                }
            }
            else
            {
                if (openPeriod == OpenPeriod.Night || openPeriod == OpenPeriod.AlwaysOpen)
                {
                    m_canInteractNotification.SetActive(true);
                    m_canInteractNotification.GetComponentInChildren<Text>().text = GameManager_Data.Instance.keys["InteractKey"].ToString();
                    m_canInteract = true;
                    interactingItem = other.gameObject;
                }
            }

            Debug.Log("Can Interact with" + other.GetComponent<Interaction>().interactableItem.name);
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (m_canInteract)
        {
            m_canInteractNotification.SetActive(false);
            m_canInteract = !m_canInteract;
            interactingItem = null;
        }
    }


    private void FixedUpdate()
    {

        s_RecordedPlayerPos.x = gameObject.transform.position.x;
        s_RecordedPlayerPos.y = gameObject.transform.position.y;

        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }

    public void Interact()
    {
        if (m_canInteract && GameManager_Menu.Instance.gameState == GameState.Running)
        {
            Interaction interaction = null;
            if (interactingItem.GetComponent<Interaction>() != null)
            {
                interaction = interactingItem.GetComponent<Interaction>();
            }
            switch (interaction.interactableItem.type)
            {
                case "BuskingDay":
                    interaction.interactableItem.LoadBusking();
                    break;
                case "BuskingDay2":
                    interaction.interactableItem.LoadBusking2();
                    break;
                case "VenueNight":
                    interaction.interactableItem.LoadLobby();
                    break;
                case "BrokeBusking":
                    interaction.interactableItem.LoadNight();
                    GameManager_Data.Instance.groupyData.PhraseOneNum += 500; // todo delete later
                    Menu_MainMap.Instance.UpdatePropertyInfo();
                    break;
                case "NPC":
                    interaction.interactableItem.SpeakToNPC(interactingItem);
                    break;
                case "MusicShop":
                    interaction.interactableItem.LoadShop1();
                    break;
                case "MusicShop2":
                    interaction.interactableItem.LoadShop2();
                    break;
                case "LMShop":
                    interaction.interactableItem.LoadLMShop();
                    break;
                case "VM":
                    interaction.interactableItem.LoadDrink();
                    break;
                case "Train":
                    interaction.interactableItem.LoadFestival();
                    break;
                case "TrainToMainMap":
                    print("loadmainmap_night");
                    LevelLoader.BackToMainMapFromMusicFestival(GameManager_Menu.Instance.LoadingScreen);
                    break;
                default:
                    break;
            }
        }
    }


    public void Move(float move)
    {
        if (move == 0)
        {
            anim.SetBool("Walking", false);
            anim.SetBool("WalkingLeft", false);
        }
        else
        {
            if (move > 0)
            {
                anim.SetBool("Walking", true);
                anim.SetBool("WalkingLeft", false);
            }
            if (move < 0)
            {
                anim.SetBool("WalkingLeft", true);
                anim.SetBool("Walking", false);
            }
        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            //m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            m_Rigidbody2D.velocity = targetVelocity;
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
