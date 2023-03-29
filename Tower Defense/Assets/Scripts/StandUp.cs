using UnityEngine;

namespace TowerDefense
{
    public class StandUp : MonoBehaviour
    {
        private Rigidbody2D m_rigidbody;
        private SpriteRenderer m_spriteRenderer;

        private void Start()
        {
            m_rigidbody = transform.root.GetComponent<Rigidbody2D>();
            m_spriteRenderer = GetComponent<SpriteRenderer>();
        }
        private void LateUpdate()
        {
            transform.up = Vector2.up;

            if (m_rigidbody.velocity.x > 0.01f)
            {
                m_spriteRenderer.flipX = false;
            } else if (m_rigidbody.velocity.x < 0.01f)
            {
                m_spriteRenderer.flipX = true;
            }
        }
    }
}