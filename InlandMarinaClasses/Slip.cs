﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlandMarinaClasses
{
    [Table("Slip")]
    public class Slip
    {
        public int ID { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
        public int DockID { get; set; }

        public bool IsAvailable { get; set; }

        // navigation properties
        public virtual Dock Dock { get; set; }
        public virtual ICollection<Lease> Leases { get; set; }

        public static List<Slip> GetSlips(InlandMarinaContext db)
        {
            List<Slip> slips = db.Slips.ToList();

            return slips;
        }
    }
}
