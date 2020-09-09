using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineEvaluation.Models
{
    public class EvaluationSubject
    {
        public EvaluationSubject()
        {
            Responses = new HashSet<Response>();
        }
        public int Id { get; set; }
        public int? StateCorporationId { get; set; }
        public string personBeingEvaluatedId { get; set; }
        public DateTime? DateOfAppointment { get; set; }
        public string EnablingLegalInstument { get; set; }
        public string ChairpersonId { get; set; }
        public string CEOId { get; set; }
        public string DirectorId { get; set; }
        public string EndofTerm { get; set; }
        public int EvaluationTypeId { get; set; }
        [ForeignKey("CEOId")]
        public virtual ApplicationUser CEO { get; set; }
        [ForeignKey("ChairpersonId")]
        public virtual ApplicationUser Chairperson { get; set; }
        [ForeignKey("DirectorId")]
        public virtual ApplicationUser Director { get; set; }
        [ForeignKey("personBeingEvaluatedId")]
        public virtual ApplicationUser personBeingEvaluated { get; set; }
        [ForeignKey("StateCorporationId")]
        public virtual StateCorporation StateCorporations { get; set; }
        [ForeignKey("EvaluationTypeId")]
        public virtual EvaluationType EvaluationType { get; set; }
        public virtual ICollection<Response> Responses { get; set; }

    }
}
