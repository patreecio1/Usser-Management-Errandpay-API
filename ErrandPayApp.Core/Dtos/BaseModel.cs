﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrandPayApp.Core.Dtos
{
    public class BaseModel<T>
        {
            public T Id { get; set; }
            public bool IsActive { get; set; } = true;
            public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;
            public string CreatedBy { get; set; }
            public DateTimeOffset? ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
        }
    
}
