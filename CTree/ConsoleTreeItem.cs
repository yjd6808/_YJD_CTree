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
using System.Runtime.CompilerServices;
using System.Text;

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
 * - Count: 자기를 제외한 자식의 수
 * - CountRecursive: 자기를 제외한 서브트리 자식들까지 포함한 수
 */
public class ConsoleTreeItem : IList<ConsoleTreeItem>, ICloneable
{
    private List<ConsoleTreeItem> Items { get; }
    public ConsoleTreeItem? Parent { get; set; }
    public bool Fold { get; set; }
    public int Count => Items.Count;                
    public int CountRecursive => CountOf(this);
    public bool Dummy { get; set; }
    public string Name { get; set; }
    public object? Tag { get; set; }
    public ConsoleColor ForegroundColor { get; set; } = ConsoleColor.Black;
    public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.Black;
    public bool IsReadOnly => false;
    public int BridgeLength { get; set; } = DefaultBridgeLength;

    public static int DefaultBridgeLength = 0;


    public ConsoleTreeItem this[int index]
    {
        get => Items[index];
        set => Items[index] = value;
    }

    public ConsoleTreeItem()
    {
        Name = string.Empty;
        Items = new List<ConsoleTreeItem>();
    }

    public ConsoleTreeItem(string name)
    {
        Name = name;
        Items = new List<ConsoleTreeItem>();
    }

    public ConsoleTreeItem(string name, object tag)
    {
        Name = name;
        Tag = tag;
        Items = new List<ConsoleTreeItem>();
    }

    public ConsoleTreeItem(string name, object tag, List<ConsoleTreeItem> items)
    {
        Name = name;
        Tag = tag;
        Items = items;
    }

    public ConsoleTreeItem Add(ConsoleTreeItem item)
    {
        item.Parent = this;
        Items.Add(item);
        return this;
    }

    public ConsoleTreeItem Add(string name)
    {
        var childItem = new ConsoleTreeItem(name) { Parent = this };
        Items.Add(childItem);
        return this;
    }

    public ConsoleTreeItem Add(string name, object tag)
    {
        var childItem = new ConsoleTreeItem(name, tag) { Parent = this };
        Items.Add(childItem);
        return this;
    }

    public ConsoleTreeItem Add(params ConsoleTreeItem[] items)
    {
        items.ForEach(x => x.Parent = this);
        Items.AddRange(items);
        return this;
    }

    public ConsoleTreeItem AddDummy(int count = 1)
    {
        for (int i = 0; i < count; i++)
            Items.Add(new ConsoleTreeItem() { Dummy = true });

        return this;
    }

    public ConsoleTreeItem AddReturnChild(ConsoleTreeItem item)
    {
        item.Parent = this;
        Items.Add(item);
        return item;
    }

    public ConsoleTreeItem AddReturnChild(string name)
    {
        var childItem = new ConsoleTreeItem(name) { Parent = this };
        Items.Add(childItem);
        return childItem;
    }

    public ConsoleTreeItem AddReturnChild(string name, object tag)
    {
        var childItem = new ConsoleTreeItem(name, tag) { Parent = this };
        Items.Add(childItem);
        return childItem;
    }

    public ConsoleTreeItem AddReturnParent(string name)
    {
        var childItem = new ConsoleTreeItem(name) { Parent = this };
        Items.Add(childItem);
        return Parent!;
    }

    public ConsoleTreeItem AddReturnParent(string name, object tag)
    {
        var childItem = new ConsoleTreeItem(name, tag) { Parent = this };
        Items.Add(childItem);
        return Parent!;
    }

   
    public static int CountOf(ConsoleTreeItem item)
    {
        int count = 0;
        count += item.Items.Count;
        item.ForEach(x => count += CountOf(x));
        return count;
    }

    public void Clear() => Items.Clear();
    public bool Contains(ConsoleTreeItem item) => Items.Contains(item);
    public void CopyTo(ConsoleTreeItem[] array, int arrayIndex) => Items.CopyTo(array, arrayIndex);
    public bool Remove(ConsoleTreeItem item)
    {
        bool childRemoved = Items.Remove(item);
        if (childRemoved)
            item.Parent = null;
        return childRemoved;
    }

    public int IndexOf(ConsoleTreeItem item) => Items.IndexOf(item);

    public void Insert(int index, ConsoleTreeItem item)
    {
        item.Parent = this;
        Items.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        Items.RemoveAt(index);
        Items[index].Parent = null;
    }
    public ConsoleTreeItem? Find(Predicate<ConsoleTreeItem> predicate) => Items.Find(predicate);

    public ConsoleTreeItem? Find(Predicate<ConsoleTreeItem> predicate, out int index)
    {
        index = -1;

        for (var i = 0; i < Items.Count; i++)
        {
            if (!predicate(Items[i])) continue;
            index = i;
            return Items[i];
        }

        return null;
    }

    public ConsoleTreeItem? FindLast(Predicate<ConsoleTreeItem> predicate) => Items.FindLast(predicate);

    public ConsoleTreeItem? FindLast(Predicate<ConsoleTreeItem> predicate, out int index)
    {
        index = -1;

        for (var i = Items.Count - 1; i >= 0; i--)
        {
            if (!predicate(Items[i])) continue;
            index = i;
            return Items[i];
        }

        return null;
    }

    public IEnumerator<ConsoleTreeItem> GetEnumerator()
    {
        return Items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    void ICollection<ConsoleTreeItem>.Add(ConsoleTreeItem item) => throw new NotImplementedException();

    public object Clone()
    {
        object? childCloneableTag = Tag;

        if (Tag is ICloneable childCloneable)
            childCloneableTag = childCloneable.Clone();

        return new ConsoleTreeItem(Name, childCloneableTag!, CloneChildren(this));
    }

    private static List<ConsoleTreeItem> CloneChildren(ConsoleTreeItem parent)
    {
        return parent.Select(item => (item.Clone() as ConsoleTreeItem)!).ToList();
    }

    public ConsoleTreeItem SetFold(bool foldEnabled)
    {
        Fold = foldEnabled;
        return this;
    }

    public ConsoleTreeItem SetForegroundColor(ConsoleColor foregroundColor)
    {
        ForegroundColor = foregroundColor;
        return this;
    }

    public ConsoleTreeItem SetBackgroundColor(ConsoleColor backgroundColor)
    {
        BackgroundColor = backgroundColor;
        return this;
    }

    public ConsoleTreeItem SetDummy(bool dummyEnabled)
    {
        Dummy = dummyEnabled;
        return this;
    }

    public ConsoleTreeItem SetTag(object tag)
    {
        Tag = tag;
        return this;
    }

    public ConsoleTreeItem SetBridgeLength(int length)
    {
        BridgeLength = length;
        return this;
    }

    public ConsoleTreeItem? GetParent()
    {
        return Parent;
    }
}

