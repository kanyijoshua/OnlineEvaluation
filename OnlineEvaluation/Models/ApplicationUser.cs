using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineEvaluation.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            EvaluationSubjects = new HashSet<EvaluationSubject>();
            Responses = new HashSet<Response>();
        }
        //[NotMapped]
        public virtual ICollection<EvaluationSubject> EvaluationSubjects { get; set; }
        public virtual ICollection<Response> Responses { get; set; }
    }
}
