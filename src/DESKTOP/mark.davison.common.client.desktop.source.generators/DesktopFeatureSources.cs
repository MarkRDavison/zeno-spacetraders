namespace mark.davison.common.client.desktop.source.generators;

[ExcludeFromCodeCoverage]
public static class DesktopFeatureSources
{
    public static string UseDesktopStateAttribute(string ns)
    {
        return $@"namespace {ns};

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class UseDesktopStateAttribute : Attribute
{{
    public Type[] Types {{ get; set; }}

    public UseDesktopStateAttribute(params Type[] types)
    {{
        Types = types;
    }}
}}";
    }

    public static string DesktopEffectAttribute(string ns)
    {
        return $@"namespace {ns};

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class DesktopEffectAttribute : Attribute
{{
}}";
    }

    public static string DesktopReducerAttribute(string ns)
    {
        return $@"namespace {ns};

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class DesktopReducerAttribute : Attribute
{{
}}";
    }
}
