﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlandMarinaClasses
{
    [Table("Dock")]
    public class Dock
    {
        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public bool WaterService { get; set; }
        public bool ElectricalService { get; set; }

        // navigation property
        public virtual ICollection<Slip> Slips { get; set; }

        public static List<Dock> GetSlips(InlandMarinaContext db)
        {
            List<Dock> docks = db.Docks.ToList();

            return docks;
        }
    }
}
