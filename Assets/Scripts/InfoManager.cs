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
    [SerializeField] private Image[] weaponImages; // ���� �̹��� �迭
    [SerializeField] private Image[] armorImages; // �� �̹��� �迭
    [SerializeField] private TMP_Text smallHpPotionText;
    [SerializeField] private TMP_Text largeHpPotionText;

    private void Update()
    {
        UpdateInfo();
    }

    private void UpdateInfo()
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
        int maxExperience = SaveManager.Level * 5;
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
            weaponImages[weaponIndex].gameObject.SetActive(true);
        }

        // 9. ��
        int armorIndex = SaveManager.Armor;
        if (armorIndex >= 0 && armorIndex < armorImages.Length)
        {
            armorImages[armorIndex].gameObject.SetActive(true);
        }

        // 10. ����
        smallHpPotionText.text = SaveManager.SmallHpPotion.ToString();
        largeHpPotionText.text = SaveManager.LargeHpPotion.ToString();
    }
}
