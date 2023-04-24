using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            ShopPanel(true);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            ShopPanel(false);
        }
    }

    private void ShopPanel(bool PlayerOnShop)
    {
        shopPanel.SetActive(PlayerOnShop);
    }
}
