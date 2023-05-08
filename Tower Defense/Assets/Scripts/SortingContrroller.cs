using UnityEngine;

namespace TowerDefense
{
    public class SortingContrroller : MonoBehaviour
    {
        private SpriteRenderer m_SpriteRenderer;

        private void Start()
        {
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {//—ортирует спрайты по sortingOrder в зависимости от положени€ Y.
            m_SpriteRenderer.sortingOrder = -(int)((m_SpriteRenderer.bounds.min.y) * 100);

            //float distanceToCamera = Vector3.Distance(transform.position, Camera.main.transform.position);

            //m_SpriteRenderer.sortingOrder = -(int)(distanceToCamera * 100);

            //foreach (Transform child in transform)
            //{
            //    SpriteRenderer childSprite = child.GetComponent<SpriteRenderer>();

            //    if (childSprite != null)
            //    {
            //        childSprite.sortingOrder = m_SpriteRenderer.sortingOrder;
            //    }
            //}
        }
    }
}