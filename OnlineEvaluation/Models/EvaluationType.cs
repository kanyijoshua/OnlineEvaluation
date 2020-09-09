using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineEvaluation.Models
{
    public class EvaluationType
    {
        public EvaluationType()
        {
            Questionnaire = new HashSet<Questionnaire>();
            EvaluationSubject = new HashSet<EvaluationSubject>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool StatusOpen { get; set; }
        public DateTime OpeningDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public virtual ICollection<Questionnaire> Questionnaire { get; set; }
        public virtual ICollection<EvaluationSubject> EvaluationSubject { get; set; }
    }
}
