using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Encodings.Web;

public static class FilterEmployeesTask4
{
    public static string FilterEmployees(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
    {
        var filteredEmployees = employees
            .Where(emp => 
                emp.Age >= 25 && emp.Age <= 40 &&
                (emp.Department == "IT" || emp.Department == "Finance") &&
                emp.Salary >= 5000 && emp.Salary <= 9000 &&
                emp.HireDate.Year > 2017)
            .ToList();

        var sortedNames = filteredEmployees
            .OrderByDescending(emp => emp.Name.Length)
            .ThenBy(emp => emp.Name)
            .Select(emp => emp.Name)
            .ToList();

        var result = new
        {
            Names = sortedNames,
            TotalSalary = filteredEmployees.Sum(emp => emp.Salary),
            AverageSalary = filteredEmployees.Count > 0 ? Math.Round(filteredEmployees.Average(emp => emp.Salary), 2) : 0,
            MinSalary = filteredEmployees.Count > 0 ? filteredEmployees.Min(emp => emp.Salary) : 0,
            MaxSalary = filteredEmployees.Count > 0 ? filteredEmployees.Max(emp => emp.Salary) : 0,
            Count = filteredEmployees.Count
        };

        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true 
        };

        return JsonSerializer.Serialize(result, options);
    }
}
