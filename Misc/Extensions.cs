using System.ComponentModel;
using System.Reflection;

namespace Backend.Misc;

public static class EnumExtensions
{
    private static Dictionary<char, char> _toBeReplaced = new Dictionary<char, char>()
    {
        {'Ç', 'C'},
        {'Ş', 'S'},
        {'Ö', 'O'},
        {'İ', 'I'},
        {'Ğ', 'G'},
        {'Ü', 'U'},
        {'ç', 'c'},
        {'ş', 's'},
        {'ö', 'o'},
        {'ı', 'i'},
        {'ğ', 'g'},
        {'ü', 'u'}
    };

    public static string GetDescription<T>(this T enumerationValue) where T : struct
    {
        Type type = enumerationValue.GetType();
        if (!type.IsEnum)
        {
            throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
        }

        //Tries to find a DescriptionAttribute for a potential friendly name
        //for the enum
        var value = enumerationValue.ToString();
        if (value is null)
            return string.Empty;

        MemberInfo[] memberInfo = type.GetMember(value);
        if (memberInfo != null && memberInfo.Length > 0)
        {
            object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attrs != null && attrs.Length > 0)
            {
                //Pull out the description value
                return ((DescriptionAttribute)attrs[0]).Description;
            }
        }
        //If we have no description attribute, just return the ToString of the enum
        return value;
    }

    public static string Linkify(this string? str)
    {
        if (string.IsNullOrWhiteSpace(str))
            return string.Empty;

        // Replace Turkish characters with english equivalent
        foreach (var kvp in _toBeReplaced)
        {
            str = str.Replace(kvp.Key, kvp.Value);
        }

        // Replace leave only alpha numeric, space and dash characters
        char[] arr = str.Where(c => (char.IsLetterOrDigit(c) ||
                             char.IsWhiteSpace(c) ||
                             c == '-')).ToArray();

        str = new string(arr);

        // Trim and replace spaces with dashes
        return "/" + str.ToLower().Trim().Replace(' ', '-');
    }

    public static string Clearify(this string? str)
    {
        if (str is null)
            return string.Empty;

        // Replace Turkish characters with english equivalent
        foreach (var kvp in _toBeReplaced)
        {
            str = str.Replace(kvp.Key, kvp.Value);
        }

        // Replance leave only alpha numeric, space and dash characters
        char[] arr = str.Where(c => (char.IsLetterOrDigit(c) ||
                             char.IsWhiteSpace(c) ||
                             c == '_')).ToArray();

        str = new string(arr);

        return str.ToLower().Trim().Replace(' ', '_');
    }

    public static bool IsEmpty(this string? str)
    {
        return string.IsNullOrWhiteSpace(str);
    }

    public static bool IsEmpty(this Guid? guid)
    {
        return guid == null || guid == Guid.Empty;
    }
}
