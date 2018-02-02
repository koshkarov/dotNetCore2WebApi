using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetCore2WebApi.Entities
{
    public class Article
    {
        public int Id { get; set; }
        [MinLength(3)]
        public string Name { get; set; }
        public string Text { get; set; }
    }
}
