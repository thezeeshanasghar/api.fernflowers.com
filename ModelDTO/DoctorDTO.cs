using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using api.fernflowers.com.Data;
using api.fernflowers.com.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.fernflowers.com.ModelDTO;

namespace api.fernflowers.com.ModelDTO
{

    public class DoctorDTO
        {
        public long Id { get; set; }
        public int MobileNumber { get; set; }
        public string Password { get; set; }
        public string DoctorType { get; set; }
        
    }

}