using Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode
{
    class Challenges
    {

        public void Day1()
        {
            Console.WriteLine("Day1: ");
            // read input file
            string[] nums = File.ReadAllLines("D:\\payno\\Documents\\GitHub\\AdventOfCode2020\\AdventOfCode\\Day1Input.txt");
            var resultFound = false;
            // find2 that add upto 2020

            int[] numInts = new int[nums.Length];

            for(int i = 0; i < nums.Length; i++)
            {
                numInts[i] = int.Parse(nums[i]);
            }

            foreach (var num in numInts)
            {
                if (num == 0 || num > 2020)
                {
                    continue;
                }

                foreach (var num2 in numInts)
                {
                    if (num2 == 0 || num2 > 2020 || num2 == num)
                    {
                        continue;
                    }

                    if ((num + num2) == 2020)
                    {
                        Console.WriteLine($"2 num Answer: {num * num2}, num1: {num}, num2: {num2}");
                    }

                    foreach(var num3 in numInts)
                    {
                        if (num2 == 0 || num2 > 2020 || num2 == num3 || num == num3)
                        {
                            continue;
                        }
                        if ((num + num2 + num3) == 2020)
                        {
                            Console.WriteLine($"3 num Answer: {num * num2 * num3}, num1: {num}, num2: {num2}, num3:{num3}");
                            resultFound = true;
                        }
                    }
                }

                if (resultFound) break;
            }
            //multiply them together
        }


        public void Day2()
        {
            List<PasswordPolicy> passwordList = new List<PasswordPolicy>();

            IEnumerable<string> rawLines = File.ReadLines("D:\\payno\\Documents\\GitHub\\AdventOfCode2020\\AdventOfCode\\Day2Input.txt");

            // get passwords
            foreach(var line in rawLines)
            {
                PasswordPolicy p = new PasswordPolicy();

                var lower = line.Split('-');
                p.LowerLimit = int.Parse(lower[0]);

                var rest = lower[1].Split(' ');

                p.UpperLimit = int.Parse(rest[0]);

                p.RequiredCharacter = char.Parse(rest[1].Remove(1,1));

                p.password = rest[2];

                passwordList.Add(p);
            }


            int i = 0;
            //get how many are valid
            foreach(var p in passwordList)
            {
                p.ComputeIsValid();
                if (p.IsValid) i++;
            }

            Console.WriteLine($"Valid Passwords: {i}");




            

        }


        public void Day3()
        {
            var lines = File.ReadAllLines("D:\\payno\\Documents\\GitHub\\AdventOfCode2020\\AdventOfCode\\Day3Input.txt");
            int trees = 0;

            char tree = '#';
            bool firstDone = false;


            int rightPos = 0;
           // int rightInc = 3;
            int[] rightIncs = new int[] { 1, 3, 5, 7, 1};
            bool skip = false;
            long total = 1;
            for(int j =0; j < rightIncs.Length; j++)
            {
                if (j == 4) skip = true;
                for(int i=0; i< lines.Length; i++)
                {
                    if(firstDone == false)
                    {
                        firstDone = true;
                        rightPos += rightIncs[j];
                        if (lines[i].StartsWith(tree)) trees++;
                        continue;
                    }

                    if(!skip)
                    {
                        while(lines[i].Length <= (rightPos + 1))
                        {
                            var a = lines[i];
                            var b = a + a;
                   
                            lines[i] = b;
                        }
                
                        if (lines[i].Substring(rightPos, 1).Equals(tree.ToString())) trees++;
                        rightPos += rightIncs[j];
                    }

                    if (j == 4) skip = !skip;
                  
                }
                Console.WriteLine($"RightInc: {rightIncs[j]}, Trees: {trees}");
                total = total * trees;
                trees = 0;
                rightPos = 0;
                firstDone = false;
            }

            Console.WriteLine($"Total: {total}");
        }
    }
}
