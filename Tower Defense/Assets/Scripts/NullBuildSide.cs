using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense
{
    public class NullBuildSide : BuildSite
    {
        public override void OnPointerDown(PointerEventData eventData)
        {
            HideControls();
        }
    }
}