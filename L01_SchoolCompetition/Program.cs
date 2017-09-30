namespace L01_SchoolCompetition
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        public static void Main()
        {
            var scores = new Dictionary<string, int>();
            var categories = new Dictionary<string, SortedSet<string>>();

            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                var args = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var name = args[0];
                var category = args[1];
                var points = int.Parse(args[2]);

                if (!scores.ContainsKey(name))
                {
                    scores[name] = 0;
                }
                if (!categories.ContainsKey(name))
                {
                    categories[name] = new SortedSet<string>();
                }

                scores[name] += points;
                categories[name].Add(category);
            }

            var orderedStudent = scores
                .OrderByDescending(kvp => kvp.Value)
                .ThenBy(kvp => kvp.Key);

            foreach (var studentKvp in orderedStudent)
            {
                var name = studentKvp.Key;
                var studentScores = studentKvp.Value;
                var studentCategories = categories[name];

                var categoryText = $"[{string.Join(", ", studentCategories)}]";

                Console.WriteLine($"{name}: {studentScores} {categoryText}");
            }
        }
    }
}