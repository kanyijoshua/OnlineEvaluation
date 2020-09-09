using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineEvaluation.Models
{
    public class Response
    {
        public int Id { get; set; }
        public string RespondantId { get; set; }
        public int QuestionId { get; set; }
        public Respond Responses { get; set; }
        public int EvaluationSubjectId { get; set; }
        [ForeignKey("RespondantId")]
        public virtual ApplicationUser Respondant { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Questionnaire question { get; set; }
        [ForeignKey("EvaluationSubjectId")]
        public virtual EvaluationSubject EvaluationSubject { get; set; }
    }
    public enum Respond
    {
        VeryPoor =1,
        Poor=2,
        Fair=3,
        Good=4,
        VeryGood=5,
    }
}
