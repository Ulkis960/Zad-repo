﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App_tables.Entities
{
    public class Clients
    {
        public Clients()
        {
            this.Purchases = new HashSet<Purchase>();
        }
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]

        public string FirstName { get; set; }
        [Required]
        [MaxLength(30)]

        public string LastName { get; set; }
        [Required]
        [MaxLength(30)]
        [MinLength(10)]

        public string EGN { get; set; }
        [Required]
        [MaxLength(30)]

        public string Address { get; set; }
        [Required]
        [EmailAddress]

        public string Email { get; set; }
        [Required]
        [MaxLength(10)]

        public string Phone { get; set; }

        public ICollection<Purchase> Purchases { get; set; }


    }
}
