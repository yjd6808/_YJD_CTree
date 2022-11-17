/* * * * * * * * * * * * * 
 * 작성자: 윤정도
 * 생성일: 11/17/2022 3:14:20 PM
 * * * * * * * * * * * * *
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace CTree.Internal;


internal static class EnumerableEx
{
    public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
        using var enumerator = enumerable.GetEnumerator();
        while (enumerator.MoveNext())
            action(enumerator.Current);
    }
}