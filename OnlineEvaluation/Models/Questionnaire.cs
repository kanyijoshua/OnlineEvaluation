using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineEvaluation.Models
{
    public class Questionnaire
    {
        public Questionnaire()
        {
            Responses = new HashSet<Response>();
        }
        public int Id { get; set; }
        public string Question { get; set; }
        public int EvaluationTypeId { get; set; }
        [ForeignKey("EvaluationTypeId")]
        public virtual EvaluationType EvaluationType { get; set; }
        //cd[NotMapped]
        public Dictionary<int, string> RespondOtions { get; set; } = new Dictionary<int, string>()
        {
            {1,"Very Poor" },
            {2,"Poor" },
            {3,"Fair" },
            {4,"Good" },
            {5,"Very Good" },
        };
        public virtual ICollection<Response> Responses { get; set; }
    }
}