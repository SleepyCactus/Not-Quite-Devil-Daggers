using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float GameWinTime;
    public float CurrentTime;
    public List<GameObject> ObjectsToDestroy = new List<GameObject>();
    public PlayerController PlayerController;
    public EnemyManager EnemyManager;

    public static GameManager Instance;
    
    [SerializeField] private GameObject m_playerPrefab;
    [SerializeField] private GameObject m_baseCameraPrefab;
    [SerializeField] private GameObject m_menuUI;
    
    
    private GameObject m_baseCameraInstance;
    private StateMachine m_stateMachine = new StateMachine();
    private GameObject m_playerInstance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        m_stateMachine.ChangeState(new BeginMenuState(m_stateMachine));
    }

    void Update()
    {
        m_stateMachine.Update();
    }

    public void PlayGame()
    {
        m_stateMachine.ChangeState(new BeginGameState(m_stateMachine));
    }
    public void EndGame(bool _hasDied)
    {
        EnemyManager.PauseEnemies();
        PlayerController.SpawnPostCam(_hasDied);
        DeletePlayer();
        if (_hasDied)
            m_stateMachine.ChangeState(new LoseGameState(m_stateMachine));


    }    
    public void SetupMenu()
    {
        ToggleMenu(true);
        SpawnBaseCamera();
        ResetCursor();
    }
    public void SetupGame() 
    {
        ClearObjects();
        EnemyManager.ClearEnemies();
        ToggleMenu(false);
        SpawnPlayer();
        DeleteBaseCamera();
        ResetTime();
    }
    private void ClearObjects()
    {
        foreach (GameObject obj in ObjectsToDestroy)
        {
            Destroy(obj);
        }
        ObjectsToDestroy.Clear();
    }
    private void DeleteBaseCamera()
    {
        if(m_baseCameraInstance != null)
            Destroy(m_baseCameraInstance);
        m_baseCameraInstance = null;
    }
    private void SpawnBaseCamera()
    {
        m_baseCameraInstance = Instantiate(m_baseCameraPrefab, Vector3.zero, Quaternion.identity);
    }
    private void DeletePlayer()
    {
        if(m_playerInstance != null)
            Destroy(m_playerInstance);
        m_playerInstance = null;
        PlayerController = null;
    }
    private void SpawnPlayer()
    {
        m_playerInstance = Instantiate(m_playerPrefab, Vector3.zero, Quaternion.identity);
        PlayerController = m_playerInstance.GetComponentInChildren<PlayerController>();
    }
    private void ToggleMenu(bool _toggle)
    {
        m_menuUI.SetActive(_toggle);
    }
    private void ResetTime()
    {
        CurrentTime = 0;
    }
    private void ResetCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
