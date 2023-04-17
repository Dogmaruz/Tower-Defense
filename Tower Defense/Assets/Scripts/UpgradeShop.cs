using UnityEngine;
using UnityEngine.UI;
using System;

namespace TowerDefense
{
    public class UpgradeShop : MonoBehaviour
    {
        [SerializeField] private int m_money;

        [SerializeField] private Text m_textMoney;

        [SerializeField] private BuyUpgrade[] m_sales;

        private void Start()
        {
            m_money = MapCompletion.Instance.TotalScore;

            m_textMoney.text = m_money.ToString();

            foreach (var slot in m_sales)
            {
                slot.Initialise();
            }
        }
    }
}