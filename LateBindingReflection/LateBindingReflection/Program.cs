using System.Reflection;
using System.Runtime.CompilerServices;

internal class Program
{
    private static void Main(string[] args)
    {
        const string TargetAssemblyFileName = "UtilityFunctioncs.dll";
        const string TargetNamespace = "UtilityFunctioncs";

        Assembly assembly = Assembly.LoadFile(
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            TargetAssemblyFileName));

        List<System.Type> classes = assembly.GetTypes()
            .Where(t => t.Namespace == TargetNamespace)
            .ToList();

        WritePromptToScreen("Please press the number key with the class you want to test");

        DisplayProgramElementList(classes);

        ConsoleKey consoleKey = Console.ReadKey().Key;
        Type? typeChoice = ReturnProgramElementReferenceFromList<Type>(classes);

        object? classInstance = Activator.CreateInstance(typeChoice, null);

        Console.Clear();

        WriteHeadingToScreeen($"Class: {typeChoice}");

        WritePromptToScreen("Choose relevant method");

        List<MethodInfo> methods = typeChoice.GetMethods().ToList();

        DisplayProgramElementList(methods);

        MethodInfo methodChoice = ReturnProgramElementReferenceFromList(methods);

        if (methodChoice != null )
        {
            Console.Clear();
            WriteHeadingToScreeen($"Class: {typeChoice} Method: {methodChoice.Name}");

            ParameterInfo[] parameters = methodChoice.GetParameters();

            object result = GetResult(classInstance, methodChoice, parameters);
        
            WriteResultToScreen(result);
        }
    }

    public static void WriteResultToScreen(object result)
    {
        Console.WriteLine();
        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"Result: {result}");
        Console.ResetColor();
        Console.WriteLine();
    }

    public static object GetResult(Object classInstance, MethodInfo methodInfo, ParameterInfo[] parameters)
    {
        object result = null;

        if (parameters.Length == 0)
        {
            result = methodInfo.Invoke(classInstance, null);
        }
        else
        {
            var paramValueArray = ReturnParameterValueInputAsObjectArray(parameters);
            result = methodInfo.Invoke(classInstance, paramValueArray);
        }

        return result;
    }

    private static object[] ReturnParameterValueInputAsObjectArray(ParameterInfo[] parameters)
    {
        object[] paramValues = new object[parameters.Length];
        int itemCount = 0;

        foreach (ParameterInfo parameter in parameters)
        {
            WritePromptToScreen($"Please enter a value for {parameter}");
            if(parameter.ParameterType == typeof(string))
            {
                string inputString = Console.ReadLine();
                paramValues[itemCount] = inputString;
            }
            else if (parameter.ParameterType == typeof(int))
            {
                int inputInt = Int32.Parse(Console.ReadLine());
                paramValues[itemCount] = inputInt;
            }
            else if (parameter.ParameterType == typeof(double))
            {
                double inputDouble = Double.Parse(Console.ReadLine());
                paramValues[itemCount] = inputDouble;
            }

            itemCount++;
        }
        return paramValues;
    }

    public static T ReturnProgramElementReferenceFromList<T>(List<T> list)
    {
        ConsoleKey consoleKey = Console.ReadKey().Key;

        switch (consoleKey)
        {
            case ConsoleKey.D1:
                return list[0];
            case ConsoleKey.D2:
                return list[1];
            case ConsoleKey.D3:
                return list[2];
            case ConsoleKey.D4:
                return list[3];
        }

        return default;
    }

    public static void DisplayProgramElementList<T>(List<T> list)
    {
        int count = 0;
        foreach (var item in list)
        {
            count++;
            Console.WriteLine($"{count}. {item}");
        }
    }

    private static void WritePromptToScreen (string promptText)
    {
        Console.WriteLine(promptText);
    }

    private static void WriteHeadingToScreeen(string heading)
    {
        Console.WriteLine(heading);
        Console.WriteLine(new string ('-',heading.Length));
        Console.WriteLine();
    }
}

