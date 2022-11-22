using System.Collections.Generic;
using UnityEngine;

public class CardInventoryController : MonoBehaviour
{
    [SerializeField] private MatchItem[] matchItems;
    [SerializeField] private MatchItemUI _itemPrefab;
    [SerializeField] private Transform _itemContainer;

    private List<MatchItem> _matchItemsCopy;
    private List<MatchItemUI> _matchItemUIs;

    private int _totalMatchItem;

    public int Init()
    {
        _matchItemsCopy = new List<MatchItem>();
        _matchItemUIs = new List<MatchItemUI>();
        _totalMatchItem = matchItems.Length;

        GenerateMatchItems();

        return _totalMatchItem;
    }

    private void GenerateMatchItems()
    {

        if (matchItems == null || matchItems.Length == 0 || _itemPrefab == null || _itemContainer == null) { return; }
        int totalItem = matchItems.Length;
        int divItem = totalItem % 2;
        _totalMatchItem = totalItem - divItem;

        for (int i = 0; i < _totalMatchItem; i++)
        {

            var matchItem = matchItems[i];
            if (matchItem != null)
            {
                matchItem.Id = i;
            }
        }

        _matchItemsCopy.AddRange(matchItems);
        _matchItemsCopy.AddRange(matchItems);

        ShuffleMatchItem();
        ClearGrid();

        foreach (var itemData in _matchItemsCopy)
        {
            CreateItemCard(itemData);
        }

    }

    private void CreateItemCard(MatchItem itemData)
    {
        var matchItemUIClone = Instantiate(_itemPrefab, Vector3.zero, Quaternion.identity);

        matchItemUIClone.name = "Item : " + itemData.Id.ToString();
        matchItemUIClone.transform.SetParent(_itemContainer);
        matchItemUIClone.transform.localScale = Vector3.one;
        matchItemUIClone.transform.localPosition = Vector3.zero;

        matchItemUIClone.Init(itemData);

        _matchItemUIs.Add(matchItemUIClone);
    }

    private void ShuffleMatchItem()
    {
        if (_matchItemsCopy == null && _matchItemsCopy.Count <= 0) return;
        for (int i = 0; i < _matchItemsCopy.Count; i++)
        {
            var temp = _matchItemsCopy[i];
            if (temp != null)
            {
                int randIdx = Random.Range(0, _matchItemsCopy.Count);
                _matchItemsCopy[i] = _matchItemsCopy[randIdx];
                _matchItemsCopy[randIdx] = temp;
            }
        }
    }

    private void ClearGrid()
    {
        if (_itemContainer)
        {
            for (int i = 0; i < _itemContainer.childCount; i++)
            {
                var child = _itemContainer.GetChild(i);
                if (child)
                {
                    Destroy(child.gameObject);
                }
            }

        }
    }

}
