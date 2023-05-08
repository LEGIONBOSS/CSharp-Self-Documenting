using System;
using System.Collections.Generic;

namespace SelfDocumenting
{
    class TestClass
    {
        int field1;
        double field2;
        public long prop1 { get; set; }
        public List<int> prop2 { get; set; }
        public float Method1(int param1, string param2)
        {
            return 0;
        }
        public Dictionary<int, DateTime> Method2(int param1, string param2)
        {
            return new Dictionary<int, DateTime>();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(MDDocumentService.MakeMDDocumentation(new TestClass()));
            Console.ReadLine();
        }
    }
}
