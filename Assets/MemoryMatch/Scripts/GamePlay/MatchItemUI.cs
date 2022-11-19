using System.Text.RegularExpressions;
using UnityEngine.UI;
using UnityEngine;
using System;

[System.Serializable]
public class MatchItemUI : MonoBehaviour, IItemCard
{
    [Header("Item Sprite")]
    [SerializeField] private Sprite _frame;
    [SerializeField] private Sprite _background;

    [Header("Item Field")]
    [SerializeField] private Image _imgBg;
    [SerializeField] private Image _imgIcon;
    [SerializeField] private Button _btnInspect;
    [SerializeField] private Animator _anim;

    [Header("Event Raised")]
    [SerializeField] private CardEventChannel OnCardClick;

    private ItemInfo _itemInfo;

    public void Init(MatchItem itemData)
    {
        if (_imgBg != null)
        {
            _imgBg.sprite = _background;
        }
        if (_imgIcon != null)
        {
            _imgIcon.sprite = itemData.icon;
            _imgIcon.gameObject.SetActive(false);
        }

        _itemInfo = new ItemInfo(itemData);
    }

    public void OnPointerClick()
    {
        //event
        OnCardClick?.RaiseEvent(this);
    }

    private void SetInteractable(bool isInteractable)
    {
        _btnInspect.interactable = isInteractable;
    }

    private void SetOpenState(bool isShow)
    {
        _itemInfo.IsOpened = isShow;
    }

    public void ChangeState()
    {
        SetBackground(_itemInfo.IsOpened);
        ShowIcon(_itemInfo.IsOpened);
    }
    private void SetBackground(bool isShow)
    {
        _imgBg.sprite = isShow ? _frame : _background;
    }

    private void ShowIcon(bool isShow)
    {
        _imgIcon.gameObject.SetActive(isShow);
    }

    private void OpenAnimTrigger()
    {
        _anim.SetBool(AnimState.Flip.ToString(), true);
    }

    public void ExplodeAnimTrigger()
    {
        _anim.SetBool(AnimState.Explode.ToString(), true);
    }

    public void BackToIdle()
    {
        if (_anim)
        {
            _anim.SetBool(AnimState.Flip.ToString(), false);
        }
        SetInteractable(!_itemInfo.IsOpened);
    }

    public bool IsDifference(IItemCard itemInfo)
    {
        var targetItemInfo = (itemInfo as MatchItemUI)._itemInfo; 
        return this._itemInfo.Data.Id == targetItemInfo.Data.Id;
    }

    public void OnResultFail()
    {
        // animation
        OpenAnimTrigger();
        Debug.Log("isCalledbackToIdle");
        SetOpenState(!_itemInfo.IsOpened);
    }

    public void OnResultSuccess()
    {
        ExplodeAnimTrigger();
    }

    public void OnSelectSuccess()
    {
        SetOpenState(!_itemInfo.IsOpened);
        SetInteractable(!_itemInfo.IsOpened);

        // Animmation
        OpenAnimTrigger();
    }
}

public class ItemInfo
{
    public MatchItem Data;
    public bool IsOpened;
    public ItemInfo(MatchItem itemData)
    {
        Data = itemData;
    }
}

public interface IItemCard
{
    public bool IsDifference(IItemCard itemInfo);
    public void OnSelectSuccess();
    public void OnResultFail();
    public void OnResultSuccess();
}
