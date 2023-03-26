using UnityEngine;

namespace TowerDefense
{
    [RequireComponent(typeof(TD_PatrolController))]
    public class Enemy : MonoBehaviour
    {
        public void Use(EnemyAsset asset)
        {
            //var sr = GetComponentInChildren<SpriteRenderer>();

            var sr = transform.Find("Sprite").GetComponent<SpriteRenderer>();

            sr.color = asset.color;
        }
    }
}