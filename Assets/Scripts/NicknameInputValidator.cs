using TMPro;
using UnityEngine;

public class NicknameInputValidator : MonoBehaviour
{
    [SerializeField] private TMP_InputField nicknameInputField;
    private const int maxCharacterCount = 6;

    private void Start()
    {
        if (nicknameInputField != null)
        {
            // �Է��ʵ忡 �̺�Ʈ ������ ���
            nicknameInputField.onValueChanged.AddListener(OnNicknameValueChanged);
        }
    }

    private void OnNicknameValueChanged(string input)
    {
        if (input.Length > maxCharacterCount)
        {
            // �Է��� 6���ڸ� �ʰ��ϸ� �Է� �ʵ��� ���� �ڸ���
            nicknameInputField.text = input.Substring(0, maxCharacterCount);
        }
    }
}
