using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovementController : MonoBehaviour
{
    #region GameObject Components

    private Rigidbody m_Rigidbody;
    public AudioListener m_audioListener;
    public AudioSource m_audioSource;

    #endregion

    #region Child GameObjects

    [Header("Linked Components")]
    [SerializeField] private Camera m_Camera;

    #endregion

    #region UI Variables and Components

    [Header("UI")]
    [SerializeField] private Image m_DeadImageEffect;
    [SerializeField] private Text m_LifeStatusText;
    [SerializeField] private Text m_KeyAmountText;
    private string m_Alive = "Alive";
    private string m_Dead = "Dead";
    private string m_KeyText = "Keys: ";
    private Color m_AliveColor = Color.green;
    private Color m_DeadColor = Color.red;
    private int m_KeyAmount = 0;

    #endregion

    #region Movement Variables

    [Header("Movement Settings")]
    [SerializeField] private float m_WalkSpeed = 1f;
    [SerializeField] private float m_RotationSpeed = 1f;
    [SerializeField] private float rotAngle = 45f;
    private Vector3 m_Movement;
    private float mouseAxis;
    private float mouseCameraAxis;
    private float camRotation;

    #endregion

    #region Player Status Variables

    private bool m_IsLiving = true;
    public bool IsLiving => m_IsLiving;
    private int m_PlayerLife = 5;
    public int PlayerLife => m_PlayerLife;
    public bool levelClearedStatus = false;
    public int KeyAmount => m_KeyAmount;
    public int nextSceneId = 0;

    #endregion

    #region Audio Clips

    public AudioClip KeyPickupSound;
    public AudioClip HurtSound;
    public AudioClip LifeSound;
    public AudioClip RiftEnterSound;
    public AudioClip doorEnterSound;

    #endregion

    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_LifeStatusText.text = m_Alive;
        m_LifeStatusText.color = m_AliveColor;
        m_KeyAmountText.text = $"{m_KeyText} {m_KeyAmount}";
        LockCursor();
    }

    void Update()
    {
        InputToDirectionVector();
        MouseAxisFromInput();
        CheckLifeState();
        CameraLookRotation();
        SwitchLifeState();

        if (levelClearedStatus)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                LoadNextLevel();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    private void FixedUpdate()
    {
        MovePlayer(m_Movement);
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    #region Movement Methods

    private void InputToDirectionVector()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        m_Movement = new Vector3(h, 0, v);
    }

    private void MouseAxisFromInput()
    {
        mouseAxis = Input.GetAxis("Mouse X");
        mouseCameraAxis = Input.GetAxis("Mouse Y");
    }

    private void CameraLookRotation()
    {
        camRotation -= mouseCameraAxis;
        camRotation = Mathf.Clamp(camRotation, -rotAngle, rotAngle);
        m_Camera.transform.localRotation = Quaternion.Euler(camRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseAxis * m_RotationSpeed);
    }

    private void MovePlayer(Vector3 direction)
    {
        direction = transform.TransformDirection(direction);
        m_Rigidbody.velocity = direction * m_WalkSpeed * Time.fixedDeltaTime;
    }

    #endregion

    #region Pickups and Life Methods

    private void CheckLifeState()
    {
        if(m_PlayerLife <= 0)
        {
            m_IsLiving = false;
        }
    }

    private void SwitchLifeState()
    {
        if (m_IsLiving)
        {
            m_LifeStatusText.text = m_Alive;
            m_LifeStatusText.color = m_AliveColor;
            m_DeadImageEffect.gameObject.SetActive(false);
        }
        else
        {
            m_LifeStatusText.text = m_Dead;
            m_LifeStatusText.color = m_DeadColor;
            m_DeadImageEffect.gameObject.SetActive(true);
        }
    }

    public void AddKey()
    {
        // Play key pickup sound
        m_audioSource.PlayOneShot(KeyPickupSound);
        m_KeyAmount = m_KeyAmount + 1;
        m_KeyAmountText.text = $"{m_KeyText} {m_KeyAmount}";
    }

    public void AddLife(int amount)
    {
        // Play add life sound
        m_audioSource.PlayOneShot(LifeSound);
        m_PlayerLife += amount;
    }

    public void RemoveLife(int amount)
    {
        // Play hurt sound
        m_audioSource.PlayOneShot(HurtSound);
        m_PlayerLife -= amount;
    }

    public void SetToLiving()
    {
        // Play living sound
        m_audioSource.PlayOneShot(RiftEnterSound);
        m_IsLiving = true;
    }

    #endregion

    #region Level Management Methods

    private void LoadNextLevel()
    {
        m_audioSource.PlayOneShot(doorEnterSound);
        SceneManager.LoadScene(nextSceneId);
    }


    private void RestartLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevel);
    }

    #endregion
}