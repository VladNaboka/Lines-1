using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class AppManager : Singleton<AppManager>
{
    [SerializeField] private YandexGame yandex;

    protected override void AfterAwaik()
    {
        YandexGame.GetDataEvent += () =>
         {
             SceneManager.LoadScene(1);
         };
    }

    public void ShowAd()
    {
        yandex._FullscreenShow();
    }

    public void RemoveSave()
    {
        YandexGame.savesData.CurrentLevel = 0;
        YandexGame.savesData.TotalDiamonds = 0;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
