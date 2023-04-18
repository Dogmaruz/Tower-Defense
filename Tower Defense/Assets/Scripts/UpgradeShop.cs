using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class UpgradeShop : MonoBehaviour
    {
        [SerializeField] private int m_money;

        [SerializeField] private Text m_textMoney;

        [SerializeField] private BuyUpgrade[] m_sales;

        private void Start()
        {
            
            foreach (var slot in m_sales)
            {
                slot.Initialise();

                slot.transform.Find("Button").GetComponent<Button>().onClick.AddListener(UpdateMoney);
            }

            UpdateMoney();
        }

        public void UpdateMoney()
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