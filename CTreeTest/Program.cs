using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;

using CTree;



// BasicTest();
// FoldingTest(true);
// DummyTest(true);
// ColorTest();
// FindTest();
// CloneTest();
// BridgeTest();
ItemPaddingTest();



void BasicTest(bool builder)
{
    if (builder)
    {
        var root = new ConsoleTree("루트");
        root.Add("자식-1")
            .AddReturnChild("자식-2")
                .Add("자식-2-1")
                .AddReturnChild("자식-2-2")
                    .Add("자식-2-2-1")
                    .AddReturnParent("자식-2-2-2")
                .AddReturnParent("자식-2-3")
            .Add("자식-3")
            .Add("자식-4");
        root.Print();
    }
    else
    {
        ConsoleTree root = new ConsoleTree("루트");

        ConsoleTreeItem child1 = new("자식-1");
        ConsoleTreeItem child2 = new("자식-2");
        ConsoleTreeItem child3 = new("자식-3");

        {
            ConsoleTreeItem child21 = new("자식-2-1");
            ConsoleTreeItem child22 = new("자식-2-2");

            {
                ConsoleTreeItem child221 = new("자식-2-2-1");
                ConsoleTreeItem child222 = new("자식-2-2-2");
                child22.Add(child221, child222);
            }
            ConsoleTreeItem child23 = new("자식-2-3");
            child2.Add(child21, child22, child23);
        }

        ConsoleTreeItem child4 = new("자식-4");
        root.Add(child1, child2, child3, child4);
        root.Print();
    }
}

void FoldingTest(bool builder)
{
    if (builder)
    {
        var root = new ConsoleTree("루트");
        root.Add("자식-1")
            .AddReturnChild("자식-2")
                .Add("자식-2-1")
                .AddReturnChild(new ConsoleTreeItem("자식-2-2") { Fold = true })
                    .Add("자식-2-2-1")
                    .AddReturnParent("자식-2-2-2")
                .AddReturnParent("자식-2-3")
            .Add("자식-3")
            .Add("자식-4");
        root.Print();
    }
    else
    {
        ConsoleTree root = new ConsoleTree("루트");

        ConsoleTreeItem child1 = new("자식-1");
        ConsoleTreeItem child2 = new("자식-2");

        {
            ConsoleTreeItem child21 = new("자식-2-1");
            ConsoleTreeItem child22 = new("자식-2-2") { Fold = true };

            {
                ConsoleTreeItem child221 = new("자식-2-2-1");
                ConsoleTreeItem child222 = new("자식-2-2-2");
                child22.Add(child221, child222);
            }
            ConsoleTreeItem child23 = new("자식-2-3");
            child2.Add(child21, child22, child23);
        }

        ConsoleTreeItem child3 = new("자식-3");
        ConsoleTreeItem child4 = new("자식-4");
        root.Add(child1, child2, child3, child4);
        root.Print();
    }
   
}

void DummyTest(bool builder)
{
    if (builder)
    {
        var root = new ConsoleTree("루트");
        root.Add(new ConsoleTreeItem("자식-1") { Dummy = true })
            .AddReturnChild("자식-2")
                .Add("자식-2-1")
                .AddDummy()
                .AddReturnChild("자식-2-2")
                    .Add("자식-2-2-1")
                    .AddReturnParent("자식-2-2-2")
                .AddDummy(2)
                .AddReturnParent("자식-2-3")
            .Add("자식-3")
            .Add("자식-4");
        root.Print();
    }
    else
    {
        ConsoleTree root = new ConsoleTree("루트");

        ConsoleTreeItem child1 = new("자식-1") { Dummy = true }; // 더미로 만들어버릴 수 있음
        ConsoleTreeItem child2 = new("자식-2");

        {
            ConsoleTreeItem child21 = new("자식-2-1");
            ConsoleTreeItem child22 = new("자식-2-2");

            {
                ConsoleTreeItem child221 = new("자식-2-2-1");
                ConsoleTreeItem child222 = new("자식-2-2-2");
                child22.Add(child221, child222);
            }
            ConsoleTreeItem child23 = new("자식-2-3");
            child2.Add(child21);
            child2.AddDummy();
            child2.Add(child22);
            child2.AddDummy(2);
            child2.Add(child23);
        }

        ConsoleTreeItem child3 = new("자식-3");
        ConsoleTreeItem child4 = new("자식-4");
        root.Add(child1, child2, child3, child4);
        root.Print();
    }
}

void ColorTest(bool builder)
{
    ConsoleTree root = new("루트")
    {
        ItemForegroundColor = ConsoleColor.Cyan,        // 전체 아이템 색상, 브릿지 색상 변경가능
        BridgeForegroundColor = ConsoleColor.Red
    };

    ConsoleTreeItem child1 = new("자식-1");
    ConsoleTreeItem child2 = new("자식-2");

    {
        ConsoleTreeItem child21 = new("자식-2-1");
        ConsoleTreeItem child22 = new("자식-2-2");

        {
            // 개별 아이템 색상 변경도 가능
            ConsoleTreeItem child221 = new("자식-2-2-1") { ForegroundColor = ConsoleColor.DarkYellow };
            ConsoleTreeItem child222 = new("자식-2-2-2") { ForegroundColor = ConsoleColor.DarkYellow };
            child22.Add(child221, child222);
        }
        ConsoleTreeItem child23 = new("자식-2-3");
        child2.Add(child21, child22, child23);
    }

    ConsoleTreeItem child3 = new("자식-3");
    ConsoleTreeItem child4 = new("자식-4");
    root.Add(child1, child2, child3, child4);
    root.Print();
}



void FindTest()
{
    var root = new ConsoleTree("루트", 0);
    root.Add("자식-1", 1)
        .AddReturnChild("자식-2", 2)
            .Add("자식-2-1", 3)
            .AddReturnChild("자식-2-2", 4)
                .Add("자식-2-2-1", 5)
                .AddReturnParent("자식-2-2-2", 6)
            .AddReturnParent("자식-2-3", 7)
        .Add("자식-3", 8)
        .Add("자식-4", 9);

    Debug.Assert(root.Find(x => (int)x.Tag == 0)?.Name == "루트");
    Debug.Assert(root.Find(x => (int)x.Tag == 1)?.Name == "자식-1");
    Debug.Assert(root.Find(x => (int)x.Tag == 2)?.Name == "자식-2");
    Debug.Assert(root.Find(x => (int)x.Tag == 3)?.Name == "자식-2-1");
    Debug.Assert(root.Find(x => (int)x.Tag == 4)?.Name == "자식-2-2");
    Debug.Assert(root.Find(x => (int)x.Tag == 5)?.Name == "자식-2-2-1");
    Debug.Assert(root.Find(x => (int)x.Tag == 6)?.Name == "자식-2-2-2");
    Debug.Assert(root.Find(x => (int)x.Tag == 7)?.Name == "자식-2-3");
    Debug.Assert(root.Find(x => (int)x.Tag == 8)?.Name == "자식-3");
    Debug.Assert(root.Find(x => (int)x.Tag == 9)?.Name == "자식-4");
}

void CloneTest()
{
    {
        var root = new ConsoleTree("루트", new not_cloneable { data = 0 });
        root.Add(new ConsoleTreeItem("자식-1", new not_cloneable { data = 1 }))
            .AddReturnChild("자식-2", new not_cloneable { data = 2 })
                .Add("자식-2-1", new not_cloneable { data = 3 })
                .AddReturnChild("자식-2-2", new not_cloneable { data = 4 })
                    .Add("자식-2-2-1", new not_cloneable { data = 5 })
                    .AddReturnParent("자식-2-2-2", new not_cloneable { data = 6 })
                .AddReturnParent("자식-2-3", new not_cloneable { data = 7 })
            .Add("자식-3", new not_cloneable { data = 8 })
            .Add("자식-4", new not_cloneable { data = 9 });
        root.Print();

        var newRoot = root.Clone() as ConsoleTree;
        Debug.Assert(newRoot.Find(x => ((not_cloneable)x.Tag).data == 0)?.Name == "루트");
        Debug.Assert(newRoot.Find(x => ((not_cloneable)x.Tag).data == 1)?.Name == "자식-1");
        Debug.Assert(newRoot.Find(x => ((not_cloneable)x.Tag).data == 2)?.Name == "자식-2");
        Debug.Assert(newRoot.Find(x => ((not_cloneable)x.Tag).data == 3)?.Name == "자식-2-1");
        Debug.Assert(newRoot.Find(x => ((not_cloneable)x.Tag).data == 4)?.Name == "자식-2-2");
        Debug.Assert(newRoot.Find(x => ((not_cloneable)x.Tag).data == 5)?.Name == "자식-2-2-1");
        Debug.Assert(newRoot.Find(x => ((not_cloneable)x.Tag).data == 6)?.Name == "자식-2-2-2");
        Debug.Assert(newRoot.Find(x => ((not_cloneable)x.Tag).data == 7)?.Name == "자식-2-3");
        Debug.Assert(newRoot.Find(x => ((not_cloneable)x.Tag).data == 8)?.Name == "자식-3");
        Debug.Assert(newRoot.Find(x => ((not_cloneable)x.Tag).data == 9)?.Name == "자식-4");

        // 새로 복제한 트리의 값들을 모두 수정하더라도
        // 깊은 복사가 되지 않았기 때문에 기존 트리도 영향을 받음
        newRoot.ForEach(x => ((not_cloneable)x.Tag).data++);

        Debug.Assert(root.Find(x => ((not_cloneable)x.Tag).data == 0 + 1)?.Name == "루트");
        Debug.Assert(root.Find(x => ((not_cloneable)x.Tag).data == 1 + 1)?.Name == "자식-1");
        Debug.Assert(root.Find(x => ((not_cloneable)x.Tag).data == 2 + 1)?.Name == "자식-2");
        Debug.Assert(root.Find(x => ((not_cloneable)x.Tag).data == 3 + 1)?.Name == "자식-2-1");
        Debug.Assert(root.Find(x => ((not_cloneable)x.Tag).data == 4 + 1)?.Name == "자식-2-2");
        Debug.Assert(root.Find(x => ((not_cloneable)x.Tag).data == 5 + 1)?.Name == "자식-2-2-1");
        Debug.Assert(root.Find(x => ((not_cloneable)x.Tag).data == 6 + 1)?.Name == "자식-2-2-2");
        Debug.Assert(root.Find(x => ((not_cloneable)x.Tag).data == 7 + 1)?.Name == "자식-2-3");
        Debug.Assert(root.Find(x => ((not_cloneable)x.Tag).data == 8 + 1)?.Name == "자식-3");
        Debug.Assert(root.Find(x => ((not_cloneable)x.Tag).data == 9 + 1)?.Name == "자식-4");
    }

    {
        var root = new ConsoleTree("루트", new cloneable { data = 0 });
        root.Add(new ConsoleTreeItem("자식-1", new cloneable { data = 1 }))
            .AddReturnChild("자식-2", new cloneable { data = 2 })
                .Add("자식-2-1", new cloneable { data = 3 })
                .AddReturnChild("자식-2-2", new cloneable { data = 4 })
                    .Add("자식-2-2-1", new cloneable { data = 5 })
                    .AddReturnParent("자식-2-2-2", new cloneable { data = 6 })
                .AddReturnParent("자식-2-3", new cloneable { data = 7 })
            .Add("자식-3", new cloneable { data = 8 })
            .Add("자식-4", new cloneable { data = 9 });
        root.Print();

        var newRoot = root.Clone() as ConsoleTree;
        Debug.Assert(newRoot.Find(x => ((cloneable)x.Tag).data == 0)?.Name == "루트");
        Debug.Assert(newRoot.Find(x => ((cloneable)x.Tag).data == 1)?.Name == "자식-1");
        Debug.Assert(newRoot.Find(x => ((cloneable)x.Tag).data == 2)?.Name == "자식-2");
        Debug.Assert(newRoot.Find(x => ((cloneable)x.Tag).data == 3)?.Name == "자식-2-1");
        Debug.Assert(newRoot.Find(x => ((cloneable)x.Tag).data == 4)?.Name == "자식-2-2");
        Debug.Assert(newRoot.Find(x => ((cloneable)x.Tag).data == 5)?.Name == "자식-2-2-1");
        Debug.Assert(newRoot.Find(x => ((cloneable)x.Tag).data == 6)?.Name == "자식-2-2-2");
        Debug.Assert(newRoot.Find(x => ((cloneable)x.Tag).data == 7)?.Name == "자식-2-3");
        Debug.Assert(newRoot.Find(x => ((cloneable)x.Tag).data == 8)?.Name == "자식-3");
        Debug.Assert(newRoot.Find(x => ((cloneable)x.Tag).data == 9)?.Name == "자식-4");

        // cloneable은 ICloneable을 상속받아서 깊은 복사를 구현해놨기 때문에
        // newRoot의 값들을 수정하더라도 기존 root에는 영향을 미치지 않는다.
        newRoot.ForEach(x => ((cloneable)x.Tag).data++);

        Debug.Assert(root.Find(x => ((cloneable)x.Tag).data == 0)?.Name == "루트");
        Debug.Assert(root.Find(x => ((cloneable)x.Tag).data == 1)?.Name == "자식-1");
        Debug.Assert(root.Find(x => ((cloneable)x.Tag).data == 2)?.Name == "자식-2");
        Debug.Assert(root.Find(x => ((cloneable)x.Tag).data == 3)?.Name == "자식-2-1");
        Debug.Assert(root.Find(x => ((cloneable)x.Tag).data == 4)?.Name == "자식-2-2");
        Debug.Assert(root.Find(x => ((cloneable)x.Tag).data == 5)?.Name == "자식-2-2-1");
        Debug.Assert(root.Find(x => ((cloneable)x.Tag).data == 6)?.Name == "자식-2-2-2");
        Debug.Assert(root.Find(x => ((cloneable)x.Tag).data == 7)?.Name == "자식-2-3");
        Debug.Assert(root.Find(x => ((cloneable)x.Tag).data == 8)?.Name == "자식-3");
        Debug.Assert(root.Find(x => ((cloneable)x.Tag).data == 9)?.Name == "자식-4");
    }
}

void BridgeTest()
{
    var root = new ConsoleTree("루트");

    for (int i = 0; i < 20; i++)
    {
        root.Add(new ConsoleTreeItem($"자식-{i}") { BridgeLength = i });
    }

    root.Print();
}

void ItemPaddingTest()
{
    var root = new ConsoleTree("루트");
    root.ItemLeftPad = 5;

    root.AddReturnChild(new ConsoleTreeItem($"자식-1"))
            .Add("자식-1-1")
            .Add("자식-1-2")
            .Add("자식-1-3");
    root.Add(new ConsoleTreeItem($"자식-1"));
    root.Add(new ConsoleTreeItem($"자식-1"));

    root.Print();
}

void FullTest()
{
    ConsoleTree ConsoleTree = new ConsoleTree("루트");

    {
        ConsoleTreeItem child1 = new ConsoleTreeItem("자식-1");

        {
            child1.Add(new ConsoleTreeItem("자식-1-1"));
            child1.Add(new ConsoleTreeItem("자식-1-2"));

            var child13 = new ConsoleTreeItem("자식-1-3");
            child1.Add(child13);

            {
                var child131 = new ConsoleTreeItem("자식-1-3-1");
                var child132 = new ConsoleTreeItem("자식-1-3-2");
                var child133 = new ConsoleTreeItem("자식-1-3-3");
                var child134 = new ConsoleTreeItem("자식-1-3-4");

                child13.Add(child131);
                child13.Add(child132);
                child13.Add(child133);
                child13.Add(child134);
            }

            child1.Add(new ConsoleTreeItem("자식-1-4"));
        }

        ConsoleTreeItem child2 = new ConsoleTreeItem("자식-2");// { Dummy = true};

        {
            child2.Add(new ConsoleTreeItem("자식-2-1"));
            child2.Add(new ConsoleTreeItem("자식-2-2"));
            var child23 = new ConsoleTreeItem("자식-2-3");
            child2.Add(child23);

            {
                child23.Add(new ConsoleTreeItem("자식-2-3-1"));
                child23.Add(new ConsoleTreeItem("자식-2-3-2"));
                child23.Add(new ConsoleTreeItem("자식-2-3-3"));
                child23.AddDummy();
                child23.AddDummy();
                child23.AddDummy();
                child23.Add(new ConsoleTreeItem("자식-2-3-4"));
            }
        }

        ConsoleTreeItem child3 = new ConsoleTreeItem("자식-3");
        ConsoleTreeItem child4 = new ConsoleTreeItem("자식-4");
        ConsoleTreeItem child5 = new ConsoleTreeItem("자식-5");

        {
            ConsoleTreeItem child51 = new ConsoleTreeItem("자식-5-1");
            ConsoleTreeItem child52 = new ConsoleTreeItem("자식-5-2");
            ConsoleTreeItem child53 = new ConsoleTreeItem("자식-5-3");

            {
                ConsoleTreeItem child521 = new ConsoleTreeItem("자식-5-2-1");
                ConsoleTreeItem child522 = new ConsoleTreeItem("자식-5-2-2");

                {
                    ConsoleTreeItem child5221 = new ConsoleTreeItem("자식-5-2-2-1");
                    ConsoleTreeItem child5222 = new ConsoleTreeItem("자식-5-2-2-2");
                    ConsoleTreeItem child5223 = new ConsoleTreeItem("자식-5-2-2-3") { Dummy = true };
                    ConsoleTreeItem child5224 = new ConsoleTreeItem("자식-5-2-2-4");

                    child522.Add(child5221);
                    child522.Add(child5222);
                    child522.Add(child5223);
                    child522.Add(child5224);
                }

                ConsoleTreeItem child523 = new ConsoleTreeItem("자식-5-2-3");

                child52.Add(child521);
                child52.Add(child522);
                child52.Add(child523);

                child52.ForegroundColor = ConsoleColor.DarkYellow;
            }

            child5.Add(child51);
            child5.Add(child52);
            child5.AddDummy();
            child5.AddDummy();
            child5.AddDummy();

            // child53.Dummy = true;
            child5.Add(child53);
        }

        ConsoleTree.Add(child1);
        ConsoleTree.Add(child2);
        ConsoleTree.Add(child3);
        ConsoleTree.Add(child4);
        ConsoleTree.Add(child5);
    }

    ConsoleTree.BridgeForegroundColor = ConsoleColor.Blue;
    ConsoleTree.ItemForegroundColor = ConsoleColor.Cyan;
    ConsoleTree.Print();
    Console.WriteLine();


    //while (true)
    //{
    //    Console.WriteLine(Console.BufferWidth);
    //    Console.WriteLine(Console.BufferHeight);
    //    Console.WriteLine(Console.WindowWidth);
    //    Console.WriteLine(Console.WindowHeight);
    //    Console.WriteLine("한한한한한한한한한한한한한한한한한한한한한한한한한한한한한한한한한한한한한한한한");

    //    Thread.Sleep(5000);
    //}



}




class not_cloneable
{
    public int data;
}


class cloneable : ICloneable
{
    public int data;
    public object Clone()
    {
        return new cloneable() { data = this.data };
    }
}
