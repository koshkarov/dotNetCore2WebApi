using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetCore2WebApi.Models.Authentication
{
    public class AuthResponseModel
    {
        public int UserId { get; set; }
        public string AuthToken { get; set; }
        public string DefaultUrl { get; set; }
    }
}