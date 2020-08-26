using NetCoreEFDemo.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetCoreEFDemo.Application.Entitys
{

    public class Test : IEntity
    {
        //[ScaffoldColumn(false)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)] //自增长
        [Key]
        public string ID { get; set; }
        public string Title { get; set; }
    }
}

