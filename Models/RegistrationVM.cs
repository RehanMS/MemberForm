using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace MemberForm.Models
{
    public class RegistrationVM
    {
        public int? MemberId { get; set; }
        public string Name { get; set; }
        [DisplayName("Contact No")]
        public string ContactNo { get; set; }
        [DisplayName("Joining Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime JoiningDate { get; set; }
        [DisplayName("Investment Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime InvestmentDate { get; set; }
        public string Tenure { get; set; }
        public HttpPostedFileBase Photo { get; set; }
        public byte[] PhotoLength { get; set; }
        public HttpPostedFileBase IDProof { get; set; }
        public byte[] IDProofLength { get; set; }
    }
}