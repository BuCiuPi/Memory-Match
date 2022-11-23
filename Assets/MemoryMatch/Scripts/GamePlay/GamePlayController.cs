using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{



    private List<IItemCard> _answer;

    [Header("Event Listener")]
    [SerializeField] private CardEventChannel _onSelectCardEvent;
    [SerializeField] private IntEventChannel _onSetGameState;

    [Header("Event Raiser")]
    [SerializeField] private VoidEventChannel _PlusTotalMoving;
    [SerializeField] private VoidEventChannel _PlusMatchedCard;



    private bool m_isAnswerChecking;

    public void Init()
    {
        _answer = new List<IItemCard>();
    }

    void OnEnable()
    {
        if (_onSelectCardEvent) _onSelectCardEvent.OnEventRaised += OnItemSelect;
    }

    void OnDisable()
    {
        if (_onSelectCardEvent) _onSelectCardEvent.OnEventRaised -= OnItemSelect;
    }

    public void OnItemSelect(IItemCard itemInfo)
    {
        if (m_isAnswerChecking) return;
        itemInfo.OnSelectSuccess();
        _answer.Add(itemInfo);
        _PlusTotalMoving?.RaiseEvent();

        StartCoroutine(CheckAnswerCo());
    }
    private IEnumerator CheckAnswerCo()
    {
        yield return new WaitForSeconds(1f);

        int numberOfSelectItem = _answer.Count;
        Debug.Log("Item Selected: " + numberOfSelectItem);
        if (numberOfSelectItem < 2) yield break;
        m_isAnswerChecking = true;

        bool answerResult = CompareAnswer(numberOfSelectItem);
        UpdateScore(answerResult);
        UpdateAnswerUI(answerResult);

        Debug.Log($"Answer result: {answerResult}");

        m_isAnswerChecking = false;

    }

    private bool CompareAnswer(int numberOfSelectItem)
    {
        
        IItemCard firstAnswer = _answer?[0];
        for (int i = 1; i < numberOfSelectItem; i++)
        {
            if (!firstAnswer.IsDifference(_answer[i]))
            {
                if (AudioController.Ins)
                    AudioController.Ins.PlaySound("SE_Wrong");

                return false;
            }
        }


        if (AudioController.Ins)
            AudioController.Ins.PlaySound("SE_Right");

        return true;
    }

    public void UpdateScore(bool result)
    {

        if (result)
        {
            _PlusMatchedCard?.RaiseEvent();
        }
    }
    public void UpdateAnswerUI(bool result)
    {
        if (result)
        {
            foreach (var item in _answer)
            {
                item.OnResultSuccess();
            }
        }
        else
        {
            foreach (var item in _answer)
            {
                item.OnResultFail();
            }
        }

        _answer.Clear();
    }
}
