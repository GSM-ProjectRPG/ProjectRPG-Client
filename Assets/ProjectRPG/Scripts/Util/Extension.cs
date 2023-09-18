using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Google.Protobuf.Protocol;

namespace ProjectRPG
{
    public static class Extension
    {
        public static T GetOrAddComponent<T>(this GameObject go) where T : Component
        {
            return Util.GetOrAddComponent<T>(go);
        }

        public static void BindEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
        {
            UI_Base.BindEvent(go, action, type);
        }

        public static bool IsValid(this GameObject go)
        {
            return go != null && go.activeSelf;
        }

        public static Vector3 ToVector3(this Vector vector)
        {
            return new Vector3(vector.X, vector.Y, vector.Z);
        }
    }
}