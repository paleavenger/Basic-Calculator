class CalculatorProgram
{ 
    static double ApplyBinaryOperation(double a, string op, double b) // Метод выполнения бинарных операций
    {
        switch (op)
        {
            case "+": return a + b;
            case "-": return a - b;
            case "*": return a * b;
            case "/":
                if (b == 0) // ограничение на ноль
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
        double currentValue = 0; // текущее значение калькулятора
        double memory = 0; // значение памяти
        bool isNewInput = true; // флаг для ввода второго числа
        string pendingOperation = null; // хранилище бинарной операции
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
            if (double.TryParse(input, out double number)) // конвертация инпута в double
            {
                if (pendingOperation != null)
                {
                    currentValue = ApplyBinaryOperation(currentValue, pendingOperation, number); //если бинарная операция ожидаема вызываем метод
                    pendingOperation = null;
                    Console.WriteLine($"Результат: {currentValue}");
                }
                else //если нет просто сохраняем число
                {
                    currentValue = number;
                    Console.WriteLine($"Текущее значение: {currentValue}");
                }
            }
            else
            {
                switch (input) // если введено не число, обрабатываем как операцию
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
