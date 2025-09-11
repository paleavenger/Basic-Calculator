class CalculatorProgram
{ 
    static double ApplyBinaryOperation(double a, string op, double b)
    {
        switch (op)
        {
            case "+": return a + b;
            case "-": return a - b;
            case "*": return a * b;
            case "/":
                if (b == 0)
                {
                    Console.WriteLine("На ноль делить нельзя");
                    return a;
                }
                return a / b;
            case "%": return a % b;
            case "^": return Math.Pow(a, b); 
        }
        return a;
    }
    
    static void Main()
    {
        double currentValue = 0;
        double memory = 0;
        bool isNewInput = true;
        string pendingOperation = null;
        Console.WriteLine("<< Программа калькулятор >>");
        Console.WriteLine("Поддерживаемые операции: +, -, *, /, %, ^, 1/x, x^2, sqrt, M+, M-, MR, MC. Напишите exit для выхода");
        Console.WriteLine("Перед вводом один раз нажмите Enter");
        Console.WriteLine("----------------------------------");
        
        while (true)
        {
            string input;
            if (pendingOperation == null)
            {
                Console.Write("Введите число или операцию: ");
            }
            if (pendingOperation != null)
            {
                Console.Write("Введите второе число для операции " + pendingOperation + ": ");
            }
            input = Console.ReadLine();
            if (input.ToLower() == "exit")
            {
                break;
            }
            if (double.TryParse(input, out double number))
            {
                if (pendingOperation != null)
                {
                    currentValue = ApplyBinaryOperation(currentValue, pendingOperation, number);
                    pendingOperation = null;
                    Console.WriteLine($"Результат: {currentValue}");
                }
                else
                {
                    currentValue = number;
                    Console.WriteLine($"Текущее значение: {currentValue}");
                }
            }
            else
            {
                switch (input)
                {
                    case "+":
                    case "-":
                    case "*":
                    case "/":
                    case "%":
                    case "^":
                        pendingOperation = input;
                        break;
                    case "1/x":
                        if (currentValue == 0)
                        {
                            Console.WriteLine("Деление на ноль невозможно");
                        }
                        else
                        {
                            currentValue = 1.0 / currentValue;
                            Console.WriteLine($"результат: {currentValue}");
                        }
                        isNewInput = true;
                        break;
                    case "x^2":
                        currentValue = Math.Pow(currentValue, 2);
                        Console.WriteLine($"Текущее значение: {currentValue}");
                        isNewInput = true;
                        break;
                    case "sqrt":
                        if (currentValue < 0)
                        {
                            Console.WriteLine("Нельзя извлечь корень из отриц числа");
                        }
                        else
                        {
                            currentValue = Math.Sqrt(currentValue);
                            Console.WriteLine($"результат: {currentValue}");
                        }
                        isNewInput = true;
                        break;
                    case "M+":
                        memory += currentValue;
                        Console.WriteLine($"Значение {currentValue} добавлено в память. Память: {memory}");
                        break;
                    case "M-":
                        memory -= currentValue;
                        Console.WriteLine($"Значение {currentValue} вычтено из памяти. Память: {memory}");
                        break;
                    case "MR":
                        currentValue = memory;
                        Console.WriteLine($"Восстановлено из памяти: {currentValue}");
                        isNewInput = true;
                        break;
                    case "MC":
                        memory = 0;
                        Console.WriteLine("Память очищена");
                        break;
                    default:
                        Console.WriteLine("нужна операция либо число");
                        break;

                }
            }
        }
        Console.WriteLine("<< Работа калькулятора завершена >>");
    }
}