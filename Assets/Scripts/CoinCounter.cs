using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private Transform _counterTransform;
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private AnimationCurve _scaleCurve;
    private Progress _progress;
    
    private PermanentProgress _permanentProgress;

    public float CoinsInGame;

    // ��� ����� ������ ���� float. ������ ��� �� ���������� ���� ����� ��������� ������� �������
    // ���� �� ����� int �� �������� 4 + 4% ����� ����� 4 � ������ �� ����������
    public float NumberInLevel { get; private set; }
    private void Start()
    {
        CoinsInGame = ProgressGame.Instance.Coins;
        Display();
    }
    public void Init(Progress progress, PermanentProgress permanentProgress)
    {
        _progress = progress;
        _permanentProgress = permanentProgress;
        Display();
    }
    public void AddCoins(int number)
    {
        NumberInLevel += number * (1 + _permanentProgress.GetLoot());
        CoinsInGame += number;

        Display();
        StartCoroutine(CounterAnimation());
        

    }



    private IEnumerator CounterAnimation()
    {
        for (float t = 0; t < 1f; t += Time.unscaledDeltaTime * 2f)
        {
            float scale = _scaleCurve.Evaluate(t);
            _counterTransform.localScale = Vector3.one * scale;
            yield return null;
        }
        _counterTransform.localScale = Vector3.one;
    }

    public void SpendCoins(int value)
    {
        _progress.ProgressData.Coins -= value;
        Display();
    }

    void Display()
    {
        //int totalNumber = _progress.ProgressData.Coins + Mathf.RoundToInt(NumberInLevel);
        _coinsText.text = CoinsInGame.ToString(); //totalNumber.ToString();

    }
    public void SaveToProgress()
    {
        ProgressGame.Instance.Coins = CoinsInGame;
    }

}
