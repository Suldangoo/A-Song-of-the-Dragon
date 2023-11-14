using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CreateCharacter : MonoBehaviour
{
    SceneChanger sceneChanger => SceneChanger.Instance; // �� ü����

    [SerializeField] private TextMeshProUGUI strengthText;
    [SerializeField] private TextMeshProUGUI agilityText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI wisdomText;
    [SerializeField] private TextMeshProUGUI charmText;
    [SerializeField] private TextMeshProUGUI remainingPointsText;

    [SerializeField] private GameObject[] decreaseButtons;
    [SerializeField] private GameObject[] increaseButtons;

    [SerializeField] private int minStatus = 5;
    [SerializeField] private int maxStatus = 15;

    [SerializeField] private int[] stats = { 5, 5, 5, 5, 5 };
    [SerializeField] private int remainingPoints = 15;

    [SerializeField] private TMP_InputField nicknameInputField; // �г��� �Է� �ʵ�
    [SerializeField] private Toggle maleToggle; // ���� ���
    [SerializeField] private Toggle femaleToggle; // ���� ���

    [SerializeField] private GameObject prologue; // ���ѷα� UI

    private void Start()
    {
        UpdateUI();
    }

    public void DownStatus(int index)
    {
        if (stats[index] > minStatus)
        {
            stats[index]--;
            remainingPoints++;
            UpdateUI();
        }
    }

    public void UpStatus(int index)
    {
        if (stats[index] < maxStatus && remainingPoints > 0)
        {
            stats[index]++;
            remainingPoints--;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        strengthText.text = stats[0].ToString();
        agilityText.text = stats[1].ToString();
        healthText.text = stats[2].ToString();
        wisdomText.text = stats[3].ToString();
        charmText.text = stats[4].ToString();
        remainingPointsText.text = remainingPoints.ToString();

        for (int i = 0; i < 5; i++)
        {
            decreaseButtons[i].SetActive(stats[i] > minStatus);
            increaseButtons[i].SetActive(stats[i] < maxStatus && remainingPoints > 0);
        }
    }

    public void OnStartGame()
    {
        string nickname = nicknameInputField.text.Trim();

        if (string.IsNullOrEmpty(nickname))
        {
            Debug.LogWarning("�г����� �Է��ϼ���.");
            return;
        }

        if (remainingPoints > 0)
        {
            Debug.LogWarning("���� �������ͽ� ����Ʈ�� ��� ����ϼ���.");
            return;
        }

        SaveCharacterData(nickname);

        prologue.SetActive(true);
    }

    public void OnPrologue(bool value)
    {
        if (value) { sceneChanger.SceneChange("Prologue"); }
        else { sceneChanger.SceneChange("Game"); } // ���ѷα� ��ŵ
    }

    private void SaveCharacterData(string nickname)
    {
        bool isMale = maleToggle.isOn; // ���� ����� ���õǾ� ������ true, ���ڸ� false
        int[] stats = GetStats();

        SaveManager.Progress = true;
        SaveManager.Gender = isMale;
        SaveManager.NickName = nickname;
        SaveManager.Strength = stats[0];
        SaveManager.Agility = stats[1];
        SaveManager.Health = stats[2];
        SaveManager.Wisdom = stats[3];
        SaveManager.Charm = stats[4];

        SaveManager.Level = 1; // ���� �ʱ�ȭ
        SaveManager.Hp = SaveManager.Health * 3; // �ǰ� ���� 1�� Hp 3 �ο�
    }

    private int[] GetStats()
    {
        return stats;
    }
}