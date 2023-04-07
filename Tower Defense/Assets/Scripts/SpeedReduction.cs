using SpaceShooter;
using TowerDefense;
using UnityEngine;

public class SpeedReduction : MonoBehaviour
{
    [SerializeField] private float m_speedRatio = 0.5f;

    public void ReduseSpeed(Destructible destructible)
    {
        destructible.GetComponent<TD_PatrolController>().SetNavigationLinear(m_speedRatio);
    }
}
