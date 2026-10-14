using System;
using System.Collections.Generic;
using System.Linq;

// Task 1.1: Record tanımı[cite: 2]
record Student(string Name, string Group, List<double> Grades);

class Program
{
    static void Main()
    {
        // Task 1.2: En az 6 öğrenci, 2 grup[cite: 2]
        var students = new List<Student>
        {
            new("Alice", "Group A", new List<double> { 4.5, 5.0, 3.5 }),
            new("Bob", "Group A", new List<double> { 2.0, 3.0, 2.5 }),
            new("Charlie", "Group B", new List<double> { 5.0, 5.0, 4.5 }),
            new("Diana", "Group B", new List<double> { 2.0, 4.0, 3.0 }),
            new("Eve", "Group A", new List<double> { 4.0, 4.5, 4.0 }),
            new("Frank", "Group B", new List<double> { 3.5, 3.0, 4.0 })
        };

        // Task 1.3a: Her öğrencinin ortalaması (2 basamaklı)[cite: 2]
        Console.WriteLine("--- Student Averages ---");
        foreach (var student in students)
        {
            double avg = student.Grades.Average();
            Console.WriteLine($"{student.Name}: {avg:F2}");
        }

        // Task 1.3b: En yüksek ortalamaya sahip öğrenci[cite: 2]
        var topStudent = students.MaxBy(s => s.Grades.Average());
        Console.WriteLine($"\nTop Student: {topStudent?.Name} ({topStudent?.Grades.Average():F2})");

        // Task 1.3c: Grup başına ortalama (GroupBy)[cite: 2]
        Console.WriteLine("\n--- Group Averages ---");
        var groupAverages = students
            .GroupBy(s => s.Group)
            .Select(g => new { Group = g.Key, Avg = g.SelectMany(s => s.Grades).Average() });

        foreach (var g in groupAverages)
        {
            Console.WriteLine($"{g.Group}: {g.Avg:F2}");
        }

        // Task 1.3d: Herhangi bir notu 2.0 olan öğrenciler (Alfabetik)[cite: 2]
        Console.WriteLine("\n--- Students with Grade 2.0 ---");
        var studentsWith2 = students
            .Where(s => s.Grades.Contains(2.0))
            .Select(s => s.Name)
            .Order();

        foreach (var name in studentsWith2)
        {
            Console.WriteLine(name);
        }

        // Task 1.4: Tüm notların medyanı[cite: 2]
        var allGrades = students.SelectMany(s => s.Grades).ToList();
        double? median = Median(allGrades);
        Console.WriteLine($"\nOverall Median Grade: {median:F2}");
    }

    // Task 1.4: Median metodu[cite: 2]
    static double? Median(List<double> values)
    {
        if (values == null || values.Count == 0) return null;

        var sorted = values.Order().ToList();
        int count = sorted.Count;
        if (count % 2 == 0)
        {
            return (sorted[count / 2 - 1] + sorted[count / 2]) / 2.0;
        }
        return sorted[count / 2];
    }
}