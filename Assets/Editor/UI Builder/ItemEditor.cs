using System;
using System.Collections.Generic;
using System.Linq;
using Inventory.DataSO;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Utilities.Parms;


public class ItemEditor : EditorWindow
{
    private ItemDataList_SO _dataBase;
    private List<ItemDetails> _itemList = new List<ItemDetails>();
    private VisualTreeAsset _itemRowTemplate;
    private ScrollView _itemDetailsSection;
    private ItemDetails _activeItem;

    //默认预览图片
    private Sprite _defaultIcon;

    private VisualElement _iconPreview;
    //获得VisualElement
    private ListView _itemListView;

    [MenuItem("GameEditor/ItemEditor")]
    public static void ShowExample()
    {
        ItemEditor wnd = GetWindow<ItemEditor>();
        wnd.titleContent = new GUIContent("ItemEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        // VisualElement label = new Label("Hello World! From C#");
        // root.Add(label);

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UI Builder/ItemEditor.uxml");
        VisualElement labelFromUxml = visualTree.Instantiate();
        root.Add(labelFromUxml);

        //拿到模版数据
        _itemRowTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UI Builder/ItemRowTemplate.uxml");

        //拿默认Icon图片
        _defaultIcon = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/M Studio/Art/Items/Icons/icon_M.png");

        //变量赋值
        _itemListView = root.Q<VisualElement>("ItemList").Q<ListView>("ListView");
        _itemDetailsSection = root.Q<ScrollView>("ItemDetails");
        _iconPreview = _itemDetailsSection.Q<VisualElement>("Icon");


        //获得按键
        root.Q<Button>("AddButton").clicked += OnAddItemClicked;
        root.Q<Button>("DeleteButton").clicked += OnDeleteClicked;
        //加载数据
        LoadDataBase();

        //生成ListView
        GenerateListView();
    }

    #region 按键事件
    private void OnDeleteClicked()
    {
        _itemList.Remove(_activeItem);
        _itemListView.Rebuild();
        _itemDetailsSection.visible = false;
    }

    private void OnAddItemClicked()
    {
        ItemDetails newItem = new ItemDetails();
        newItem.itemName = "NEW ITEM";
        newItem.itemID = 1001 + _itemList.Count;
        _itemList.Add(newItem);
        _itemListView.Rebuild();
    }
    #endregion

    private void LoadDataBase()
    {
        var dataArray = AssetDatabase.FindAssets("ItemDataList_SO");

        if (dataArray.Length > 1)
        {
            var path = AssetDatabase.GUIDToAssetPath(dataArray[0]);
            _dataBase = AssetDatabase.LoadAssetAtPath(path, typeof(ItemDataList_SO)) as ItemDataList_SO;
        }

        _itemList = _dataBase.itemDetailsList;
        //如果不标记则无法保存数据
        EditorUtility.SetDirty(_dataBase);
        // Debug.Log(itemList[0].itemID);
    }

    private void GenerateListView()
    {
        Func<VisualElement> makeItem = () => _itemRowTemplate.CloneTree();

        Action<VisualElement, int> bindItem = (e, i) =>
        {
            if (i < _itemList.Count)
            {
                if (_itemList[i].icon != null)
                    e.Q<VisualElement>("Icon").style.backgroundImage = _itemList[i].icon.texture;
                e.Q<Label>("Name").text = _itemList[i] == null ? "NO ITEM" : _itemList[i].itemName;
            }
        };

        _itemListView.fixedItemHeight = 50;  //根据需要高度调整数值
        _itemListView.itemsSource = _itemList;
        _itemListView.makeItem = makeItem;
        _itemListView.bindItem = bindItem;

        _itemListView.onSelectionChange += OnListSelectionChange;

        //右侧信息面板不可见
        _itemDetailsSection.visible = false;
    }

    private void OnListSelectionChange(IEnumerable<object> selectedItem)
    {
        _activeItem = (ItemDetails)selectedItem.First();
        GetItemDetails();
        _itemDetailsSection.visible = true;
    }

    private void GetItemDetails()
    {
        _itemDetailsSection.MarkDirtyRepaint();

        _itemDetailsSection.Q<IntegerField>("ItemID").value = _activeItem.itemID;
        _itemDetailsSection.Q<IntegerField>("ItemID").RegisterValueChangedCallback(evt =>
        {
            _activeItem.itemID = evt.newValue;
        });

        _itemDetailsSection.Q<TextField>("ItemName").value = _activeItem.itemName;
        _itemDetailsSection.Q<TextField>("ItemName").RegisterValueChangedCallback(evt =>
        {
            _activeItem.itemName = evt.newValue;
            _itemListView.Rebuild();
        });

        _iconPreview.style.backgroundImage = _activeItem.icon == null ? _defaultIcon.texture : _activeItem.icon.texture;
        _itemDetailsSection.Q<ObjectField>("ItemIcon").value = _activeItem.icon;
        _itemDetailsSection.Q<ObjectField>("ItemIcon").RegisterValueChangedCallback(evt =>
        {
            Sprite newIcon = evt.newValue as Sprite;
            _activeItem.icon = newIcon;

            _iconPreview.style.backgroundImage = newIcon == null ? _defaultIcon.texture : newIcon.texture;
            _itemListView.Rebuild();
        });

        //其他所有变量的绑定
        _itemDetailsSection.Q<ObjectField>("ItemSprite").value = _activeItem.onWorldIcon;
        _itemDetailsSection.Q<ObjectField>("ItemSprite").RegisterValueChangedCallback(evt =>
        {
            _activeItem.onWorldIcon = (Sprite)evt.newValue;
        });

        _itemDetailsSection.Q<EnumField>("ItemType").Init(_activeItem.itemType);
        _itemDetailsSection.Q<EnumField>("ItemType").value = _activeItem.itemType;
        _itemDetailsSection.Q<EnumField>("ItemType").RegisterValueChangedCallback(evt =>
        {
            _activeItem.itemType = (ItemType)evt.newValue;
        });

        _itemDetailsSection.Q<TextField>("Description").value = _activeItem.description;
        _itemDetailsSection.Q<TextField>("Description").RegisterValueChangedCallback(evt =>
        {
            _activeItem.description = evt.newValue;
        });

        _itemDetailsSection.Q<IntegerField>("ItemUseRadius").value = _activeItem.useRadius;
        _itemDetailsSection.Q<IntegerField>("ItemUseRadius").RegisterValueChangedCallback(evt =>
        {
            _activeItem.useRadius = evt.newValue;
        });

        _itemDetailsSection.Q<Toggle>("CanPickedup").value = _activeItem.canPickedUp;
        _itemDetailsSection.Q<Toggle>("CanPickedup").RegisterValueChangedCallback(evt =>
        {
            _activeItem.canPickedUp = evt.newValue;
        });

        _itemDetailsSection.Q<Toggle>("CanDropped").value = _activeItem.canDropped;
        _itemDetailsSection.Q<Toggle>("CanDropped").RegisterValueChangedCallback(evt =>
        {
            _activeItem.canDropped = evt.newValue;
        });

        _itemDetailsSection.Q<Toggle>("CanCarried").value = _activeItem.canCarried;
        _itemDetailsSection.Q<Toggle>("CanCarried").RegisterValueChangedCallback(evt =>
        {
            _activeItem.canCarried = evt.newValue;
        });

        _itemDetailsSection.Q<IntegerField>("Price").value = _activeItem.price;
        _itemDetailsSection.Q<IntegerField>("Price").RegisterValueChangedCallback(evt =>
        {
            _activeItem.price = evt.newValue;
        });

        _itemDetailsSection.Q<Slider>("SellPercentage").value = _activeItem.sellPercentage;
        _itemDetailsSection.Q<Slider>("SellPercentage").RegisterValueChangedCallback(evt =>
        {
            _activeItem.sellPercentage = evt.newValue;
        });
    }
}