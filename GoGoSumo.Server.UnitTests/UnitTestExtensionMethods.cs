namespace GoGoSumo.Server.UnitTests;
public static class UnitTestExtensionMethods
{
    public static PropType? GetPropertyByName<PropType>(this object obj, string property)
    {
        return (PropType?)(obj.GetType().GetProperty(property)?.GetValue(obj));
    }
}
