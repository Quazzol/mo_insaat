using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Backend.Misc;

public class Check
{
    public static void NotNull<T>(T value, string valueName)
    {
        if (value == null)
        {
            throw new ArgumentNullException(valueName);
        }
    }

    public static void NotNull<T>(T value, string valueName, string message)
    {
        if (value == null)
        {
            throw new ArgumentNullException(valueName, message);
        }
    }

    public static void NotNull<T>(T? value, string valueName)
        where T : struct
    {
        if (!value.HasValue)
        {
            throw new ArgumentNullException(valueName);
        }
    }

    public static void NotNull<T>(T? value, string valueName, string message)
        where T : struct
    {
        if (!value.HasValue)
        {
            throw new ArgumentNullException(valueName, message);
        }
    }

    public static void NotEmpty(Guid value, string valueName)
    {
        if (value.Equals(Guid.Empty))
        {
            throw new ArgumentException("Guid value must not be empty", valueName);
        }
    }

    public static void NotEmpty(int value, string valueName)
    {
        if (value == 0)
        {
            throw new ArgumentException("Guid value must not be empty", valueName);
        }
    }

    public static void NotEmpty<T>(IEnumerable<T> value, string valueName)
    {
        if (!value.Any())
        {
            throw new ArgumentException("Collection must not be empty", valueName);
        }
    }

    public static void Empty(Guid value, string valueName)
    {
        if (!value.Equals(Guid.Empty))
        {
            throw new ArgumentException("Guid value must be empty", valueName);
        }
    }

    public static void NotNullOrEmpty(string value, string valueName)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("string value must not be empty", valueName);
        }
    }

    public static void NotNullOrEmpty(string value, string valueName, string message)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException(message, valueName);
        }
    }

    public static void NotNullOrWhiteSpace(string value, string valueName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("string value must not be empty", valueName);
        }
    }

    public static void NotNullOrWhiteSpace(string value, string valueName, string message)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(message, valueName);
        }
    }

    public static void IsTrue<T>(Func<T, bool> condition, T target, string valueName)
    {
        if (!condition(target))
        {
            throw new ArgumentException("condition was not true", valueName);
        }
    }

    public static void IsTrue<T>(Func<T, bool> condition, T target, string valueName, string message)
    {
        if (!condition(target))
        {
            throw new ArgumentException(message, valueName);
        }
    }

    public static void IsFalse<T>(Func<T, bool> condition, T target, string valueName)
    {
        if (condition(target))
        {
            throw new ArgumentException("condition was true", valueName);
        }
    }

    public static void IsFalse<T>(Func<T, bool> condition, T target, string valueName, string message)
    {
        if (condition(target))
        {
            throw new ArgumentException(message, valueName);
        }
    }

    public static void GreaterThan<T>(T lowerLimit, T value, string valueName)
        where T : IComparable<T>
    {
        if (value.CompareTo(lowerLimit) <= 0)
        {
            throw new ArgumentOutOfRangeException(valueName);
        }
    }

    public static void GreaterThan<T>(T lowerLimit, T value, string valueName, string message)
        where T : IComparable<T>
    {
        if (value.CompareTo(lowerLimit) <= 0)
        {
            throw new ArgumentOutOfRangeException(valueName, message);
        }
    }

    public static void LessThan<T>(T upperLimit, T value, string valueName)
        where T : IComparable<T>
    {
        if (value.CompareTo(upperLimit) >= 0)
        {
            throw new ArgumentOutOfRangeException(valueName);
        }
    }

    public static void LessThan<T>(T upperLimit, T value, string valueName, string message)
        where T : IComparable<T>
    {
        if (value.CompareTo(upperLimit) >= 0)
        {
            throw new ArgumentOutOfRangeException(valueName, message);
        }
    }

    public static void HasItem<T>(IEnumerable<T> value, string valueName)
    {
        if (value == null)
        {
            throw new ArgumentException($"{valueName} null !");
        }

        if (!value.Any())
        {
            throw new ArgumentException($"{valueName} has not item");
        }
    }

    public static T IsTypeOf<T>(object obj)
        where T : class
    {
        NotNull(obj, "obj");

        if (obj is T value)
        {
            return value;
        }

        throw new ArgumentException($"{obj.GetType().Name} is not an instance of type {typeof(T).Name}");
    }

    public static void EnumDefined(Type enumType, object value, string valueName)
    {
        if (Enum.IsDefined(enumType, value) == false)
        {
            throw new ArgumentException($"{valueName} value not defined value in {enumType}", valueName);
        }
    }

    public static void IsUniqueSet<T>(IEnumerable<T> value, string valueName)
    {
        var set = new HashSet<T>();
        if (value.Any(item => !set.Add(item)))
        {
            throw new ArgumentException("values in the collection are not unique", valueName);
        }
    }
}
