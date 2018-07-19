using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TastTest.Models
{
    public class Player
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public int? TeamId { get; set; }
        public Team Team { get; set; }
    }
}