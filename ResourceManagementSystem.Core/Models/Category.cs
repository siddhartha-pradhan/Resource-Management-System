﻿using ResourceManagementSystem.Core.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManagementSystem.Core.Models
{
    public class Category : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        public string Title { get; set; }

        public bool IsDeleted { get; set; }
    }
}
