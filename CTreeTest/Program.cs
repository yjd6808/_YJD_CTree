using CTree;



// BasicTest();
// FoldingTest();
// DummyTest();
ColorTest();













void BasicTest()
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

void FoldingTest()
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

void DummyTest()
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

void ColorTest()
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

