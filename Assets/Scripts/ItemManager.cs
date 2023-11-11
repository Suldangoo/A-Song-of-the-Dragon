using UnityEngine;

[System.Serializable]
public class WeaponData
{
    public string name; // ���� �̸�
    public int fixedIncrease; // ���� ��� ��ġ
    public float percentIncrease; // �ۼ�Ʈ ��� ��ġ
    [TextArea(2, 5)] public string description; // ���� ���� (2�� �̻�, 5�� ����)
}

[System.Serializable]
public class ArmorData
{
    public string name; // �� �̸�
    public int fixedIncrease; // ���� ��� ��ġ
    public float percentIncrease; // �ۼ�Ʈ ��� ��ġ
    [TextArea(2, 5)] public string description; // �� ���� (2�� �̻�, 5�� ����)
}

public class ItemManager : MonoBehaviour
{
    #region �̱���
    public static ItemManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ItemManager>();
            }
            return instance;
        }
    }
    private static ItemManager instance;
    #endregion

    [SerializeField] private WeaponData[] weaponDatas; // ���� ������ �迭
    [SerializeField] private ArmorData[] armorDatas; // �� ������ �迭
}