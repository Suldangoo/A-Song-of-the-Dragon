using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoManager : MonoBehaviour
{
    #region �̱���
    public static InfoManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InfoManager>();
            }
            return instance;
        }
    }
    private static InfoManager instance;
    #endregion
    private ItemManager itemManager => ItemManager.Instance;

    [SerializeField] private Image[] characterImages; // ����, ���� ĳ���� �̹��� �迭 (0: ����, 1: ����)
    [SerializeField] private TMP_Text nickNameText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Image experienceFillImage;
    [SerializeField] private Image hpFillImage;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text strengthText;
    [SerializeField] private TMP_Text agilityText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text wisdomText;
    [SerializeField] private TMP_Text charmText;
    [SerializeField] private TMP_Text goldText;
    [SerializeField] private Image weaponImage; // ���� �̹���
    [SerializeField] private Image armorImage; // �� �̹���
    [SerializeField] public Sprite[] weaponImages; // ���� �̹��� �迭
    [SerializeField] public Sprite[] armorImages; // �� �̹��� �迭
    [SerializeField] private TMP_Text atkText;
    [SerializeField] private TMP_Text defText;
    [SerializeField] private TMP_Text smallHpPotionText;
    [SerializeField] private TMP_Text largeHpPotionText;

    private void Start()
    {
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        // 1. ĳ���� ����
        bool isMale = SaveManager.Gender;
        characterImages[0].gameObject.SetActive(isMale); // ���� ĳ���� �̹���
        characterImages[1].gameObject.SetActive(!isMale); // ���� ĳ���� �̹���

        // 2. ĳ���� �г���
        nickNameText.text = SaveManager.NickName;

        // 3. ĳ���� ����
        levelText.text = $"Level: {SaveManager.Level}";

        // 4. ĳ���� ����ġ
        int maxExperience = 5 + SaveManager.Level * 5;
        float fillAmount = (float)SaveManager.Experience / maxExperience;
        experienceFillImage.fillAmount = fillAmount;

        // 5. ĳ���� HP
        int maxHp = SaveManager.Health * 3;
        fillAmount = SaveManager.Hp / (float)maxHp;
        hpFillImage.fillAmount = fillAmount;
        hpText.text = $"{SaveManager.Hp}/{maxHp}";

        // 6. ĳ���� �������ͽ�
        strengthText.text = SaveManager.Strength.ToString();
        agilityText.text = SaveManager.Agility.ToString();
        healthText.text = SaveManager.Health.ToString();
        wisdomText.text = SaveManager.Wisdom.ToString();
        charmText.text = SaveManager.Charm.ToString();

        // 7. ���
        goldText.text = SaveManager.Gold.ToString();

        // 8. ����
        int weaponIndex = SaveManager.Weapon;
        if (weaponIndex >= 0 && weaponIndex < weaponImages.Length)
        {
            weaponImage.sprite = weaponImages[weaponIndex];
        }

        // 9. ��
        int armorIndex = SaveManager.Armor;
        if (armorIndex >= 0 && armorIndex < armorImages.Length)
        {
            armorImage.sprite = armorImages[armorIndex];
        }

        // 10. ���ݷ� / ����
        UpdateAttackAndDefense();

        // 11. ����
        smallHpPotionText.text = SaveManager.SmallHpPotion.ToString();
        largeHpPotionText.text = SaveManager.LargeHpPotion.ToString();
    }

    public void UpdateAttackAndDefense()
    {
        // ���ݷ� ���
        int playerStrength = SaveManager.Strength;
        int weaponIndex = SaveManager.Weapon;
        if (weaponIndex >= 0 && weaponIndex < itemManager.weaponDatas.Length)
        {
            WeaponData weaponData = itemManager.weaponDatas[weaponIndex];
            int fixedIncrease = weaponData.fixedIncrease;
            float percentIncrease = weaponData.percentIncrease;

            int totalAttack = Mathf.RoundToInt((playerStrength * 2 + fixedIncrease) * percentIncrease);
            atkText.text = $"���ݷ�: {totalAttack}";
        }

        // ���� ���
        int playerAgility = SaveManager.Agility;
        int armorIndex = SaveManager.Armor;
        if (armorIndex >= 0 && armorIndex < itemManager.armorDatas.Length)
        {
            ArmorData armorData = itemManager.armorDatas[armorIndex];
            int fixedIncrease = armorData.fixedIncrease;
            float percentIncrease = armorData.percentIncrease;

            int totalDefense = Mathf.RoundToInt((playerAgility * 2 + fixedIncrease) * percentIncrease);
            defText.text = $"����: {totalDefense}";
        }
    }
}
