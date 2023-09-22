using System.Collections.Generic;
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
        [SerializeField] private string _filePath = "../ProjectRPG-Server/Common/MapData";

        private KeyValuePair<Vector3, int>[,] _map = null;

        public void GenerateMap()
        {
            Debug.Log("Generate Map");
            
            int minX = (int)transform.position.x - _calculationScale.x;
            int maxX = (int)transform.position.x + _calculationScale.x;
            int minZ = (int)transform.position.z - _calculationScale.z;
            int maxZ = (int)transform.position.z + _calculationScale.z;

            int sizeX = _calculationScale.x * 2 + 1;
            int sizeZ = _calculationScale.z * 2 + 1;

            _map = new KeyValuePair<Vector3, int>[sizeZ, sizeX];

            for (int z = maxZ; z >= minZ; z--)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    var origin = new Vector3(x, _calculationScale.y, z);
                    if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit))
                    {
                        _map[z - minZ, x - minX] = new KeyValuePair<Vector3, int>(hit.point, hit.collider.gameObject.layer);
                    }
                }
            }

            using (var writer = File.CreateText($"{_filePath}/{_mapName}.txt"))
            {
                writer.WriteLine(minX);
                writer.WriteLine(maxX);
                writer.WriteLine(minZ);
                writer.WriteLine(maxZ);

                for (int z = maxZ; z >= minZ; z--)
                {
                    for (int x = minX; x <= maxX; x++)
                    {
                        var data = _map[z - minZ, x - minX];
                        int height = (int)data.Key.y;
                        int collisionFlag = data.Value == _obstacleLayerMask ? 1 : 0;
                        int cellData = (collisionFlag << 8) | height;
                        writer.Write($"{cellData} ");
                    }
                    writer.WriteLine();
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(transform.position, _calculationScale * 2);

            if (_map != null)
            {
                foreach (var origin in _map)
                {
                    int layer = origin.Value;

                    if (layer == _groundLayerMask)
                        Gizmos.color = _groundColor;
                    else if (layer == _obstacleLayerMask)
                        Gizmos.color = _obstacleColor;
                    else
                        Gizmos.color = Color.black;

                    Gizmos.DrawCube(origin.Key + new Vector3(0, 0.001f, 0), new Vector3(0.95f, 0, 0.95f));
                }
            }
        }
    }
}