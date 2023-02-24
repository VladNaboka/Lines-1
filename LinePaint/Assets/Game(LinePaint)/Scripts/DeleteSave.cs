using UnityEngine;
using UnityEngine.UI;

public class DeleteSave : MonoBehaviour
{
    private int counter = 0;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            counter++;

            if (counter >= 5)
            {
                AppManager.Instance.RemoveSave();
            }
        });
    }
}
