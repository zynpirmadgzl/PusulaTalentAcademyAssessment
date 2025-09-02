using System;
using System.Linq;
using System.Xml.Linq;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Collections.Generic;

public static class FilterPeopleFromXmlTask3
{
    public static string FilterPeopleFromXml(string xmlData)
    {
        try
        {
            XDocument doc = XDocument.Parse(xmlData);

            var filteredPeople = doc.Descendants("Person")
                .Where(person =>
                {
                    int age = int.Parse(person.Element("Age")?.Value ?? "0");
                    string department = person.Element("Department")?.Value ?? "";
                    decimal salary = decimal.Parse(person.Element("Salary")?.Value ?? "0");
                    DateTime hireDate = DateTime.Parse(person.Element("HireDate")?.Value ?? DateTime.MinValue.ToString());

                    return age > 30 &&
                           department == "IT" &&
                           salary > 5000 &&
                           hireDate.Year < 2019;
                })
                .Select(person => new
                {
                    Name = person.Element("Name")?.Value ?? "",
                    Salary = decimal.Parse(person.Element("Salary")?.Value ?? "0")
                })
                .ToList();

            var result = new
            {
                Names = filteredPeople.OrderBy(p => p.Name).Select(p => p.Name).ToList(),
                TotalSalary = filteredPeople.Sum(p => p.Salary),
                AverageSalary = filteredPeople.Count > 0 ? filteredPeople.Average(p => p.Salary) : 0,
                MinSalary = filteredPeople.Count > 0 ? filteredPeople.Min(p => p.Salary) : 0,
                MaxSalary = filteredPeople.Count > 0 ? filteredPeople.Max(p => p.Salary) : 0,
                Count = filteredPeople.Count
            };

            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = false
            };

            return JsonSerializer.Serialize(result, options);
        }
        catch
        {
            var emptyResult = new
            {
                Names = new List<string>(),
                TotalSalary = 0m,
                AverageSalary = 0m,
                MinSalary = 0m,
                MaxSalary = 0m,
                Count = 0
            };

            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            return JsonSerializer.Serialize(emptyResult, options);
        }
    }
}
