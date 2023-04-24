using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private TextMeshProUGUI moneyTxt;
    [SerializeField] private Button capacityBuyButton;
    [SerializeField] private Button skinBuyButton;

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

    void Update()
    {
        moneyTxt.text = GameManager.instance.playerMoney.ToString();
    }

    public void DisableCapacityBuyButton()
    {
        capacityBuyButton.interactable = false;
    }

    public void DisableSkinBuyButton()
    {
        skinBuyButton.interactable = false;
    }
}

