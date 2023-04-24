using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class UpgradeShop : MonoBehaviour
    {
        [SerializeField] private int m_money;

        [SerializeField] private Text m_textMoney;

        [SerializeField] private UpgradeAsset m_TowerUpgrades;

        [SerializeField] private List<BuyUpgrade> m_sales;

        public List<BuyUpgrade> Sales { get => m_sales; }

        private void Start()
        {
            foreach (var slot in m_sales)
            {
                slot.Initialize();

                slot.transform.Find("Button").GetComponent<Button>().onClick.AddListener(UpdateRestrictions);
            }

            UpdateRestrictions();
        }


        public void UpdateRestrictions()
        {
            foreach (var sales in m_sales)
            {
                if(sales.CheckLevel(m_TowerUpgrades))
                {
                    m_money = MapCompletion.Instance.TotalScore;

                    m_money -= Upgrades.GetTotalCost();

                    m_textMoney.text = m_money.ToString();

                    foreach (var slot in m_sales)
                    {
                        slot.CheckCost(m_money);
                    }
                }
            }

        }

        private void OnValidate()
        {
           
            var buyUpgrades = GetComponentsInChildren<BuyUpgrade>();

            if (m_sales.Count < buyUpgrades.Length || m_sales.Count > buyUpgrades.Length)
            {
                m_sales.Clear();

                for (int i = 0; i < buyUpgrades.Length; i++)
                {
                    m_sales.Add(buyUpgrades[i]);
                }
            }
        }
    }
}