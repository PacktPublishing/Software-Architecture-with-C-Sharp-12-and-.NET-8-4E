using System.Linq;

namespace dotNet8Library
{
    /// <summary>
    /// This declaration indicates that any class that implements 
    /// the interface IContentEvaluaded can be used for this
    /// service. Besides, the service will be responsible to 
    /// create the evaluated content
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EvaluationService<T> where T : IContentEvaluated, new()
    {
        /// <summary>
        /// The content is generic and totatly managed by the class
        /// </summary>
        public T Content { get; set; }

        /// <summary>
        /// A better approach, instead of using reflection
        /// </summary>
        public EvaluationService()
        {
            Content = new T();
        }


        /// <summary>
        /// No matter the Evaluation, the calculation will always get values from the method CalculateGrade
        /// </summary>
        /// <returns>The average of the grade from Evaluations</returns>
        public double CalculateEvaluationAverage() =>
            Content.Evaluations
                .Select(x => x.CalculateGrade())
                .Average();
    }
}
