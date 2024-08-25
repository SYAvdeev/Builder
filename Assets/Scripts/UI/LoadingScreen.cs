using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Builder.UI
{
    public class LoadingScreen : UIScreen
    {
        [SerializeField] private TextMeshProUGUI _percentProgressText;
        [SerializeField] private Slider _loadingSlider;

        public void SetProgress(float progress)
        {
            _loadingSlider.value = progress;
            _percentProgressText.text = ((int)(progress * 100)).ToString() + "%";
        }
        
        protected override UniTask ShowInternal(bool isImmediate)
        {
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        protected override UniTask HideInternal(bool isImmediate)
        {
            gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }
    }
}
