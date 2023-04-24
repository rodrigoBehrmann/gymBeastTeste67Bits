using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int playerMoney;
    public int enemyInBack;
    public int coletableEnemies = 4;
    public GameObject playerGO;

    private int moneyMultiplier = 5;
    private float enemyVerticalOffset = 3f;
    private float anchorMultiplier = 2.8f;

    private bool firstPositionAdd = false;

    [SerializeField] private List<GameObject> backEnemyPos;
    [SerializeField] private List<GameObject> enemiesColected;

    [SerializeField] private GameObject enemyStartPosition;
    [SerializeField] private GameObject enemy;

    [SerializeField] private Renderer playerRenderer;
    [SerializeField] private Material materialRedSkin;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        enemyInBack = 0;
        playerMoney = 50;

        CreateEnemyPositions();
    }

    //controls the creation of enemies
    public void EnemyCollect()
    {

        if (enemyInBack < backEnemyPos.Count)
        {
            if (enemiesColected.Count <= enemyInBack || enemiesColected[enemyInBack] == null)
            {
                GameObject enemyObject = Instantiate(
                    enemy,
                    new Vector3
                    (
                        backEnemyPos[enemyInBack].transform.position.x,
                        backEnemyPos[enemyInBack].transform.position.y + enemyVerticalOffset,
                        backEnemyPos[enemyInBack].transform.position.z),
                        playerGO.transform.rotation,
                        backEnemyPos[enemyInBack].transform
                    );
                Debug.Log(enemyInBack);
                enemyObject.transform.position = backEnemyPos[enemyInBack].transform.position;
                enemiesColected.Insert(enemyInBack, enemyObject);

            }
            enemyInBack++;
        }
    }

    //creates positions where enemies are on the character
    void CreateEnemyPositions()
    {
        if (!firstPositionAdd)
        {
            backEnemyPos.Add(enemyStartPosition);
            firstPositionAdd = true;
        }

        for (int i = 1; i < coletableEnemies; i++)
        {
            if (backEnemyPos.Count > i && backEnemyPos[i] != null)
            {
                continue;
            }

            GameObject positionObject = Instantiate(enemyStartPosition, new Vector3(enemyStartPosition.transform.position.x, backEnemyPos[i - 1].transform.position.y + enemyVerticalOffset, enemyStartPosition.transform.position.z), Quaternion.identity);
            backEnemyPos.Add(positionObject);
            backEnemyPos[i].GetComponent<Rigidbody>().isKinematic = false;
        }

        CreateJoints();
    }

    //creates the new positions after getting the level up 
    void CreateNewEnemyPositions()
    {
        for (int i = backEnemyPos.Count; i < coletableEnemies; i++)
        {
            if (backEnemyPos.Count > i && backEnemyPos[i] != null)
            {
                continue;
            }

            GameObject positionObject = Instantiate(backEnemyPos[backEnemyPos.Count - 1], new Vector3(enemyStartPosition.transform.position.x, backEnemyPos[i - 1].transform.position.y + enemyVerticalOffset, enemyStartPosition.transform.position.z), Quaternion.identity);
            backEnemyPos.Add(positionObject);
            backEnemyPos[i].GetComponent<Rigidbody>().isKinematic = false;
            foreach (Transform child in backEnemyPos[i].transform)
            {
                Destroy(child.gameObject);
            }
        }
        CreateJoints();
    }

    //sets anchor y position and connects to main rigidbody
    void CreateJoints()
    {
        for (int i = 0; i < backEnemyPos.Count; i++)
        {
            ConfigurableJoint joint = backEnemyPos[i].GetComponent<ConfigurableJoint>();

            if (i == 0)
            {
                Destroy(joint);
            }

            if (i >= 1)
            {
                joint.connectedBody = backEnemyPos[0].GetComponent<Rigidbody>(); // connects the joint to the first element
                joint.anchor = new Vector3(0, i * -anchorMultiplier, 0);
            }
        }
    }

    //add money to player
    public void MoneyCollect()
    {
        playerMoney += moneyMultiplier * enemyInBack;

        for (int i = enemiesColected.Count - 1; i >= 0; i--)
        {
            Destroy(enemiesColected[i]);
            enemiesColected.RemoveAt(i);
        }
        enemyInBack -= enemyInBack;
    }

    //LEVEL UPS

    //change the player color
    public void ChangeColorLevelUp()
    {
        if (playerMoney >= 50)
        {
            playerMoney -= 50;
            UIManager.instance.DisableSkinBuyButton();
            playerRenderer.material.SetColor("_Color", Color.red);
        }
        else
        {
            Debug.Log("you dont have money");
        }
    }

    //increases the amount of enemies the player can carry
    public void ChangeCapacityLevelUp()
    {
        if (playerMoney >= 50)
        {
            playerMoney -= 50;
            UIManager.instance.DisableCapacityBuyButton();
            coletableEnemies = 7;
            CreateNewEnemyPositions();
        }
        else
        {
            Debug.Log("you dont have money");
        }
    }
}
