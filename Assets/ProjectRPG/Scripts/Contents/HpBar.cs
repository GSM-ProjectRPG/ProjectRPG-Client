using UnityEngine;

namespace ProjectRPG
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField]
        private Transform _hpBar;

        public void SetHpBar(float ratio)
        {
            ratio = Mathf.Clamp01(ratio);
            _hpBar.localScale = new Vector3(ratio, 1, 1);
        }
    }
}