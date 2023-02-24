using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using YG;

namespace LinePaint
{
    public class UIManager_paint : MonoBehaviour
    {
        [SerializeField] private Text totalDiamonds, diamondsEarned, levelText, controlInfoText, infoText;
        [SerializeField] private GameObject mainMenu, levelCompleteMenu, extraBtnHolder, sountBtnOff;
        [SerializeField] private Button settingsBtn, nextButton, soundBtn, vbrationBtn, retryBtn;

        public Text LevelText { get => levelText; }
        public Text TotalDiamonds { get => totalDiamonds; }
        public GameObject panel_loading;

        private void OnEnable()
        {
            YandexGame.OpenFullAdEvent += LevelCompleted;
            //YandexGame.GetDataEvent += OnGetData;
        }

        private void OnDisable()
        {
            YandexGame.OpenFullAdEvent -= LevelCompleted;
            //YandexGame.GetDataEvent -= OnGetData;
        }

        private void Start()
        {
            OnGetData();
        }

        private void OnGetData()
        {
            var savesDataVolume = YandexGame.savesData.Volume;
            
            if (GameManager_paint.currentLevel >= 1)
            {
                infoText.gameObject.SetActive(false);
                controlInfoText.gameObject.SetActive(false);
            }  
            
            sountBtnOff.SetActive(savesDataVolume == 0);
            AudioListener.volume = savesDataVolume;

            settingsBtn.onClick.AddListener(() => OnClick(settingsBtn));
            nextButton.onClick.AddListener(() => OnClick(nextButton));
            soundBtn.onClick.AddListener(() => OnClick(soundBtn));
            retryBtn.onClick.AddListener(() => OnClick(retryBtn));
        }

        private void OnClick(Button btn)
        {
            SoundManager_paint.Instance.PlayFx(FxType.Button);
            switch (btn.name)
            {
                case "SettingsBtn":
                    extraBtnHolder.SetActive(!extraBtnHolder.activeInHierarchy);
                    break;
                case "NextBtn":
                case "RetryBtn":
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    break;
                case "SoundBtn":
                    var savesDataVolume = YandexGame.savesData.Volume;
                    
                    YandexGame.savesData.Volume = savesDataVolume == 0 ? 1 : 0;
                    sountBtnOff.SetActive(savesDataVolume == 0);
                    AudioListener.volume = savesDataVolume;
                    
                    YandexGame.SaveProgress();
                    break;
            }
        }

        public void LevelCompleted()
        {
            mainMenu.SetActive(false);
            levelCompleteMenu.SetActive(true);
            totalDiamonds.text = "" + GameManager_paint.totalDiamonds;
        }
        public void click_menu()
        {
            panel_loading.SetActive(true); 
            SceneManager.LoadSceneAsync(0, LoadSceneMode.Single); 
        }

    }
}