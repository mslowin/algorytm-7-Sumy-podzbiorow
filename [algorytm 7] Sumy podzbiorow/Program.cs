namespace _algorytm_7__Sumy_podzbiorow
{
    internal class Program
    {
        private static List<List<float>> _ListOfSubSets = new();






        static void Main(string[] args)
        {
            List<float> Set = new() { 8, -1, 3, 4 };
            var variants = Set.Count * Set.Count;

            //DoMath(Set, SubSet, ListOfSubSets, 0);

            var reg = 0;
            var wszystkieIteracje = 0;
            var dwumianIteracje = 0;

            for (int i = 0;  ; i++)
            {
                wszystkieIteracje++;

                List<float> SubSet = new();

                var mainNum = Set[i];
                SubSet.Add(mainNum);
                // i - indeks liczby ktorej nie chcemy w subsecie, bo juz tam jest

                for (int j = 0; j < Set.Count; j++) // tu jest problem bo dla ostatniej liczby juz nie sprawdza
                {
                    SubSet.Add(Set[j]);
                    
                    if (SubSet.Count > Set.Count - reg)    // tu coś nie tak
                    {
                        SubSet.Remove(Set[i]);
                    }
                    //if(Set[j] == Set[i])   // !!!!!!!!!!!!!!!!!!!!!!!
                    //{
                    //    SubSet.RemoveAt(j + 1);
                    //}
                }

                SubSet.Sort();
                _ListOfSubSets.Add(SubSet);


                // dwumian newtona

                if (i == 3)
                {
                    i = -1;
                }
                if (wszystkieIteracje == DwumianNewtona(Set.Count, Set.Count - dwumianIteracje))
                {
                    reg++;
                    dwumianIteracje++;
                }

                //if (reg == 4)
                //{
                //    reg = 0;
                //}

            }

            //_ListOfSubSets = _ListOfSubSets.Distinct().ToList();

            foreach (List<float> subList in _ListOfSubSets)
            {
                foreach (float item in subList)
                {
                    Console.WriteLine(item);
                }
            }


        }




        public static int DwumianNewtona(int n, int k)
        {
            return silnia(n) / (silnia(k) * silnia(n - k));
        }





        public static int silnia(int n)
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

        //public static void DoMath(List<float> Set, List<float> SubSet, List<List<float>> ListOfSubSets, int iteration)
        //{
        //    for (int i = iteration; i < Set.Count; i++)
        //    {
        //        var mainNum = Set[i];
        //        for (int j = 0; j < Set.Count; j++)
        //        {

        //        }
        //    }
        //}
    }
}