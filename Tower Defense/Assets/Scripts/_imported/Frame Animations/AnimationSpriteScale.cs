using UnityEngine;

namespace SpaceShooter
{
    public class AnimationSpriteScale : AnimationBase
    {
        [SerializeField] private GameObject m_Renderer;

        [SerializeField] private AnimationCurve m_CurveX;

        [SerializeField] private AnimationCurve m_CurveY;
        private Vector2 m_InitialSize;

        private void Awake()
        {
            m_InitialSize = m_Renderer.GetComponent<RectTransform>().localScale;
        }

        //Подготовка анимации.
        public override void PrepareAnimation()
        {
            var x = m_CurveX.Evaluate(0) * m_InitialSize.x;

            var y = m_CurveY.Evaluate(0) * m_InitialSize.y;

            m_Renderer.GetComponent<Transform>().localScale = new Vector2(x, y);
        }

        //Анимация кадра.
        protected override void AnimateFrame()
        {
            var x = m_CurveX.Evaluate(NormalizeAnimationTime) * m_InitialSize.x;

            var y = m_CurveY.Evaluate(NormalizeAnimationTime) * m_InitialSize.y;

            m_Renderer.GetComponent<Transform>().localScale = new Vector2(x, y);
        }

        protected override void OnAnimationEnd()
        {
           
        }

        protected override void OnAnimationStart()
        {
            
        }
    }
}
