/* * * * * * * * * * * * * 
 * 작성자: 윤정도
 * 생성일: 11/17/2022 4:36:19 PM
 *
 * * * * * * * * * * * * *
 * 옛날에 코딩 처음 배웠을 때 cmd에서 tree 명령처럼 콘솔창에 출력하는 프로그램을
 * 만들어보고 싶었는데... 이번에 생각난김에 만듬
 *
 */

using System.Collections;

using CTree.Internal;

namespace CTree;

/*
 *     BridgeLength
 *       <---->
 *      ├────── <CListItem>  
 *      │         ├──────── <CListItem> 
 *      │         ├──────── <CListItem> 
 *      │         └──────── <CListItem> 
 *      │
 *      └─── <CListItem> 
 *            └──────── <CListItem> 
 *        
 * 
 * - Fold: true시 자식 항목 출력안됨
 * - Dummy: 해당 아이템을 비어있는 것처럼 처리함
 * - Tag: 사용자 지정 값을 넣을 수 있도록 함
 * - Name: 출력시 보일 이름
 * - BridgeLength: ──── 이거 길이
 * - Count: 자식 갯수
 * - ForegroundColor: 해당 아이템의 전경 색상만 변경
 * - BackgroundColor: 해당 아이템의 배경 색상만 변경
 *    => 초기 검정 색상으로 설정되어있는데 디폴트 값으로 처리함. 실제로 검정색으로 출력되는게 아님
 */
public class ConsoleTreeItem : IList<ConsoleTreeItem>
{
    private readonly List<ConsoleTreeItem> _items;
    public ConsoleTreeItem? Parent { get; private set; }
    public bool Fold { get; set; }
    public int Count => _items.Count;
    public bool Dummy { get; set; }
    public string Name { get; set; }
    public object? Tag { get; set; }
    public ConsoleColor ForegroundColor { get; set; } = ConsoleColor.Black;
    public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.Black;
    public bool IsReadOnly => false;
    public int BridgeLength { get; set; } = DefaultBridgeLength;

    public static int DefaultBridgeLength = 0;


    private static readonly ConsoleTreeItem s_dummy = new() { Dummy = true };

    public ConsoleTreeItem this[int index]
    {
        get => _items[index];
        set => _items[index] = value;
    }

    public ConsoleTreeItem()
    {
        Name = string.Empty;
        _items = new List<ConsoleTreeItem>();
    }

    public ConsoleTreeItem(string name)
    {
        Name = name;
        _items = new List<ConsoleTreeItem>();
    }

    public ConsoleTreeItem(string name, object tag)
    {
        Name = name;
        Tag = tag;
        _items = new List<ConsoleTreeItem>();
    }


    public void Add(ConsoleTreeItem item)
    {
        item.Parent = this;
        _items.Add(item);
    }

    public void Add(params ConsoleTreeItem[] items)
    {
        items.ForEach(x => x.Parent = this);
        _items.AddRange(items);
    }

    public void AddDummy(int count = 1)
    {
        for (int i = 0; i < count; i++)
            _items.Add(s_dummy);
    }

    public void Clear() => _items.Clear();
    public bool Contains(ConsoleTreeItem item) => _items.Contains(item);
    public void CopyTo(ConsoleTreeItem[] array, int arrayIndex) => _items.CopyTo(array, arrayIndex);
    public bool Remove(ConsoleTreeItem item) => _items.Remove(item);
    public int IndexOf(ConsoleTreeItem item) => _items.IndexOf(item);
    public void Insert(int index, ConsoleTreeItem item) => _items.Insert(index, item);
    public void RemoveAt(int index) => _items.RemoveAt(index);
    public ConsoleTreeItem? Find(Predicate<ConsoleTreeItem> predicate) => _items.Find(predicate);

    public ConsoleTreeItem? Find(Predicate<ConsoleTreeItem> predicate, out int index)
    {
        index = -1;

        for (var i = 0; i < _items.Count; i++)
        {
            if (!predicate(_items[i])) continue;
            index = i;
            return _items[i];
        }

        return null;
    }

    public ConsoleTreeItem? FindLast(Predicate<ConsoleTreeItem> predicate) => _items.FindLast(predicate);

    public ConsoleTreeItem? FindLast(Predicate<ConsoleTreeItem> predicate, out int index)
    {
        index = -1;

        for (var i = _items.Count - 1; i >= 0; i--)
        {
            if (!predicate(_items[i])) continue;
            index = i;
            return _items[i];
        }

        return null;
    }

    public IEnumerator<ConsoleTreeItem> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
