using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu(fileName = "TowerAsset", menuName = "Tower/TowerAsset")]
    [System.Serializable]
    public class TowerAsset : ScriptableObject
    {
        public int GoldCost = 15;
        public Sprite GUISprite;
        public Sprite ElementSprite;

    }
}