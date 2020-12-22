﻿using Helpers;
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
            Console.WriteLine("Day: 2");
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
            Console.WriteLine("Day: 3");
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


        public void Day4()
        {
            Console.WriteLine("Day: 4");
            // read input
            var data = File.ReadAllText("D:\\payno\\Documents\\GitHub\\AdventOfCode2020\\AdventOfCode\\Day4Input.txt");
            var passports = data.Split(new string[] { "\r\n\r\n" },StringSplitOptions.RemoveEmptyEntries);

            int validPassports = 0;

            int missingComponents = 0;
            string birthYear = "byr";
            string issueYear = "iyr";
            string expirationYear = "eyr";
            string hairColour = "hcl";
            string height = "hgt";
            string eyeColour = "ecl";
            string passportId = "pid";
            string countryId = "cid";

            foreach (var passport in passports)
            {

                //basic validation
                if (false == passport.Contains(birthYear)) missingComponents++;
                if (false == passport.Contains(issueYear)) missingComponents++;
                if (false == passport.Contains(expirationYear)) missingComponents++;
                if (false == passport.Contains(hairColour)) missingComponents++;
                if (false == passport.Contains(height)) missingComponents++;
                if (false == passport.Contains(eyeColour)) missingComponents++;
                if (false == passport.Contains(passportId)) missingComponents++;

                var isValid = false;
                if (missingComponents == 0) {
                    isValid = true;
                } else { isValid = false; }

                missingComponents = 0;

                //more validation
                char[] separator = new char[] { ' ', '\r' };
                var infos = passport.Split(separator);
                if(isValid == true)
                {
                    foreach (var info in infos)
                    {
                        if (isValid && info.Contains(birthYear))
                        {
                            var b = info.Split(':');
                            int year = int.Parse(b[1]);

                            if (1920 <= year && year <= 2002) {
                                isValid = true;
                            } else {
                                isValid = false;
                            }
                        }

                        if (isValid && info.Contains(issueYear))
                        {
                            var b = info.Split(':');
                            int year = int.Parse(b[1]);
                            if (2010 <= year && year <= 2020) { isValid = true; } else { isValid = false; }
                        }

                        if (isValid && info.Contains(expirationYear))
                        {
                            var b = info.Split(':');
                            int year = int.Parse(b[1]);
                            if (2020 <= year && year <= 2030) { isValid = true; } else { isValid = false; }
                        }

                        if (isValid && info.Contains(hairColour))
                        {
                            var h = info.Split(':')[1];

                            if (h.StartsWith('#'))
                            {
                                if(h.Remove(0, 1).Length == 6)
                                {
                                    isValid = true;
                                }
                                else
                                {
                                    isValid = false;
                                }
                            }else
                            {
                                isValid = false;
                            }
                        }

                        if (isValid && info.Contains(height))
                        {

                            var inf = info.Remove(0, 4);
                            if (inf.Contains("cm"))
                            {
                                var cm = inf.Split("cm");
                                if (cm[0].StartsWith(':')) cm[0] = cm[0].Remove(0, 1);
                                try
                                {
                                    var cmI = int.Parse(cm[0]);
                                    if(150 <= cmI && cmI <= 193)
                                    {
                                        isValid = true;
                                    }
                                    else
                                    {
                                        isValid = false;
                                    }
                                }
                                catch(Exception e)
                                {
                                    isValid = false;
                                }
                            
                            } else if (inf.Contains("in"))
                            {
                                var inI = inf.Split("in");
                                if(inI[0].StartsWith(':')) inI[0] =inI[0].Remove(0, 1);
                                try
                                {
                                    var cmI = int.Parse(inI[0]);
                                    if (59 <= cmI && cmI <= 76)
                                    {
                                        isValid = true;
                                    }
                                    else
                                    {
                                        isValid = false;
                                    }
                                }
                                catch (Exception e)
                                {
                                    isValid = false;
                                }
                            } else
                            {
                                isValid = false;
                            }

                        }

                        if (isValid && info.Contains(eyeColour))
                        {
                            var e = info.Split(':')[1];
                        
                            if(e.Equals("amb") || e.Equals("blu") || e.Equals("brn") || e.Equals("gry") || e.Equals("grn") || e.Equals("hzl") || e.Equals("oth"))
                            {
                                isValid = true;
                            }
                            else
                            {
                                isValid = false;
                            }
                        }

                        if (isValid && info.Contains(passportId))
                        {
                            var b = info.Split(':');
                            
                            if (b[1].Length == 9)
                            {
                                try { 
                                    var p = int.Parse(b[1]);
                                    isValid = true;
                                }
                                catch(Exception e)
                                {
                                    isValid = false;
                                }
                            } else
                            {
                                isValid = false;
                            }

                            
                        }
                    }

                    if (isValid) validPassports++;
                }
                
            }
            Console.WriteLine($"Valid Passports: {validPassports}");
        }
    }
}
