using FluentValidation;
using FluentValidation.Results;
using System.Runtime.Serialization;

namespace ElmaApplicationBffService.Core.Infrastructure;

[Serializable]
public class ValidationException : Exception
{
    public IDictionary<string, string[]> Failures { get; }

    public ValidationException()
        : base("One or more validation failures have occurred.")
    {
        Failures = new Dictionary<string, string[]>();
    }

    public ValidationException(List<ValidationFailure> failures)
        : this()
    {
        IEnumerable<string> enumerable = failures.Select((e) => e.PropertyName).Distinct();
        foreach (string propertyName in enumerable)
        {
            string[] value = (from e in failures
                              where e.PropertyName == propertyName
                              select e.ErrorMessage).ToArray();
            Failures.Add(propertyName, value);
        }
    }

    protected ValidationException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        Failures = (Dictionary<string, string[]>)info.GetValue("Failures", typeof(Dictionary<string, string[]>));
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("Failures", new Dictionary<string, string[]>(Failures), typeof(Dictionary<string, string[]>));
        base.GetObjectData(info, context);
    }
}