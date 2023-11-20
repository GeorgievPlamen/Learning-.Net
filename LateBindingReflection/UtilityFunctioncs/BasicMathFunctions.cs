using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityFunctioncs
{
    [Information(Description = "Basic math functions")]
    public class BasicMathFunctions
    {
        [Information(Description = "Divide")]
        public double DivideOperation(double number1, double number2)
        {
            return number1 / number2;
        }
        [Information(Description = "Multiply")]
        public double MultiplyOperation(double number1, double number2)
        {
            return number1 * number2;
        }
    }
}
