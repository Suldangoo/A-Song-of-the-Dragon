using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

[System.Serializable]
public class Monster
{
    public string Name; // ���� �̸�
    public Sprite illustration; // ���� �Ϸ���Ʈ
    public int AttackPower; // ������ ���ݷ�
    public int HP; // ������ ü��
    public int RewardGold; // óġ �� ���� ���
}

[System.Serializable]
public class Dungeon
{
    public string title; // ���� �̸�
    public Sprite illustration; // ���� �Ϸ���Ʈ
    public string dedescription; // ���� ù ���� �� ���� �ؽ�Ʈ
    [TextArea(1, 50)] public string[] text; // ���� Ž�� �� ����� �ؽ�Ʈ��
    public Monster[] monsters; // �ش� �������� ���� �� �ִ� ����
    public Monster boss; // �ش� ������ ����
}

public class DungeonManager : MonoBehaviour
{
    #region �̱���
    public static DungeonManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DungeonManager>();
            }
            return instance;
        }
    }
    private static DungeonManager instance;
    #endregion

    EventManager eventManager => EventManager.Instance;

    public Dungeon currentDungeon; // ���� �÷��̾ ��ġ�� ����
    public int progress = 0; // ���� �����

    public Dungeon[] dungeons; // ����

    // ȭ���� UI�� ǥ�õǴ� ������Ʈ��
    [SerializeField] private Image illustrationImage;
    [SerializeField] private TMP_Text dungeonProgress;
    [SerializeField] private Image progressBar;
    [SerializeField] private TMP_Text text;
    [SerializeField] private TMP_Text button1;
    [SerializeField] private TMP_Text button2;
    [SerializeField] private TMP_Text button3;
    [SerializeField] private TMP_Text button4;

    // ������ ���� �޼ҵ�
    public void EnterDungeon(int dungeon)
    {
        currentDungeon = dungeons[dungeon]; // ��ġ�� ���� ����
        dungeonProgress.gameObject.SetActive(true); // ���� ������� ��Ÿ���� GUI ������Ʈ �ѱ�

        UpdateProgressUI();

        illustrationImage.sprite = currentDungeon.illustration; // ���� �Ϸ���Ʈ ���
        text.text = currentDungeon.dedescription; // ���� ���� ���

        // ��ư1 �ؽ�Ʈ�� �̺�Ʈ ����
        SetButton(button1, "Ž���Ѵ�.", ExploreDungeon);

        // ��ư2 �ؽ�Ʈ�� �̺�Ʈ ����
        SetButton(button2, "���ư���.", BackToVillage);

        // ��Ȱ��ȭ�� ��ư3�� ��ư4
        button3.gameObject.SetActive(false);
        button4.gameObject.SetActive(false);
    }

    // ��ư �ؽ�Ʈ�� �̺�Ʈ�� �����ϴ� �޼ҵ�
    private void SetButton(TMP_Text buttonText, string text, UnityAction onClickEvent)
    {
        // ���� �κп� '�� ' �߰�
        string formattedText = $"�� {text}";
        buttonText.text = formattedText;

        // UITextInteraction ��ũ��Ʈ�� �߰��ϰ� onClickEvent�� �Ҵ�
        UITextInteraction textInteraction = buttonText.gameObject.GetComponent<UITextInteraction>();
        if (textInteraction == null)
        {
            textInteraction = buttonText.gameObject.AddComponent<UITextInteraction>();
        }

        // UnityAction�� UnityEvent�� ��ȯ�Ͽ� �Ҵ�
        UnityEvent clickAction = new UnityEvent();
        clickAction.AddListener(onClickEvent);
        textInteraction.onClickEvent = clickAction;

        // SetActive�� �ؽ�Ʈ�� ������� ���� ��쿡�� True�� ����
        buttonText.gameObject.SetActive(!string.IsNullOrEmpty(text));
    }

    private void UpdateProgressUI()
    {
        dungeonProgress.text = $"���� �����: {progress}%";
        progressBar.fillAmount = progress / 100f;
    }

    // ������ Ž���ϴ� �޼ҵ�
    public void ExploreDungeon()
    {
        // ���⿡ ���� Ž�� �� �߻��� �̺�Ʈ �� ���൵ ��� ���� �߰�
        // ���Ŀ��� ��ư �ؽ�Ʈ�� �̺�Ʈ�� �ٽ� ������Ʈ�� �� �ֽ��ϴ�.
    }

    // ������ ��ȯ�ϴ� �޼ҵ�
    public void BackToVillage()
    {
        progress = 0; // ���� ���൵ �ʱ�ȭ
        dungeonProgress.gameObject.SetActive(false); // ���� ������� ��Ÿ���� GUI ������Ʈ ����

        eventManager.ChangeEvent(0); // ������ ���ư���
    }
}
