using UnityEngine;

namespace ProjectRPG
{
    public class CharacterCustomizer : MonoBehaviour
    {
        private SkinnedMeshRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        }

        public void Customize(int bodyId, int faceId, int accessoryId)
        {
            if (_renderer != null)
            {
                var materials = _renderer.materials;
                materials[0] = Managers.Resource.Load<Material>($"Prefabs/Customize/Body/M_Chibi_Cat_{bodyId:00}");
                materials[1] = Managers.Resource.Load<Material>($"Prefabs/Customize/Face/M_Chibi_Emo_{faceId:00}");
                _renderer.materials = materials;
            }
        }

        public void Customize(Material body, Material face)
        {
            if (_renderer != null)
            {
                var materials = _renderer.materials;
                materials[0] = body;
                materials[1] = face;
                _renderer.materials = materials;
            }
        }
    }
}