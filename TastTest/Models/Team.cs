using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace TastTest.Models
{
    public class Team
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Тема")]
        public string Thema { get; set; }
        [Display(Name = "Дата")]
        public string Dates { get; set; }
        [Display(Name = "Время")]
        public string Times { get; set; }
        [Display(Name = "Количество участников")]
        public int Amount { get; set; }
        public int SumAmount { get; set; }

        public ICollection<Player> Players { get; set; }
        public Team()
        {
            Players = new List<Player>();
        }
        
    }
}