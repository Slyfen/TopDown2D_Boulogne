using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    int gold;
    [SerializeField] TextMeshProUGUI goldText;


    public void AddGold(int amount)
    {
        gold += amount;
        goldText.text = gold.ToString();
    }
}
