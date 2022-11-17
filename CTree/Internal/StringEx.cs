/* * * * * * * * * * * * * 
 * 작성자: 윤정도
 * 생성일: 11/17/2022 9:01:17 PM
 * * * * * * * * * * * * *
 */

namespace CTree.Internal;

internal static class StringEx
{
    // 아스키 코드 값이 256보다 큰 문자는 콘솔 너비 2를 차지한다고 가정한다.
    internal static int Width(this string str)
    {
        return str.Aggregate(0, (len, ch) => ch > 256 ? len + 2 : len + 1);
    }

    internal static string AddSpace(this string str, int count)
    {
        return str + "".PadRight(count);
    }

    internal static string AddChar(this string str, char ch, int count)
    {
        return str + "".PadRight(count, ch);
    }
}