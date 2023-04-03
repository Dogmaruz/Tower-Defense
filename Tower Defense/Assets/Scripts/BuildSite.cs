﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense
{
    public class BuildSite : MonoBehaviour, IPointerDownHandler
    {
        public static event Action<Transform> OnclickEvent;

        public static void HideControls()
        {
            OnclickEvent(null);
        }
        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnclickEvent(transform.root);
        }
    }
}