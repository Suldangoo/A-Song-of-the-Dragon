using System.Collections;
using TMPro;
using UnityEngine;

public class TextBlink : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    private bool isTextVisible = true;

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        StartCoroutine(BlinkText());
    }

    private IEnumerator BlinkText()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // 1�ʸ��� �Ѱ� ����
            isTextVisible = !isTextVisible;
            textMeshPro.enabled = isTextVisible;
        }
    }
}