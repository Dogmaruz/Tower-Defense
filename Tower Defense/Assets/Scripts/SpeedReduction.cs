using SpaceShooter;
using UnityEngine;

public class SpeedReduction : MonoBehaviour
{
    private SpaceShip m_enemy;

    [SerializeField] private float m_speedRatio = 0.5f;

    public void ReduseSpeed(Destructible destructible)
    {
        m_enemy = destructible.transform.root.GetComponent<SpaceShip>();

        m_enemy.SetSpeed(m_speedRatio);
    }

}
