using System.Collections.Generic;
using UnityEngine;

namespace ProjectRPG
{
    public interface ILoader<Key, Value>
    {
        Dictionary<Key, Value> MakeDict();
    }

    public class DataManager
    {
        // TODO : Data Dictionary

	    public void Init()
        {
            // TODO : LoadJson And MakeDict
	    }

        private Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
        {
		    TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Loader>(textAsset.text);
	    }
    }
}