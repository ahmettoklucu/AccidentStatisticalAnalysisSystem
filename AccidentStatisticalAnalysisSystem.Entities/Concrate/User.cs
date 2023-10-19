using AccidentStatisticalAnalysisSystem.Entities;
using AccidentStatisticalAnalysisSystem.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.Entities.Concrate
{
    public class User : IEntity
    {
        public Guid Id { get; set; } = new Guid();
        public string Name { get; set; }
        public string SureName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string EMail { get; set; }
        public int RoleId { get; set; } = 2;
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
        public bool IsDelete { get; set; }=false;
        public DateTime StarDate { get; set; } = DateTime.Now;
        public Guid SecretKey { get; set; }= new Guid();
        public List<Company> Companies { get; set; }
    }
}
