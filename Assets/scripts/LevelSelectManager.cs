using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    [Header("Popup")]
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text objective1Text;
    [SerializeField] private TMP_Text objective2Text;
    [SerializeField] private TMP_Text objective3Text;

    [Header("Scene")]
    [SerializeField] private string gameSceneName = "CityAssault";

    private int selectedLevel = 1;

    public void OpenLevelPopup(int levelNumber)
    {
        selectedLevel = levelNumber;

        titleText.text = $"Niveau {levelNumber}";

        int shipsToDestroy = levelNumber * 10;
        int precisionRequired = 65 + levelNumber * 5;
        int timeLimit = 60 + levelNumber * 10;

        objective1Text.text = $"Détruire {shipsToDestroy} navires";
        objective2Text.text = $"Précision ≥ {precisionRequired}%";
        objective3Text.text = $"Finir en moins de {timeLimit}s";

        popupPanel.SetActive(true);
    }

    public void StartSelectedLevel()
    {
        PlayerPrefs.SetInt("SelectedLevel", selectedLevel);
        PlayerPrefs.Save();

        SceneManager.LoadScene(gameSceneName);
    }

    public void ClosePopup()
    {
        popupPanel.SetActive(false);
    }
}