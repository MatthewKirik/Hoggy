﻿using System;

namespace TestConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Generator generator = new Generator();
            generator.InitializeHierarchy(1, 2, 3, 3, 2);
            //generator.CreateGroup(10);
            //generator.TestFiles();
            while (true)
                Console.ReadLine();
        }
    }
}
