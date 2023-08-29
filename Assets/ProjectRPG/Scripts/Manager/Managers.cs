using UnityEngine;

namespace ProjectRPG
{
    public class Managers : MonoBehaviour
    {
        private static Managers _instance;
        public static Managers Instance { get { Init(); return _instance; } }

        #region Contents
        private InventoryManager _inventory = new InventoryManager();
        private MapManager _map = new MapManager();
        private ObjectManager _object = new ObjectManager();
        private NetworkManager _network = new NetworkManager();
        private WebManager _web = new WebManager();

        public static InventoryManager Inventory { get => Instance._inventory; }
        public static MapManager Map { get => Instance._map; }
        public static ObjectManager Object { get => Instance._object; }
        public static NetworkManager Network { get => Instance._network; }
        public static WebManager Web { get => Instance._web; }
        #endregion

        #region Core
        DataManager _data = new DataManager();
        PoolManager _pool = new PoolManager();
        ResourceManager _resource = new ResourceManager();
        SceneManagerEx _scene = new SceneManagerEx();
        SoundManager _sound = new SoundManager();
        UIManager _ui = new UIManager();

        public static DataManager Data { get => Instance._data; }
        public static PoolManager Pool { get => Instance._pool; }
        public static ResourceManager Resource { get => Instance._resource; }
        public static SceneManagerEx Scene { get => Instance._scene; }
        public static SoundManager Sound { get => Instance._sound; }
        public static UIManager UI { get => Instance._ui; }
        #endregion

        private void Start()
        {
            Init();
        }

        private void Update()
        {
            _network.Update();
        }

        private static void Init()
        {
            if (_instance == null)
            {
                GameObject go = GameObject.Find("@Managers");

                if (go == null)
                {
                    go = new GameObject { name = "@Managers" };
                    go.AddComponent<Managers>();
                }

                DontDestroyOnLoad(go);
                _instance = go.GetComponent<Managers>();

                _instance._data.Init();
                _instance._pool.Init();
                _instance._sound.Init();
            }
        }

        public static void Clear()
        {
            Sound.Clear();
            Scene.Clear();
            UI.Clear();
            Pool.Clear();
        }
    }
}