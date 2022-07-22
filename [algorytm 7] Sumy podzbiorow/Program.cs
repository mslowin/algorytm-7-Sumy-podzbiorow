using System.Diagnostics;

namespace _algorytm_7__Sumy_podzbiorow
{
    internal static class Program
    {
        /// <summary>
        /// Lista wszystkich podzbiorów głównego zbioru (zakładając, że sam główny zbiór też jest swoim podzbiorem).
        /// </summary>
        private static readonly List<List<float>> _ListOfSubSets = new();



        private static void Main()
        {
            // List<float> Set = new() { 8, -1, 3, 4 }; // Set1
            List<float> Set = new() { -4, 5, 1, 0 }; // Set2
            //List<float> Set = new() {   1, 5, -6, 6, -6, 2, 4, -5, 8, 10, -3, 4, 5, 8, -10}; // Set3


            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();
            DoMath(Set);  // Obliczanie wszystkich możliwośći ustawienia liczb w tablicy głównej i jej pod tablicach
            stopwatch.Stop();

            List<List<float>> finalListOfSubSets = new();
            finalListOfSubSets.AddRange(_ListOfSubSets);

            //finalListOfSubSets = UsunJednoelementoweListy(finalListOfSubSets);  // FinalListOfSubSets nie ma zbiorów jednoelementowych

            List<float> results = ObliczSumyZbiorow(finalListOfSubSets);  // Obliczanie sum elementów w zbiorach

            WyswietlOutput(results, finalListOfSubSets);  // Wyświetlanie wyników
            Console.WriteLine(stopwatch.Elapsed);
        }

        /// <summary>
        /// Przeprowadza symulacje ustawiania liczb we wszystkich wariancjach bez powtórzeń na całym zbiorze i wszystkich jego podzbiorach.
        /// </summary>
        /// <param name="Set">Zbiór do przeprowadzenia obliczeń.</param>
        public static void DoMath(List<float> Set)
        {
            var dwumianIteracje = 0;  // reguluje ile liczb powinno być w nowo tworzonym subsecie, zmniejsza podstawę dwumianu NEwtona jeśli wszystkie wariacje poprzedniego dwumianu zostaną zaspokojone
            var wszystkieIteracje = 0;  // pomaga określić ile iteracji wydarzyło się od samego początku pętli

            for (int i = 0; ; i++)
            {
                wszystkieIteracje++;

                List<float> SubSet = new();

                List<float> SetPom = new();  // pomocniczy Set, który utrzymuje aktualnie przetwarzaną liczbę na pierwszym miejscu, dzięki czemu można iterować od dwumianIteracje
                SetPom.AddRange(Set);

                var item = SetPom[i];  // Umieszczanie aktualnie przetwarzanej liczby na pierwszym miejscu w SetPom
                SetPom.RemoveAt(i);
                SetPom.Insert(0, item);

                for (int j = dwumianIteracje; j < Set.Count; j++)  // dodawanie reszty liczb do list (która maleje wraz ze zmniejszaniem się podstawy dwumianu)
                {
                    SubSet.Add(SetPom[j]);
                }

                _ListOfSubSets.Add(SubSet);  // dodawanie SubSetu do listy SubSetów

                if (i == Set.Count - 1)  // jeśli i dociera do końca setu trzeba rozpocząć od początku
                {
                    i = -1;
                }
                if (dwumianIteracje == Set.Count)  // jeśli podstawa dwumianu dojdzie do zera (wszyskie możliwości się wyczerpały) - trzeba zakończyć funkcję
                {
                    break;
                }

                // jeśli ilość wszystkich operacji jest równa wynikowi dwumianu, to znaczy, że wszystkie możliwości ustawienia liczb,
                // dla danej długości zbioru, zostały wykorzystane i należy zmniejszyć miejsca o jedno
                if (wszystkieIteracje == DwumianNewtona(Set.Count, Set.Count - dwumianIteracje))
                {
                    dwumianIteracje++;
                    wszystkieIteracje = 0;
                }
            }
        }

        /// <summary>
        /// Oblicza sumy wartości we wszystkich zbiorach.
        /// </summary>
        /// <param name="finalListOfSubSets">Lista zbiorów.</param>
        /// <returns>Lista wyników.</returns>
        public static List<float> ObliczSumyZbiorow(List<List<float>> finalListOfSubSets)
        {
            List<float> results = new();
            float result = 0;

            foreach (List<float> subList in finalListOfSubSets)
            {
                foreach (float item in subList)
                {
                    result += item;
                    Console.Write(item + " ");
                }
                results.Add(result);
                Console.Write("\t\t" + result);
                Console.WriteLine();
                result = 0;
            }
            Console.WriteLine();

            return results;
        }

        /// <summary>
        /// Usuwa listy, które mają tylko jedną liczbę (Dla takich nie można obliczyć sumy - w sensie matematycznym).
        /// </summary>
        /// <param name="finalListOfSubSets">Lista zbiorów.</param>
        /// <returns>Lista zbiorów bez zbiorów jednoelementowych.</returns>
        public static List<List<float>> UsunJednoelementoweListy(List<List<float>> finalListOfSubSets)
        {
            foreach (List<float> subList in _ListOfSubSets)
            {
                if (subList.Count == 1)
                {
                    finalListOfSubSets.Remove(subList);
                }
            }
            return finalListOfSubSets;
        }

        /// <summary>
        /// Wyświetla wyniki.
        /// </summary>
        /// <param name="results">Lista sum zbiorów.</param>
        /// <param name="finalListOfSubSets">Lista zbiorów.</param>
        public static void WyswietlOutput(List<float> results, List<List<float>> finalListOfSubSets)
        {
            List<int> maxValueIndexes = new();

            for (int i = 0; i < results.Count; i++)
            {
                if (results[i] == results.Max())
                {
                    maxValueIndexes.Add(i);
                }
            }
            for (int i = 0; i < maxValueIndexes.Count; i++)
            {
                finalListOfSubSets[maxValueIndexes[i]].ForEach(x => Console.Write(x + " "));
                Console.WriteLine("\nSuma: " + results[maxValueIndexes[i]] + "\n");
            }
        }

        /// <summary>
        /// Oblicza wartość dwumanu Newtona.
        /// </summary>
        /// <param name="n">Suma wykładników (liczba wszystkich elementów w głównym zbiorze).</param>
        /// <param name="k">Podstawa (ilosć miejsc w które możemy wkładać obiekty ze zbioru).</param>
        /// <returns>Wartość dwumianu Newtona.</returns>
        public static double DwumianNewtona(int n, int k)
        {
            return silnia(n) / (silnia(k) * silnia(n - k));
        }

        /// <summary>
        /// Oblicza wartość silni dla podanej liczby.
        /// </summary>
        /// <param name="n">Liczba.</param>
        /// <returns>Wartość silni.</returns>
        public static double silnia(int n)
        {
            if (n > 1)
            {
                return silnia(n - 1) * n;
            }
            else
            {
                return 1;
            }
        }
    }
}