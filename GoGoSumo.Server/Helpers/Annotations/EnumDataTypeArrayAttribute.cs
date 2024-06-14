using System.ComponentModel.DataAnnotations;

namespace GoGoSumo.Server.Helpers.Annotations;

// Ref: https://stackoverflow.com/questions/31760687/how-to-validate-an-array-of-type-enum-with-enumdatatype-using-data-annotations-w

[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false)]
public sealed class EnumDataTypeArrayAttribute : DataTypeAttribute
{
    public EnumDataTypeArrayAttribute(Type enumType)
        : base("Enumeration")
    {
        if (enumType == null)
        {
            throw new InvalidOperationException("Type cannot be null");
        }
        if (!enumType.IsEnum)
        {
            throw new InvalidOperationException("Type must be an enum");
        }
        EnumType = enumType;
    }

    public override bool IsValid(object? value)
    {
        if (value is null) return true;
        var at = value.GetType();
        if (!at.IsArray) return false;
        var t = at.GetElementType();
        if (t != EnumType) return false;
        foreach (var v in (value as Array)!)
        {
            if (!Enum.IsDefined(EnumType, v))
            {
                return false;
            }
        }
        return true;
    }

    public Type EnumType
    {
        get;
        private set;
    }
}