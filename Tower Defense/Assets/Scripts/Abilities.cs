using SpaceShooter;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{

    public class Abilities : SingletonBase<Abilities>
    {
        public interface IUsable
        {
            void Use();

        }

        [Serializable]
        public class FireAbility : IUsable
        {
            [SerializeField] private float m_Cost = 5;

            [SerializeField] private int m_Damage = 2;
            public void Use()
            {

            }
        }

        [Serializable]
        public class TimeAbility : IUsable
        {
            [SerializeField] private int m_Cost = 10;

            [SerializeField] private float m_CoolDown = 15;

            [SerializeField] private float m_Duration = 5f;

            private float m_speedSlowRatio = 0.5f;

            //public void Stop()
            //{
            //    Instance.StopAllCoroutines();
            //}

            public void Use()
            {
                void Slow(TD_PatrolController patrolController, float speed)
                {
                    //LevelResultController.Instance.OnShowPanel += Instance.m_TimeAbility.Stop;

                    patrolController.SetNavigationLinear(speed);
                }

                void SlowEnemy(Enemy enemy)
                {
                    Slow(enemy.GetComponent<TD_PatrolController>(), m_speedSlowRatio);
                }

                IEnumerator Restore()
                {
                    yield return new WaitForSeconds(m_Duration);

                    foreach (var dest in Destructible.AllDestructible)
                    {
                        Slow(dest.GetComponent<TD_PatrolController>(), dest.GetComponent<TD_PatrolController>().PreviousSpeed);
                    }

                    EnemyWaveManager.OnEnemySpawn -= SlowEnemy;
                }

                foreach (var dest in Destructible.AllDestructible)
                {
                    Slow(dest.GetComponent<TD_PatrolController>(), m_speedSlowRatio);
                }

                EnemyWaveManager.OnEnemySpawn += SlowEnemy;

                Instance.StartCoroutine(Restore());

                IEnumerator TimeAbilityButton()
                {
                    Instance.m_TimeButton.interactable = false;

                    yield return new WaitForSeconds(m_CoolDown);

                    Instance.m_TimeButton.interactable = true;
                }

                Instance.StartCoroutine(TimeAbilityButton());

                //LevelResultController.Instance.OnShowPanel -= Instance.m_TimeAbility.Stop;
            }
        }

        [SerializeField] private Button m_TimeButton;

        [SerializeField] private FireAbility m_FireAbility;

        public void UseFireAbility() => m_FireAbility.Use();

        [SerializeField] private TimeAbility m_TimeAbility;

        public void UseTimeAbility() => m_TimeAbility.Use();

        //private void OnDestroy()
        //{
        //    LevelResultController.Instance.OnShowPanel -= Instance.m_TimeAbility.Stop;
        //}
    }
}