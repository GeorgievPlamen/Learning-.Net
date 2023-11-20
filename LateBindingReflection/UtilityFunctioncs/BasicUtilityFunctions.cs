namespace UtilityFunctioncs
{
    [Information(Description = "This class contains basic utility methods.")]
    public class BasicUtilityFunctions
    {
        [Information(Description = "Writes welcome msg")]
        public string WriteWelcomeMessage()
        {
            return "Welcome to basic utility class";
        }
        [Information(Description = "Adds two numbers")]

        public int IntergerPlusInterger(int operand1, int operand2)
        {
            return operand1 + operand2;
        }
        [Information(Description = "Length of string")]

        public int GetStringLength(string stringValue)
        {
            return stringValue.Length;
        }
    }
}