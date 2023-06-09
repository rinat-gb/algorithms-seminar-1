// я так думаю что тут получается линейная сложность, O(n)
// потому что вермя вычисления возможных вариантов растёт линейно
// и зависит от количества кубиков и количества граней на них
//
using System;
using System.Diagnostics;

namespace dices;

class Program
{
    const int MAX_DICES = 9;
    const int MAX_FACETS = 9;

    static void Main(string[] args)
    {
        int k;

#region get dices count
        do
        {
            Console.Write("Введите количество игральных костей: ");
            k = int.Parse(Console.ReadLine() ?? "0");

            if (k == 0)
            {
                Console.WriteLine("Количество игральных костей не может быть нулём!\n");
                continue;
            }
            if (k > MAX_DICES)
            {
                Console.WriteLine(String.Format("Количество игральных костей не может быть больше чем {0}!\n", MAX_DICES));
                continue;
            }
            break;
        } while (true);
#endregion

        int n;

#region get dice edges count
        do
        {
            Console.Write("Введите количество граней на игральных костях: ");
            n = int.Parse(Console.ReadLine() ?? "0");

            if (n == 0)
            {
                Console.WriteLine("Количество граней не может быть нулём!\n");
                continue;
            }
            if (n > MAX_FACETS)
            {
                Console.WriteLine(String.Format("Количество граней не может быть больше чем {0}!\n", MAX_FACETS));
                continue;
            }
            break;
        } while (true);
#endregion

        // One dice can roll exactly as many times as it has faces,
        // two dices can roll exactly twice as many as one does, and so on.
        int numberOfVariants = Convert.ToInt32(Math.Pow(n, k));

        Console.WriteLine(String.Format("При количестве игральных костей {0} и на каждой по {1} граней доступно {2} вариантов",
                                        k, n, numberOfVariants));
        Console.Write("Нажмите [Enter] для просмотра этих вариантов...");
        Console.ReadLine();
        Console.WriteLine();

        string variantString = new string('1', k);
        int variantValue = 0;

#region convert from string to integer with radix
        for (int i = k - 1; i >= 0; i--)
        {
            int tmp = "0123456789".IndexOf(variantString[i]) * Convert.ToInt32(Math.Pow(n + 1, k - (i + 1)));
            variantValue += tmp;
        }
#endregion

        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();

        for (int i = 0; i < numberOfVariants; i++)
        {
            do
            {
                variantString = String.Empty;
                int tmpCurrValue = variantValue;

#region convert from integer to string with radix
                while (tmpCurrValue > 0)
                {
                    int idx = tmpCurrValue % (n + 1);
                    variantString = "0123456789"[idx] + variantString;
                    tmpCurrValue /= (n + 1);
                }
#endregion

                ++variantValue;

                if (variantString.IndexOf('0') == -1)
                    break;

            } while (true);

            Console.WriteLine(variantString);
        }

        stopWatch.Stop();
        Console.WriteLine(String.Format("\nВсе варианты просчитались за {0} миллисекунд", stopWatch.ElapsedMilliseconds));
    }
}
