﻿
using AccidentStatisticalAnalysisSystem.Entities;
using AccidentStatisticalAnalysisSystem.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace AccidentStatisticalAnalysisSystem.Entities.Concrate
{
    public class Company:IEntity
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }
        public int NaceId { get; set; }
        public int CompanyTypeId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; } 
        public int NumberOfWorkers { get; set; }
        public List<Incident> Incidents { get; set; }
        public bool IsDelete { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public string GUID { get; set; }
        public string Image { get; set; }
        public int Status { get; set; }
    }
}