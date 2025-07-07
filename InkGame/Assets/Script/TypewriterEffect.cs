using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textDisplay; //�Ψ���ܥ��r�ʵe��r�� TextMeshPro ����

    private float waitingSeconds; //// �C�Ӧr����ܪ����j��ơA����r�t��
    private Coroutine typingCoroutine; // �Ψ��x�s���r�ʵe�� Coroutine�A��K����r�L�{
    private bool isTyping; // �ΨӼаO�O�_���b���r

    public void StartTyping(string text, float speed) // ��l�ƥ��r�ʵe
    {
        waitingSeconds = speed; // �C�Ӧr����ܪ����j���
        if (typingCoroutine != null) // �p�G�w�g�����r�ʵe�b�i��A�h���
        {
            StopCoroutine(typingCoroutine);
        }
    
        typingCoroutine = StartCoroutine(TypeLine(text)); // �}�l�s�����r�ʵe
    }

    private IEnumerator TypeLine(string text) // �}�l���r�ʵe����{ 
                                              // IEnumerator �O C# ���u���N�������v�A�쥻�ΨӤ@�Ӥ@�Ө��X��ơ]�Ҧp�}�C�βM��^�C
                                              // �����S�ʬO�G�i�H�O��W�����檺��m�A�C������ɱq�����~��]�z�L MoveNext / Current�^�C
                                              // Unity �Q�ΤF�o�ӯS�ʡA���ڭ̥i�H�g�X�u��{ Coroutine�v�X�X ���椤���Ȱ��B���ݡB�M��A�~���޿�C
                                              // �ҥH�b Unity ���ڭ̥� IEnumerator �ӹ�{���u���r���ĪG�v�B�u���ݴX��A�i��U�@�B�v�o���\��C
                                              // IEnumerator = ���� + �۰ʱ��� + ���~�Ȱ���@�q�@�q����v
    {
        isTyping = true; // �аO�����b���r
        textDisplay.text = text; // �]�w�n��ܪ���r
        textDisplay.maxVisibleCharacters = 0; // ��l�Ʈɤ���ܥ���r��

        for (int i = 0; i <= text.Length; i++) // �`���C�Ӧr��
                                               // �o�̪� i �N��ثe�n��ܪ��r���ƶq
                                               // �C���`�����|�W�[�@�Ӧr����i���r���ƶq��
        {
            textDisplay.maxVisibleCharacters = i ; // ��ܥثe���r���ƶq
            yield return new WaitForSeconds(waitingSeconds);  // ���ݫ��w���ɶ�
        }
        isTyping = false; 
    }
    public void CompleteLine() // �������r�ʵe�A��ܩҦ��r��
    {
        if (typingCoroutine != null) // �p�G�����r�ʵe���b�i��A�h���
        {
            StopCoroutine(typingCoroutine);  

        }

        textDisplay.maxVisibleCharacters = textDisplay.text.Length; // ��ܩҦ��r��
        isTyping = false; 
    }
    public bool IsTyping() => isTyping; // �ˬd�O�_���b���r
}