using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace MemberForm.Models
{
    public class RegistrationViewModel
    {
        public int? MemberId { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string JoiningDate { get; set; }
        public string InvestmentDate { get; set; }
        public string Tenure { get; set; }
        public HttpPostedFileBase Photo { get; set; }
        public byte[] PhotoLength { get; set; }
        public HttpPostedFileBase IDProof { get; set; }
        public byte[] IDProofLength { get; set; }
    }
}