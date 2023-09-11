using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectRPG
{
    public class UI_SelectServerPopup : UI_Popup
    {
        public List<UI_SelectServerPopup_Item> Items = new List<UI_SelectServerPopup_Item>();

        public override void Init()
        {
            base.Init();
        }

        public void SerServerInfo(List<ServerInfo> servers)
        {
            Items.Clear();

            var grid = GetComponentInChildren<GridLayoutGroup>().gameObject;
            foreach (Transform child in grid.transform)
                Destroy(child.gameObject);

            foreach (var info in servers)
            {
                var go = Managers.Resource.Instantiate("UI/Popup/UI_SelectServerPopup_Item", grid.transform);
                var item = go.GetOrAddComponent<UI_SelectServerPopup_Item>();
                Items.Add(item);
                item.Info = info;
            }

            RefreshUI();
        }

        public void RefreshUI()
        {
            if (Items.Count == 0) return;

            foreach (var item in Items)
                item.RefreshUI();
        }
    }
}