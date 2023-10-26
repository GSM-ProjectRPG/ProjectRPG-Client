using System;
using System.Collections.Generic;
using Google.Protobuf.Protocol;
using UnityEngine;

namespace ProjectRPG
{
    public class ObjectManager
    {
        public PlayerController MyPlayer { get; set; }
        
        private Dictionary<int, GameObject> _objects = new Dictionary<int, GameObject>();

        public static GameObjectType GetObjectTypeById(int id)
        {
            var type = (id >> 24) & 0x7F;
            return (GameObjectType)type;
        }

        public GameObject FindObjectById(int id)
        {
            _objects.TryGetValue(id, out var go);
            return go;
        }

        public GameObject FindObject(Func<GameObject, bool> predicate)
        {
            foreach (var go in _objects.Values)
            {
                if (predicate.Invoke(go))
                    return go;
            }
            return null;
        }

        public void Add(GameObjectInfo info, bool myPlayer = false)
        {
            if (MyPlayer != null && MyPlayer.Id == info.Id) return;
            if (_objects.ContainsKey(info.Id)) return;

            var type = GetObjectTypeById(info.Id);
            if (type == GameObjectType.Player)
            {
                var go = Managers.Resource.Instantiate("Creature/TestPlayer");
                go.name = info.Name;
                _objects.Add(info.Id, go);

                if (myPlayer)
                {
                    MyPlayer = go.GetComponent<PlayerController>();
                    MyPlayer.Id = info.Id;
                    MyPlayer.IsMine = true;
                    MyPlayer.ServerPos = info.Transform.Position.ToVector3();
                    MyPlayer.Stat.MergeFrom(info.Stat);
                }
                else
                {
                    var pc = go.GetComponent<PlayerController>();
                    pc.Id = info.Id;
                    pc.ServerPos = info.Transform.Position.ToVector3();
                    pc.Stat.MergeFrom(info.Stat);
                }

                var customizer = go.GetComponentInChildren<CharacterCustomizer>();
                if (customizer != null)
                {
                    customizer.Customize(info.CustomizeInfo);
                }
            }
            else if (type == GameObjectType.Monster)
            {

            }
        }

        public void Remove(int id)
        {
            if (MyPlayer != null && MyPlayer.Id == id) return;
            if (_objects.ContainsKey(id) == false) return;

            var go = FindObjectById(id);
            if (go == null) return;

            _objects.Remove(id);
            Managers.Resource.Destroy(go);
        }

        public void Clear()
        {
            foreach (var go in _objects.Values)
                Managers.Resource.Destroy(go);
            _objects.Clear();
            MyPlayer = null;
        }
    }
}