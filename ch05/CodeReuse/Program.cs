using dotNet8Library;
using dotNet8Library.Evaluations.Content;
using dotNet8Library.Evaluations.Types;
using System;

var buEvaluation = new BasicUsersEvaluation
{
    Id = 1,
    User = "Gabriel",
    Description = "Good city to visit",
    Grade = 3
};

var puEvaluation = new PrimeUsersEvaluation
{
    Id = 2,
    User = "Francesco",
    Description = "Good city to visit",
    Grade = 4
};

var service = new EvaluationService<CityEvaluation>();
service.Content.Evaluations.Add(buEvaluation);
service.Content.Evaluations.Add(puEvaluation);
Console.WriteLine($"The final evaluation is {service.CalculateEvaluationAverage()}");
Console.ReadKey();
