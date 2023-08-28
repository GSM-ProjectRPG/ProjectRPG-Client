using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;

namespace ProjectRPG
{
    [System.Serializable]
    public class SpawnArea
    {
        public Color GizmoColor;

        [Header("Spawn Area Settings")]
        public string Name;
        public Vector3Int Offset;
        public int Radius;
        public int MaxSpawnCount;
    }

    public class MapGenerator : MonoBehaviour
    {
        [Header("Map Settings")]
        [SerializeField] private Vector3Int _calculationScale;

        [Space]
        [SerializeField] private Color _groundColor;
        [SerializeField] private Color _obstacleColor;
        [SerializeField] private int _groundLayerMask;
        [SerializeField] private int _obstacleLayerMask;
        [SerializeField] private List<SpawnArea> _spawnAreas;

        [Header("Extract Settings")]
        [SerializeField] private string _mapName;
        [SerializeField] private string _filePath = "Assets/ProjectRPG/Resources/Map";

        private List<List<Vector3>> _map = new List<List<Vector3>>();
        private Dictionary<Vector3, int> _mapData = new Dictionary<Vector3, int>();

        public void GenerateMap()
        {
            Debug.Log("Generate Map");

            _map.Clear();
            _mapData.Clear();

            int sizeX = _calculationScale.x * 2 + 1;
            int sizeZ = _calculationScale.z * 2 + 1;

            int minX = (int)transform.position.x - _calculationScale.x;
            int maxX = (int)transform.position.x + _calculationScale.x;
            int minZ = (int)transform.position.z - _calculationScale.z;
            int maxZ = (int)transform.position.z + _calculationScale.z;

            for (int z = maxZ; z >= minZ; z--)
            {
                var list = new List<Vector3>();

                for (int x = minX; x <= maxX; x++)
                {
                    var origin = new Vector3(x, _calculationScale.y, z);
                    
                    if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit))
                    {
                        var pos = hit.point;
                        list.Add(pos);
                        _mapData.Add(pos, hit.collider.gameObject.layer);
                    }
                }

                _map.Add(list);
            }

            using (var writer = File.CreateText($"{_filePath}/{_mapName}.txt"))
            {
                writer.WriteLine(minX);
                writer.WriteLine(maxX);
                writer.WriteLine(minZ);
                writer.WriteLine(maxZ);

                foreach (var line in _map)
                {
                    foreach (var position in line)
                    {
                        writer.Write((int)position.y);
                    }
                    writer.WriteLine();
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(transform.position, _calculationScale * 2);


            foreach (var origin in _map.SelectMany(list => list))
            {
                if (_mapData.TryGetValue(origin, out int layer))
                {
                    if (layer == _groundLayerMask)
                        Gizmos.color = _groundColor;
                    else if (layer == _obstacleLayerMask)
                        Gizmos.color = _obstacleColor;
                    else
                        Gizmos.color = Color.black;
                }

                Gizmos.DrawCube(origin + new Vector3(0, 0.001f, 0), new Vector3(0.95f, 0, 0.95f));
            }
        }
    }
}