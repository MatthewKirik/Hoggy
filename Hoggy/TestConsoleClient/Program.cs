﻿using System;

namespace TestConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Generator generator = new Generator();
            //generator.DeletionTest(1, 2, 3, 3, 2);
            generator.InitializeHierarchy(2, 2, 3, 3, 2);
            //generator.TeamHierarchy(4, 1, 3, 3, 2);
            //generator.CreateGroup(10);
            //generator.TestFiles();
           // generator.EditionTest();
            while (true)
                Console.ReadLine();
        }
    }
}
