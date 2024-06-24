namespace mark.davison.common.client.desktop.source.generators;

[ExcludeFromCodeCoverage]
[Generator]
public class Generator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForPostInitialization(_ =>
        {
            _.AddSource("UseDesktopStateAttribute.g.cs", SourceText.From(DesktopFeatureSources.UseDesktopStateAttribute("mark.davison.common.client.desktop.generators"), Encoding.UTF8));
            _.AddSource("DesktopEffectAttribute.g.cs", SourceText.From(DesktopFeatureSources.DesktopEffectAttribute("mark.davison.common.client.desktop.generators"), Encoding.UTF8));
            _.AddSource("DesktopReducerAttribute.g.cs", SourceText.From(DesktopFeatureSources.DesktopReducerAttribute("mark.davison.common.client.desktop.generators"), Encoding.UTF8));
        });
    }

    private (HashSet<ITypeSymbol> StateTypes, Dictionary<ITypeSymbol, List<IMethodSymbol>> EffectTypes, Dictionary<ITypeSymbol, List<IMethodSymbol>> ReducerTypes)
    ExtractStateTypeInfo(IList<ITypeSymbol> types)
    {
        HashSet<ITypeSymbol> stateTypes = [];
        Dictionary<ITypeSymbol, List<IMethodSymbol>> effectClassMethods = [];
        Dictionary<ITypeSymbol, List<IMethodSymbol>> reducerClassMethods = [];

        foreach (var symbol in types)
        {
            if (symbol.Interfaces.Any(_ => _.Name == "IDesktopState" && !_.IsGenericType))
            {
                stateTypes.Add(symbol);
                continue;
            }

            var attributes = symbol.GetAttributes();

            if (attributes.Any(_ => _.AttributeClass?.Name == "DesktopEffectAttribute"))
            {
                var effectMethods = new List<IMethodSymbol>();

                var membersEffect = symbol.GetMembers();

                foreach (var effectMember in symbol.GetMembers())
                {
                    if (effectMember is IMethodSymbol { IsStatic: false, IsAsync: true } methodSymbol)
                    {
                        var returnType = methodSymbol.ReturnType;
                        var paramTypes = methodSymbol.Parameters.ToList();

                        if (returnType.Name != "Task" || paramTypes.Count != 2 || paramTypes[1].Type.Name != "IDesktopStateDispatcher")
                        {
                            continue;
                        }

                        effectMethods.Add(methodSymbol);
                    }
                }

                effectClassMethods.Add(symbol, effectMethods);
                continue;
            }

            var members = symbol.GetMembers();

            foreach (var member in members)
            {
                var memberAttributes = member.GetAttributes();

                if (memberAttributes.Any(_ => _.AttributeClass?.Name == "DesktopReducerAttribute"))
                {

                    if (member is IMethodSymbol { IsStatic: true, IsAsync: false } methodSymbol)
                    {
                        var returnType = methodSymbol.ReturnType;
                        var paramTypes = methodSymbol.Parameters.ToList();

                        if (!paramTypes.Any(_ => SymbolEqualityComparer.Default.Equals(_.Type, returnType)))
                        {
                            continue;
                        }

                        if (!reducerClassMethods.ContainsKey(symbol))
                        {
                            reducerClassMethods.Add(symbol, []);
                        }

                        var classMethods = reducerClassMethods[symbol];

                        classMethods.Add(methodSymbol);
                    }
                }
            }
        }

        return (stateTypes, effectClassMethods, reducerClassMethods);
    }

    public void Execute(GeneratorExecutionContext context)
    {
        var (assemblyMarkerClassNamespace, namespaces) = FetchDesktopStateNamespaces(context);

        if (!namespaces.Any())
        {
            return;
        }

        var symbols = SourceGeneratorHelpers.GetPotentialTypeSymbols(context, namespaces);

        var (state, effect, reducer) = ExtractStateTypeInfo(symbols);

        AddIgnition(context, assemblyMarkerClassNamespace, state, effect, reducer);
    }

    private void AddIgnition(GeneratorExecutionContext context, string assemblyMarkerClassNamespace, HashSet<ITypeSymbol> stateTypes, Dictionary<ITypeSymbol, List<IMethodSymbol>> effectTypes, Dictionary<ITypeSymbol, List<IMethodSymbol>> reducerTypes)
    {
        StringBuilder stringBuilder = new();
        stringBuilder.AppendLine($"using Microsoft.Extensions.DependencyInjection;");
        stringBuilder.AppendLine();
        stringBuilder.AppendLine($"namespace {assemblyMarkerClassNamespace}");
        stringBuilder.AppendLine($"{{");
        stringBuilder.AppendLine($"    public static class DesktopStateDependecyInjectionExtensions");
        stringBuilder.AppendLine($"    {{");
        stringBuilder.AppendLine($"        public static IServiceCollection AddDesktopState(this IServiceCollection services)");
        stringBuilder.AppendLine($"        {{");
        stringBuilder.AppendLine($"            services.AddSingleton(typeof(IState<>), typeof(StateImplementation<>));");
        stringBuilder.AppendLine();

        const string StateStoreFullyQualified = "mark.davison.spacetraders.desktop.ui.State.StateStore"; // TODO: update when common-ified

        foreach (var s in stateTypes)
        {

        }

        foreach (var eKv in effectTypes)
        {
            var effectClassFullyQualified = SourceGeneratorHelpers.GetFullyQualifiedName(eKv.Key);
            stringBuilder.AppendLine($"            services.AddTransient<{effectClassFullyQualified}>();");
            stringBuilder.AppendLine();

            foreach (var effectMethod in eKv.Value)
            {
                var actionFullyQualified = SourceGeneratorHelpers.GetFullyQualifiedName(effectMethod.Parameters[0].Type); // TODO: Better return type so this isn't trusting prev validation

                stringBuilder.AppendLine($"            {StateStoreFullyQualified}.RegisterEffectCallback<{actionFullyQualified}, {effectClassFullyQualified}>((services, action, dispatcher) => services.GetRequiredService<{effectClassFullyQualified}>().{effectMethod.Name}(action, dispatcher));");
            }
        }

        if (effectTypes.Any())
        {
            stringBuilder.AppendLine();
        }

        foreach (var rKv in reducerTypes)
        {
            foreach (var reducerMethod in rKv.Value)
            {
                var actionFullyQualified = SourceGeneratorHelpers.GetFullyQualifiedName(reducerMethod.Parameters[1].Type); // TODO: Better return type so this isn't trusting prev validation
                var stateFullyQualified = SourceGeneratorHelpers.GetFullyQualifiedName(reducerMethod.ReturnType); // TODO: Better return type so this isn't trusting prev validation

                var reducerClassFullyQualified = SourceGeneratorHelpers.GetFullyQualifiedName(rKv.Key);

                stringBuilder.AppendLine($"            {StateStoreFullyQualified}.RegisterReducerCallback<{actionFullyQualified}, {stateFullyQualified}>((state, action) => {reducerClassFullyQualified}.{reducerMethod.Name}(state, action));");
            }
        }

        if (reducerTypes.Any())
        {
            stringBuilder.AppendLine();
        }

        stringBuilder.AppendLine($"            return services;");
        stringBuilder.AppendLine($"        }}");
        stringBuilder.AppendLine($"    }}");
        stringBuilder.AppendLine($"}}");

        context.AddSource("DesktopStateDependecyInjectionExtensions.g.cs", SourceText.From(stringBuilder.ToString(), Encoding.UTF8));
    }

    private (string, HashSet<string>) FetchDesktopStateNamespaces(GeneratorExecutionContext context)
    {
        var types = context.Compilation.SourceModule.GlobalNamespace
            .GetNamespaceMembers()
            .SelectMany(SourceGeneratorHelpers.GetAllTypes)
            .ToList();

        var assemblyMarkerClass = types
            .FirstOrDefault(_ => _
                .GetAttributes()
                .Any(__ => __.AttributeClass?.Name == "UseDesktopStateAttribute"));


        if (assemblyMarkerClass == null)
        {
            return (string.Empty, new());
        }

        var at = assemblyMarkerClass.GetAttributes().FirstOrDefault(_ => _.AttributeClass?.Name == "UseDesktopStateAttribute");

        var assemblyMarkerClassNamespace = SourceGeneratorHelpers.GetNamespace(assemblyMarkerClass);

        var namedArgs = at?.NamedArguments.ToList();

        var stateTypes = namedArgs?.FirstOrDefault(_ => _.Key == "Types").Value.Values.ToList();

        if (stateTypes is null || !stateTypes.Any())
        {
            return (string.Empty, new());
        }

        var stateTypeSymbols = stateTypes.Select(_ => _.Value as INamedTypeSymbol).Where(_ => _ != null).Cast<INamedTypeSymbol>().ToList();

        return (assemblyMarkerClassNamespace, new(stateTypeSymbols.Select(SourceGeneratorHelpers.GetNamespace)));
    }

}
