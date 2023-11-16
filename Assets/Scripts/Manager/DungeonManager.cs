using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System.Collections;

[System.Serializable]
public class Monster
{
    public string Name; // ���� �̸�
    public int Code; // ���� �ڵ�
    public int AttackPower; // ������ ���ݷ�
    public int HP; // ������ ü��
    public int Speed; // ������ ���ǵ�
    public int RewardGold; // óġ �� ���� ���
    public int RewardExp; // óġ �� ���� ����ġ
}

[System.Serializable]
public class Dungeon
{
    public string title; // ���� �̸�
    public Sprite illustration; // ���� �Ϸ���Ʈ
    public string dedescription; // ���� ù ���� �� ���� �ؽ�Ʈ
    public int treasure; // ���� �ִ� ���� ����
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
    InfoManager infoManager => InfoManager.Instance;
    ItemManager itemManager => ItemManager.Instance;

    public Dungeon currentDungeon; // ���� �÷��̾ ��ġ�� ����
    public int progress = 0; // ���� �����

    public Dungeon[] dungeons; // ����

    // ȭ���� UI�� ǥ�õǴ� ������Ʈ��
    [SerializeField] private Image illustrationImage;
    [SerializeField] private Image monsterImage;
    [SerializeField] private Animator monsterAnim;
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
        illustrationImage.sprite = currentDungeon.illustration; // ���� �Ϸ���Ʈ ���

        StartDungeon(currentDungeon);
    }

    public void StartDungeon(Dungeon dungeon)
    {
        UpdateProgressUI();

        text.text = currentDungeon.dedescription; // ���� ���� ���

        monsterImage.gameObject.SetActive(false);
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);

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
        infoManager.UpdateInfo();

        illustrationImage.gameObject.SetActive(currentDungeon.illustration != null);
        dungeonProgress.text = $"���� �����: {progress}%";
        progressBar.fillAmount = progress / 100f;
    }

    // ������ Ž���ϴ� �޼ҵ�
    public void ExploreDungeon()
    {
        int randomValue = Random.Range(1, 101); // 1���� 100������ ������ ��

        // ���� ����� 1~4% ���
        progress += Random.Range(1, 5);

        // ������Ʈ�� ���� ������ UI�� �ݿ�
        UpdateProgressUI();

        if (randomValue <= 40) // 40% Ȯ��: ���� Ž�� �ؽ�Ʈ
        {
            // Dungeon Ŭ������ text �� �����ϰ� ����
            string dungeonText = currentDungeon.text[Random.Range(0, currentDungeon.text.Length)];
            ShowDungeonText(dungeonText);
        }
        else if (randomValue <= 70) // 30% Ȯ��: ���� ����
        {
            EncounterMonster();
        }
        else if (randomValue <= 90) // 20% Ȯ��: �޽�ó �̺�Ʈ (�̱���)
        {
            RestEvent();
        }
        else // 10% Ȯ��: ���� �̺�Ʈ (�̱���)
        {
            TreasureEvent();
        }
    }

    // ���� Ž�� �ؽ�Ʈ�� ����ϴ� �޼ҵ�
    private void ShowDungeonText(string text)
    {
        this.text.text = text;

        // ������Ʈ�� ���� ������ UI�� �ݿ�
        UpdateProgressUI();
    }

    // ���� ���� �̺�Ʈ�� ó���ϴ� �޼ҵ�
    private void EncounterMonster()
    {
        monsterImage.gameObject.SetActive(true); // ���� Ȱ��ȭ
        dungeonProgress.gameObject.SetActive(false); // ��� ��Ȱ��ȭ

        Monster randomMonster = currentDungeon.monsters[Random.Range(0, currentDungeon.monsters.Length)];

        monsterAnim.SetInteger("monster", randomMonster.Code); // ���� �ִϸ����� ���

        this.text.text = $"{randomMonster.Name}��(��) �����ߴ�!\nü��: {randomMonster.HP}\n���ݷ�: {randomMonster.AttackPower}\n���ǵ�: {randomMonster.Speed}";

        // ��ư1 �ؽ�Ʈ�� �̺�Ʈ ����
        SetButton(button1, "���� ����!", () => StartBattle(randomMonster));

        // ��ư2 �ؽ�Ʈ�� �̺�Ʈ ����
        SetButton(button2, "����ġ��.", RunAway);

        // ��Ȱ��ȭ�� ��ư3�� ��ư4
        button3.gameObject.SetActive(false);
        button4.gameObject.SetActive(false);
    }

    // ���ͷκ��� ����ġ�� �޼ҵ�
    public void RunAway()
    {
        monsterImage.gameObject.SetActive(false); // ���� ��Ȱ��ȭ
        dungeonProgress.gameObject.SetActive(true);

        StartDungeon(currentDungeon);
    }

    // ���� ���� �޼ҵ�
    private void StartBattle(Monster monster)
    {
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);

        StartCoroutine(AutoBattle(monster));
    }

    // �ڵ� ���� �ڷ�ƾ
    private IEnumerator AutoBattle(Monster monster)
    {
        // ���� �α� �ʱ�ȭ
        text.text = "";

        // ���� �ʱ� ü�� ����
        int monsterHP = monster.HP;

        // ���� ���� ���
        while (SaveManager.Hp > 0 && monsterHP > 0)
        {
            // �÷��̾�� ������ ������ ���
            int playerDamage = CalculatePlayerDamage();
            int monsterDamage = CalculateMonsterDamage(monster);

            // ���Ϳ��� ������ ������
            monsterHP -= playerDamage;

            // ���� �α׿� �÷��̾��� ���� �޽��� �߰�
            text.text += $"����� ����! {playerDamage}�� �������� �־���!\n";

            // ���� ��� üũ
            if (monsterHP <= 0)
            {
                // ���� ��� ó��
                monsterAnim.SetTrigger("death");

                // ���� �α׿� �¸� �޽��� �߰�
                text.text += $"{monster.Name}���� �������� �¸��ߴ�!\n";
                text.text += $"{monster.RewardGold}��带 ȹ���ߴ�.\n";
                text.text += $"{monster.RewardExp}��ŭ�� ����ġ�� ȹ���ߴ�.";

                // SaveManager�� ���� ����ġ �ݿ�
                SaveManager.Gold += monster.RewardGold;
                SaveManager.Experience += monster.RewardExp;

                // �� ����
                infoManager.UpdateInfo();

                // ��ư1 �ؽ�Ʈ�� �̺�Ʈ ����
                button1.gameObject.SetActive(true);
                SetButton(button1, "�ٽ� Ž���� �����Ѵ�.", () => StartDungeon(currentDungeon));

                yield break; // �ڷ�ƾ ����
            }

            // 0.7�� ���
            yield return new WaitForSeconds(0.7f);

            // �÷��̾�� ������ ������
            SaveManager.Hp -= monsterDamage;

            // ���� �α׿� ������ ���� �޽��� �߰�
            text.text += $"{monster.Name}�� ����! {monsterDamage}�� ���ظ� �Ծ���!\n";

            // �÷��̾� ��� üũ
            if (SaveManager.Hp <= 0)
            {
                // �÷��̾� ��� ó�� (�ļ� ó���� PlayerDead() �޼ҵ忡��)
                PlayerDead();

                yield break; // �ڷ�ƾ ����
            }

            // 0.7�� ���
            yield return new WaitForSeconds(0.7f);
        }

        // �� ����
        infoManager.UpdateInfo();
    }

    // �÷��̾��� ������ ��� �޼ҵ�
    private int CalculatePlayerDamage()
    {
        // �÷��̾��� ���ݷ� ���
        int playerStrength = SaveManager.Strength;
        int weaponIndex = SaveManager.Weapon;
        int damage = 0;

        if (weaponIndex >= 0 && weaponIndex < itemManager.weaponDatas.Length)
        {
            WeaponData weaponData = itemManager.weaponDatas[weaponIndex];
            int fixedIncrease = weaponData.fixedIncrease;
            float percentIncrease = weaponData.percentIncrease;

            damage = Mathf.RoundToInt((playerStrength * 2 + fixedIncrease) * percentIncrease);
        }

        // ���� ���� �߰� (+- 10%)
        float randomFactor = UnityEngine.Random.Range(0.9f, 1.1f);
        damage = Mathf.Max(1, Mathf.RoundToInt(damage * randomFactor));

        return damage;
    }

    // ������ ������ ��� �޼ҵ�
    private int CalculateMonsterDamage(Monster monster)
    {
        // ������ ���ݷ�
        int monsterAttackPower = monster.AttackPower;

        // �÷��̾��� ���� ���
        int playerAgility = SaveManager.Agility;
        int armorIndex = SaveManager.Armor;
        int totalDefense = 0;

        if (armorIndex >= 0 && armorIndex < itemManager.armorDatas.Length)
        {
            ArmorData armorData = itemManager.armorDatas[armorIndex];
            int fixedIncrease = armorData.fixedIncrease;
            float percentIncrease = armorData.percentIncrease;

            totalDefense = Mathf.RoundToInt((playerAgility * 2 + fixedIncrease) * percentIncrease);
        }

        // ���� ������ ���
        int damage = Mathf.Max(1, monsterAttackPower - totalDefense);

        // ���� ���� �߰� (+- 10%)
        float randomFactor = UnityEngine.Random.Range(0.9f, 1.1f);
        damage = Mathf.Max(1, Mathf.RoundToInt(damage * randomFactor));

        return damage;
    }

    // �÷��̾ �׾��� �� ȣ��Ǵ� �޼ҵ�
    private void PlayerDead()
    {
        // �÷��̾� ��� ó��
        // (�߰����� ���� ���� ȭ�� ǥ�� ���� ������ ���⿡ �߰��� �� �ֽ��ϴ�.)
        // ��: GameManager.Instance.GameOver();
    }


    // �޽�ó �̺�Ʈ�� ó���ϴ� �޼ҵ�
    private void RestEvent()
    {
        // "������ ��Ҹ� �߰��ߴ�. ��� �޽��ϸ� ü���� ������ �� ���� �� ����." ��� ���� ���
        this.text.text = "������ ��Ҹ� �߰��ߴ�. ��� �޽��ϸ� ü���� ������ �� ���� �� ����.";

        // ��ư1 �ؽ�Ʈ�� �̺�Ʈ ����
        SetButton(button1, "�޽��Ѵ�.", () => Rest());

        // ��ư2 �ؽ�Ʈ�� �̺�Ʈ ����
        SetButton(button2, "����ģ��.", () => StartDungeon(currentDungeon));

        // ��Ȱ��ȭ�� ��ư3�� ��ư4
        button3.gameObject.SetActive(false);
        button4.gameObject.SetActive(false);
    }

    // �޽��� �ϴ� �޼ҵ�
    private void Rest()
    {
        // SaveManager�� �̿��Ͽ� ü�� ȸ��
        int maxHp = SaveManager.Health * 3;
        SaveManager.Hp += 10;

        // �ִ� ü���� ���� �ʵ��� ����
        SaveManager.Hp = Mathf.Min(SaveManager.Hp, maxHp);

        // �޽� �� ExploreDungeon ����
        ExploreDungeon();
    }

    // ���� �̺�Ʈ�� ó���ϴ� �޼ҵ�
    private void TreasureEvent()
    {
        text.text = "���� ���ڸ� �߰��ߴ�! �����?";

        // ��ư1 �ؽ�Ʈ�� �̺�Ʈ ����
        SetButton(button1, "�����.", OpenTreasure);

        // ��ư2 �ؽ�Ʈ�� �̺�Ʈ ����
        SetButton(button2, "����ģ��.", () => StartDungeon(currentDungeon));
    }

    // ���� ���ڸ� �� �� ȣ��Ǵ� �޼ҵ�
    private void OpenTreasure()
    {
        int goldAmount = Random.Range(10, currentDungeon.treasure); // 10 ~ �ִ뺸�� ������ ������ ��� ȹ��

        // ��带 ���� �ؽ�Ʈ ���
        text.text = $"���� ���ڸ� ���� {goldAmount}��带 ȹ���ߴ�!";

        // SaveManager�� �̿��Ͽ� ��� ȹ��
        SaveManager.Gold += goldAmount;

        // �� ����
        infoManager.UpdateInfo();

        // ��ư1 �ؽ�Ʈ�� �̺�Ʈ ����
        SetButton(button1, "����ģ��.", () => StartDungeon(currentDungeon));
        button2.gameObject.SetActive(false);
    }

    // ������ ��ȯ�ϴ� �޼ҵ�
    public void BackToVillage()
    {
        progress = 0; // ���� ���൵ �ʱ�ȭ
        dungeonProgress.gameObject.SetActive(false); // ���� ������� ��Ÿ���� GUI ������Ʈ ����

        eventManager.ChangeEvent(0); // ������ ���ư���
    }
}
