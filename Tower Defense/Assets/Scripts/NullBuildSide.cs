using UnityEngine.EventSystems;

namespace TowerDefense
{
    public class NullBuildSide : BuildSite
    {//Скрываем меню постройки при попадании мышью в пустоту.
        public override void OnPointerDown(PointerEventData eventData)
        {
            HideControls();
        }
    }
}