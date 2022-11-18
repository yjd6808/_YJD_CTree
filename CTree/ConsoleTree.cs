/* * * * * * * * * * * * * 
 * 작성자: 윤정도
 * 생성일: 11/17/2022 4:36:19 PM
 *
 * * * * * * * * * * * * *
 * 옛날에 코딩 처음 배웠을 때 cmd에서 tree 명령처럼 콘솔창에 출력하는 프로그램을
 * 만들어보고 싶었는데... 이번에 생각난김에 만듬
 *
 */

using System.Reflection.Metadata.Ecma335;

using CTree.Internal;
using CTree;

public class ConsoleTree : ICloneable
{
    public ConsoleTreeItem Root { get; }
    public int Count => Root.Count + 1;
    public int CountRecursive => Root.CountRecursive + 1;

    public char VertialBridge { get; set; } = '│';
    public char VerticalItemBridge { get; set; } = '├';
    public char VerticalItemBridgeLast { get; set; } = '└';
    public char HorizontalBrdige { get; set; } = '─';
    public char Space { get; set; } = ' ';
    public int ItemLeftPad { get; set; } = 1;


    public ConsoleColor BridgeForegroundColor { get; set; } = Console.ForegroundColor;
    public ConsoleColor ItemForegroundColor { get; set; } = Console.ForegroundColor;
    public ConsoleColor ItemBackgroundColor { get; set; } = Console.BackgroundColor;

    public ConsoleTree() => Root = new ConsoleTreeItem();
    public ConsoleTree(ConsoleTreeItem root) => Root = root;
    public ConsoleTree(string rootName) => Root = new ConsoleTreeItem(rootName);
    public ConsoleTree(string rootName, object tag) => Root = new ConsoleTreeItem(rootName, tag);
    public ConsoleTree(string rootName, object tag, List<ConsoleTreeItem> children) => Root = new ConsoleTreeItem(rootName, tag, children);
    public ConsoleTreeItem Add(ConsoleTreeItem item1)
    {
        Root.Add(item1);
        return Root;
    }
    public ConsoleTreeItem Add(params ConsoleTreeItem[] items)
    {
        Root.Add(items);
        return Root;
    }

    public ConsoleTreeItem Add(string name)
    {
        Root.Add(name);
        return Root;
    }

    public ConsoleTreeItem Add(string name, object tag)
    {
        Root.Add(name, tag);
        return Root;
    }

    public ConsoleTreeItem AddDummy(int count = 1)
    {
        Root.AddDummy(count);
        return Root;
    }

    public ConsoleTreeItem AddReturnChild(ConsoleTreeItem item) => Root.AddReturnChild(item);
    public ConsoleTreeItem AddReturnChild(string name) => Root.AddReturnChild(name);
    public ConsoleTreeItem AddReturnChild(string name, object tag) => Root.AddReturnChild(name ,tag);

    public bool Remove(ConsoleTreeItem item) => Root.Remove(item);
    public bool Contains(ConsoleTreeItem item) => Contains(Root, item);
    public ConsoleTreeItem? Find(Predicate<ConsoleTreeItem> predicate) => Find(Root, predicate);
    public void ForEach(Action<ConsoleTreeItem> action) => ForEach(Root, action);
    private static bool Contains(ConsoleTreeItem parent, ConsoleTreeItem item)
    {
        if (parent.Contains(item))
            return true;

        foreach (var child in parent)
            if (Contains(child, item))
                return true;

        return false;
    }

    private static void ForEach(ConsoleTreeItem parent, Action<ConsoleTreeItem> action)
    {
        action(parent);

        foreach (var child in parent)
            ForEach(child, action);
    }

    private static ConsoleTreeItem? Find(ConsoleTreeItem parent, Predicate<ConsoleTreeItem> predicate)
    {
        ConsoleTreeItem? find = null;

        if (predicate(parent))
            return parent;

        if (find == null)
        {
            foreach (var child in parent)
            {
                find = Find(child, predicate);

                if (find != null)
                    return find;
            }
        }

        return find;
    }

    

    public void Print()
    {
        PrintItemName(Root);
        Print(Root, string.Empty);
    }

    /*
     * bridge: ├────── <아이템 명>
     *         <----->
     *        아이템 명 이전의 문자열을 bridge라고 내가 명명함
     */

    private void Print(ConsoleTreeItem item, string bridge)
    {
        if (item.Count <= 0)
            return;

        ConsoleTreeItem? lastDummy = item.FindLast(x => x.Dummy, out int lastDummyIdx);     // 마지막 더미 아이템의 위치
        ConsoleTreeItem? unused = item.FindLast(x => !x.Dummy, out int lastNotDummyidx);    // 마지막 더미 아이템이 아닌 아이템의 위치

        // 마지막 위치에 더미노드가 있는 경우 잠시 빼놨다가 마지막에 다시 넣어주도록 한다.
        if (lastDummyIdx == item.Count - 1)
            item.RemoveAt(lastDummyIdx);

        for (int i = 0; i < item.Count; i++)
        {
            var child = item[i];

            string currentBridge;
            string nextBridge;

            if (i < lastNotDummyidx)
            {
                currentBridge = bridge + VerticalItemBridge.ToString().AddChar(HorizontalBrdige, child.BridgeLength).AddSpace(ItemLeftPad);
                nextBridge = currentBridge.Replace(VerticalItemBridge, VertialBridge).Replace(HorizontalBrdige, Space);
            }
            else
            {
                currentBridge = bridge + VerticalItemBridgeLast.ToString().AddChar(HorizontalBrdige, child.BridgeLength).AddSpace(ItemLeftPad);
                nextBridge = currentBridge.Replace(VerticalItemBridgeLast, Space).Replace(HorizontalBrdige, Space);
            }

            if (child.Dummy)
            {
                ConsoleEx.WriteLine(nextBridge, BridgeForegroundColor);
                continue;
            }

            ConsoleEx.Write(currentBridge, BridgeForegroundColor);
            PrintItemName(child);

            // 접힌 경우 자식은 출력안함
            if (!child.Fold)
                Print(child, nextBridge);
        }

        if (lastDummyIdx == item.Count - 1)
            item.Add(lastDummy!);
    }

    private void PrintItemName(ConsoleTreeItem item) =>
        ConsoleEx.WriteLine(item.Name,
            item.ForegroundColor == ConsoleColor.Black ? ItemForegroundColor : item.ForegroundColor,
            item.BackgroundColor == ConsoleColor.Black ? ItemBackgroundColor : item.BackgroundColor);

    public object Clone() => new ConsoleTree((Root.Clone() as ConsoleTreeItem)!);

    public ConsoleTree SetBridgeForegroundColor(ConsoleColor bridgeForegroundColor)
    {
        BridgeForegroundColor = bridgeForegroundColor;
        return this;
    }

    public ConsoleTree SetItemForegroundColor(ConsoleColor itemForegroundColor)
    {
        ItemForegroundColor = itemForegroundColor;
        return this;
    }

    public ConsoleTree SetItemBackgroundColor(ConsoleColor itemBackgroundColor)
    {
        ItemBackgroundColor = itemBackgroundColor;
        return this;
    }
}
