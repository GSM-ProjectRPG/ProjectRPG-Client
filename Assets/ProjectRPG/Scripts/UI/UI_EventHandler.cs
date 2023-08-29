﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ProjectRPG
{
    public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IDragHandler
    {
        public Action<PointerEventData> OnClickHandler = null;
        public Action<PointerEventData> OnDragHandler = null;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClickHandler?.Invoke(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            OnDragHandler?.Invoke(eventData);
        }
    }
}