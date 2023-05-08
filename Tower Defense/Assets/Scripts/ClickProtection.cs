using SpaceShooter;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TowerDefense
{

    public class ClickProtection : SingletonBase<ClickProtection>, IPointerClickHandler, IPointerMoveHandler
    {
        private Image m_BlockerImage;

        private Action<Vector2> m_OnClickAction;

        private void Start()
        {
            m_BlockerImage = GetComponent<Image>();
        }

        public void Activate(Action<Vector2> mouseAction)
        {
            m_BlockerImage.enabled = true;

            m_OnClickAction = mouseAction;
        }

        //Передает в Action вектор клика мышки и скрывает мишень.
        public void OnPointerClick(PointerEventData eventData)
        {
            m_BlockerImage.enabled = false;

            m_OnClickAction(eventData.pressPosition);

            m_OnClickAction = null;

            Abilities.Instance.TargetCircle.enabled = false;
        }

        //Перемещает мишень в координаты мышки.
        public void OnPointerMove(PointerEventData eventData)
        {
            Abilities.Instance.TargetCircle.transform.position = eventData.position;
        }
    }
}