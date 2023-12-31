﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlandMarinaClasses
{
    [Table("Lease")]
    public class Lease
    {
        public int ID { get; set; }
        public int SlipID { get; set; }
        public int CustomerID { get; set; }

        //navigation properties
        public virtual Customer Customer { get; set; }
        public virtual Slip Slip { get; set; }

        public static List<Lease> GetSlips(InlandMarinaContext db)
        {
            List<Lease> leases = db.Leases.ToList();

            return leases;
        }
    }
}
