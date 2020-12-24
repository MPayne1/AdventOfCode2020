using Helpers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Text.RegularExpressions.RegexOptions;
using Bags = System.Collections.Immutable.ImmutableDictionary<string, int>;

namespace AdventOfCode
{
    class Challenges
    {

        public void Day1()
        {
            Console.WriteLine("Day1: ");
            // read input file
            string[] nums = File.ReadAllLines("D:\\payno\\Documents\\GitHub\\AdventOfCode2020\\AdventOfCode\\InputData\\Day1Input.txt");
            var resultFound = false;
            // find2 that add upto 2020

            int[] numInts = new int[nums.Length];

            for (int i = 0; i < nums.Length; i++)
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

                    foreach (var num3 in numInts)
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

            IEnumerable<string> rawLines = File.ReadLines("D:\\payno\\Documents\\GitHub\\AdventOfCode2020\\AdventOfCode\\InputData\\Day2Input.txt");

            // get passwords
            foreach (var line in rawLines)
            {
                PasswordPolicy p = new PasswordPolicy();

                var lower = line.Split('-');
                p.LowerLimit = int.Parse(lower[0]);

                var rest = lower[1].Split(' ');

                p.UpperLimit = int.Parse(rest[0]);

                p.RequiredCharacter = char.Parse(rest[1].Remove(1, 1));

                p.password = rest[2];

                passwordList.Add(p);
            }


            int i = 0;
            //get how many are valid
            foreach (var p in passwordList)
            {
                p.ComputeIsValid();
                if (p.IsValid) i++;
            }

            Console.WriteLine($"Valid Passwords: {i}");






        }


        public void Day3()
        {
            Console.WriteLine("Day: 3");
            var lines = File.ReadAllLines("D:\\payno\\Documents\\GitHub\\AdventOfCode2020\\AdventOfCode\\InputData\\Day3Input.txt");
            int trees = 0;

            char tree = '#';
            bool firstDone = false;


            int rightPos = 0;
            // int rightInc = 3;
            int[] rightIncs = new int[] { 1, 3, 5, 7, 1 };
            bool skip = false;
            long total = 1;
            for (int j = 0; j < rightIncs.Length; j++)
            {
                if (j == 4) skip = true;
                for (int i = 0; i < lines.Length; i++)
                {
                    if (firstDone == false)
                    {
                        firstDone = true;
                        rightPos += rightIncs[j];
                        if (lines[i].StartsWith(tree)) trees++;
                        continue;
                    }

                    if (!skip)
                    {
                        while (lines[i].Length <= (rightPos + 1))
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
            var data = File.ReadAllText("D:\\payno\\Documents\\GitHub\\AdventOfCode2020\\AdventOfCode\\InputData\\Day4Input.txt");
            var passports = data.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);

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
                if (missingComponents == 0)
                {
                    isValid = true;
                }
                else { isValid = false; }

                missingComponents = 0;

                //more validation
                char[] separator = new char[] { ' ', '\r' };
                var infos = passport.Split(separator);
                if (isValid == true)
                {
                    foreach (var info in infos)
                    {
                        if (isValid && info.Contains(birthYear))
                        {
                            var b = info.Split(':');
                            int year = int.Parse(b[1]);

                            if (1920 <= year && year <= 2002)
                            {
                                isValid = true;
                            }
                            else
                            {
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
                                if (h.Remove(0, 1).Length == 6)
                                {
                                    isValid = true;
                                }
                                else
                                {
                                    isValid = false;
                                }
                            }
                            else
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
                                    if (150 <= cmI && cmI <= 193)
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

                            }
                            else if (inf.Contains("in"))
                            {
                                var inI = inf.Split("in");
                                if (inI[0].StartsWith(':')) inI[0] = inI[0].Remove(0, 1);
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
                            }
                            else
                            {
                                isValid = false;
                            }

                        }

                        if (isValid && info.Contains(eyeColour))
                        {
                            var e = info.Split(':')[1];

                            if (e.Equals("amb") || e.Equals("blu") || e.Equals("brn") || e.Equals("gry") || e.Equals("grn") || e.Equals("hzl") || e.Equals("oth"))
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
                                try
                                {
                                    var p = int.Parse(b[1]);
                                    isValid = true;
                                }
                                catch (Exception e)
                                {
                                    isValid = false;
                                }
                            }
                            else
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

        public void Day5()
        {
            Console.WriteLine("Day: 5");
            var seats = File.ReadAllLines("D:\\payno\\Documents\\GitHub\\AdventOfCode2020\\AdventOfCode\\InputData\\Day5Input.txt");
            int maxId = 0;
            bool[] seatsFilled = new bool[900];
            foreach (var seat in seats)
            {
                int front = 0;
                int back = 127;
                int left = 0;
                int right = 7;
                int seatId = 0;
                var s = seat.ToCharArray();
                foreach (var i in s)
                {
                    switch (i)
                    {
                        case 'F':
                            back = (front + back + 1) / 2 - 1;
                            break;
                        case 'B':
                            front = (front + back + 1) / 2;
                            break;
                        case 'L':
                            right = (left + right + 1) / 2 - 1;
                            break;
                        case 'R':
                            left = (left + right + 1) / 2;
                            break;
                        default: break;
                    }
                }
                seatId = (front * 8) + left;

                if (seatId > maxId) maxId = seatId;
                seatsFilled[seatId] = true;
            }
            Console.WriteLine($"Max seatId: {maxId}");


            for (int j = 1; j < seatsFilled.Length - 1; j++)
            {

                if (seatsFilled[j + 1] && seatsFilled[j - 1] && (false == seatsFilled[j]))
                {
                    Console.WriteLine($"My Seat: {j}");
                }
            }
        }

        public void Day6()
        {
            Console.WriteLine("Day: 6");
            // read input
            var data = File.ReadAllText("D:\\payno\\Documents\\GitHub\\AdventOfCode2020\\AdventOfCode\\InputData\\Day6Input.txt");
            var answers = data.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            var formattedAns = new List<string>();
            for (int i = 0; i < answers.Count(); i++)
            {
                var a = answers[i].Replace("\r\n", ";");
                formattedAns.Add(a);
            }

            //part1
            int total = 0;
            foreach (var ans in formattedAns)
            {
                List<char> countedChar = new List<char>();
                var ac = ans.ToCharArray();
                foreach (var c in ac)
                {
                    if (c.Equals(';') == false)
                    {
                        if (false == countedChar.Contains(c))
                        {
                            countedChar.Add(c);
                            total++;
                        }
                    }
                }

                //count each different char once
                //add each to total
            }

            Console.WriteLine($"Total: {total}");


            //part2
            int allYes = 0;
            foreach (var an in formattedAns)
            {
                var indiv = an.Split(';').ToList();
                //char[] alpha = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'a', 'r', 's', 't', 'u', 'v', 'x', 'y', 'z' };

                var n = indiv.First().Count(c => indiv.TrueForAll(a => a.Contains(c)));
                allYes += n;
            }
            Console.WriteLine($"Allyes: {allYes}");

        }

        const string myBag = "shiny gold";
        public void Day7()
        {
            Console.WriteLine("Day: 7");
            var rules = File.ReadLines("D:\\payno\\Documents\\GitHub\\AdventOfCode2020\\AdventOfCode\\InputData\\Day7Input.txt")
        .ToImmutableDictionary(
          line => Regex.Match(line, @"^(\w+ \w+)", Compiled).Groups[1].Value,
          line => line.Contains("no other bags.")
            ? Bags.Empty
            : Regex.Matches(line, @"(\d+) (\w+ \w+) bags?[,.]\s?", Compiled)
              .ToImmutableDictionary(
                match => match.Groups[2].Value,
                match => int.Parse(match.Groups[1].Value)));

            Console.WriteLine(GetContainerBagsCount(myBag));
            Console.WriteLine(GetContainedBagsCount(myBag));

            int GetContainerBagsCount(string bag)
            {
                bool IsBagContainedIn(Bags bags) =>
                  bags.ContainsKey(bag) ||
                  bags.Keys.Any(b => IsBagContainedIn(rules[b]));

                return rules.Values.Count(IsBagContainedIn);
            }

            int GetContainedBagsCount(string bag) =>
               rules[bag].Sum(b => b.Value + b.Value * GetContainedBagsCount(b.Key));

        }

        public void Day8()
        {
            Console.WriteLine("Day: 8");
            var instructions = File.ReadAllLines("D:\\payno\\Documents\\GitHub\\AdventOfCode2020\\AdventOfCode\\InputData\\Day8Input.txt");

            List<int> visited = new List<int>();
            bool finished = false;
            int total = 0;
            string[] cached = new string[instructions.Length];
            for (int k = 0; k < instructions.Length; k++)
            {
                cached[k] = instructions[k];
            }

            for (int j = 0; j < instructions.Count(); j++)
            {
                if (!finished)
                {
                    var cachedInstr = instructions[j];
                    if (instructions[j].Contains("jmp"))
                    {
                        instructions[j] = instructions[j].Replace("jmp", "nop");
                    }
                    else if (instructions[j].Contains("nop"))
                    {
                        instructions[j] = instructions[j].Replace("nop", "jmp");
                    }

                    total = 0;
                    bool infinite = false;
                    for (int i = 0; i < instructions.Count();)
                    {
                        if (infinite == false)
                        {
                            if (false == visited.Contains(i))
                            {
                                var ins = instructions[i].Split(' ');
                                switch (ins[0])
                                {
                                    case "acc":
                                        visited.Add(i);
                                        var val = int.Parse(ins[1].Remove(0, 1));
                                        if (ins[1].Contains('+'))
                                        {
                                            total += val;
                                            i++;
                                        }
                                        else if (ins[1].Contains('-'))
                                        {
                                            total -= val;
                                            i++;
                                        }
                                        break;
                                    case "jmp":
                                        visited.Add(i);
                                        var jmpVal = int.Parse(ins[1].Remove(0, 1));
                                        if (ins[1].Contains('+'))
                                        {
                                            i += jmpVal;
                                        }
                                        else if (ins[1].Contains('-'))
                                        {
                                            i -= jmpVal;
                                        }
                                        break;

                                    case "nop":
                                        visited.Add(i);
                                        i++;
                                        break;
                                    default: break;
                                }
                                finished = true;
                            }
                            else
                            {
                                //Console.WriteLine($"Total: {total}");
                                finished = false;
                                infinite = true;
                                break;
                            }
                        }
                    }
                    visited.Clear();
                    instructions[j] = cachedInstr;
                }
                else
                {
                    Console.WriteLine($"Fixed Total: {total}");
                }
            }

            Console.WriteLine($"End total: {total}");
        }

        public void Day9()
        {
            Console.WriteLine("Day: 9");
            var numsStrings = File.ReadAllLines("D:\\payno\\Documents\\GitHub\\AdventOfCode2020\\AdventOfCode\\InputData\\Day9Input.txt").ToList();
            List<long> nums = new List<long>();
            numsStrings.ForEach(x => nums.Add(long.Parse(x)));

            //var preamble = nums.Take(25);
            long invalidAns = 0;
            var resultFound = false;
            for (int i = 25; i < nums.Count(); i++)
            {
                resultFound = false;
                var numI = nums[i];
                for (int j = 0; j < 25 + i; j++)
                {
                    var numJ = nums[j];
                    if (numJ < numI)
                    {
                        for (int k = 0; k < (25 + i); k++)
                        {
                            var numK = nums[k];
                            if (numK < numI)
                            {
                                if (numJ + numK == numI && numJ != numK)
                                {
                                    resultFound = true;
                                }
                                else
                                {
                                    var wrong = numJ + numK;
                                }
                            }
                        }
                    }
                }

                if (resultFound == false)
                {
                    Console.WriteLine($"First unanswered: {numI}");
                    invalidAns = numI;
                    break;
                }


            }


            bool sumAnsFound = false;
            for (int i = 0; i < nums.Count() - 1; i++)
            {
                if (sumAnsFound == false)
                {
                    for (int j = 1; j < nums.Count() - 1; j++)
                    {
                        if (sumAnsFound == false)
                        {
                            var temp = nums.GetRange(i, Math.Abs(j - i));
                            if (temp.Sum() == invalidAns)
                            {
                                Console.WriteLine($"Sequence found, Max: {temp.Max()}, Min: {temp.Min()}");
                                sumAnsFound = true;
                                break;
                            }

                        }
                    }
                }
            }
        }

        public void Day10()
        {
            //Console.WriteLine("Day: 10");
            //var numsStrings = File.ReadAllLines("D:\\payno\\Documents\\GitHub\\AdventOfCode2020\\AdventOfCode\\InputData\\Day10Input.txt").ToList();
            //List<int> nums = new List<int>();
            //numsStrings.ForEach(x => nums.Add(int.Parse(x)));
            //var orderedNums = nums.OrderBy(x => x).ToList();

            //int jumps1 = 0;
            //int jumps3 = 0;
            //int outputJoltage = 0;
            //orderedNums.Add(orderedNums.Max() + 3);
            //for (int i = 0; i < orderedNums.Count(); i++)
            //{
            //    if ((orderedNums[i]) == outputJoltage + 1)
            //    {
            //        jumps1++;
            //        outputJoltage += 1; ;
            //    }
            //    else if (orderedNums[i] == outputJoltage + 2)
            //    {
            //        outputJoltage += 2;
            //    }
            //    else if (orderedNums[i] == outputJoltage + 3)
            //    {
            //        jumps3++;
            //        outputJoltage += 3;
            //    }
            //}

            //Console.WriteLine($"1 jumps: {jumps1}, 3 jumps: {jumps3}");

            //List<int> sol = new List<int>(2000);
            //orderedNums.ForEach(x => sol.Add(x));
            //for (int i = 0; i < orderedNums.Count() - 1; i++)
            //{
            //    var line = orderedNums[i];
            //    sol[line] = 0;
            //    if (sol.Contains(line - 1))
            //    {
            //        sol[line] += sol[line - 1];
            //    }
            //    if (sol.Contains(line - 2))
            //    {
            //        sol[line] += sol[line - 2];
            //    }
            //    if (sol.Contains(line - 3))
            //    {
            //        sol[line] += sol[line - 3];
            //    }
            //}

            //Console.WriteLine(sol[orderedNums.Max()]);

            string[] input = File.ReadAllLines("D:\\payno\\Documents\\GitHub\\AdventOfCode2020\\AdventOfCode\\InputData\\Day10Input.txt");
            List<long> inputs = (Array.ConvertAll(input, s => Int64.Parse(s))).ToList();
            inputs.Sort();

            Console.WriteLine("########## Day 10 2020 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(inputs)}");
            inputs.Add(inputs.Last() + 3);
            inputs.Insert(0, 0);
            Console.WriteLine($"Part two solution: {SolvePartTwo(inputs.ToArray())}");
            Console.WriteLine("#################################");
        }


        private static long SolvePartOne(List<long> inputs)
        {
            int jolt_1_diff = 1;
            int jolt_3_diff = 1;
            for (int i = 0; i < inputs.Count - 1; i++)
            {
                long temp1 = inputs[i];
                long temp2 = inputs[i + 1];
                if (temp2 - temp1 == 1)
                {
                    jolt_1_diff++;
                }
                else if (temp2 - temp1 == 3)
                {
                    jolt_3_diff++;
                }
            }

            return jolt_1_diff * jolt_3_diff;
        }

        /*
            not my proudest solution, thanks reddit on help
        
            eg for 3,4,6,7
            - 7 has no children and would have 1 path(7->10)
            - 6 has one child(7): it has 1 path (6->7->10)
            - 4 can go to 6 or 7: it has 2 paths (4->6->7->10, 4->7->10)
            - 3 can go to 4 or 6: it has 3 paths (2 via 4: 3->4->6->7->10, 3->4->7->10 and 1 via 6 3->6->7->10).
            - 0 can only reach 3: it has 3 paths.
             
             https://www.reddit.com/r/adventofcode/comments/kadp4g/why_does_this_code_work_and_what_does_it_do/gf9pixm?utm_source=share&utm_medium=web2x&context=3
        */
        static Dictionary<long, long> resultSet = new Dictionary<long, long>();
        private static long SolvePartTwo(long[] inputs)
        {
            if (resultSet.ContainsKey(inputs.Length))
            {
                return resultSet[inputs.Length];
            }

            if (inputs.Length == 1)
            {
                return 1;
            }

            long total = 0;
            for (int i = 1; i < inputs.Length; i++)
            {
                if (inputs[i] - inputs[0] <= 3)
                {
                    var temp = inputs.ToList().GetRange(i, inputs.Count() - i);
                    total += SolvePartTwo(temp.ToArray());
                }
                else
                {
                    break;
                }
            }

            resultSet.Add(inputs.Length, total);

            return total;
        }

    }
}

   