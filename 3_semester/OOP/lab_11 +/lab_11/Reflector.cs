using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

public static class Reflector
{
    private static Type GetTypeByName(string name) =>
        Type.GetType(name) ??
        AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .FirstOrDefault(t => t.FullName == name || t.Name == name);

    public static string GetAssemblyName(string className) =>
        GetTypeByName(className)?.Assembly.FullName ?? "Тип не найден";

    public static bool HasPublicConstructors(string className) =>
        GetTypeByName(className)?.GetConstructors().Length > 0;

    public static IEnumerable<string> GetPublicMethods(string className) =>
        GetTypeByName(className)?
            .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
            .Select(m => m.ToString())
            ?? Enumerable.Empty<string>();

    public static IEnumerable<string> GetFieldsAndProps(string className)
    {
        var t = GetTypeByName(className);
        if (t == null) return Enumerable.Empty<string>();

        return t.GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
            .Where(m => m.MemberType == MemberTypes.Field || m.MemberType == MemberTypes.Property)
            .Select(m => $"{m.MemberType}: {m.Name}");
    }

    public static IEnumerable<string> GetInterfaces(string className) =>
        GetTypeByName(className)?
            .GetInterfaces()
            .Select(i => i.Name)
            ?? Enumerable.Empty<string>();

    public static IEnumerable<string> MethodsWithParameterType(string className, string paramType)
    {
        var t = GetTypeByName(className);
        var p = GetTypeByName(paramType);
        if (t == null || p == null) return Enumerable.Empty<string>();

        return t.GetMethods()
            .Where(m => m.GetParameters().Any(a => a.ParameterType == p))
            .Select(m => m.Name);
    }

    private static object GenerateValue(Type t)
    {
        if (t == typeof(int)) return 1;
        if (t == typeof(double)) return 2.5;
        if (t == typeof(string)) return "generated";
        if (t == typeof(bool)) return true;
        if (t == typeof(DateTime)) return DateTime.Now;

        return Activator.CreateInstance(t);
    }

    public static object Invoke(object obj, string methodName, object[] parameters = null)
    {
        var method = obj.GetType().GetMethod(methodName);
        if (method == null) throw new Exception("Метод не найден");

        if (parameters == null)
            parameters = method.GetParameters()
                .Select(p => GenerateValue(p.ParameterType))
                .ToArray();

        return method.Invoke(obj, parameters);
    }

    public static T Create<T>()
    {
        var t = typeof(T);
        var ctor = t.GetConstructors().OrderBy(c => c.GetParameters().Length).First();
        var args = ctor.GetParameters().Select(p => GenerateValue(p.ParameterType)).ToArray();
        return (T)ctor.Invoke(args);
    }

    public static void WriteToFile(string file, string content) =>
        File.WriteAllText(file, content);
}
