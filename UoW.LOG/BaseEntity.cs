using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using UoW.CORE.Interface;

namespace UoW.LOG
{
    public class BaseEntity : IEntity
    {
        public BaseEntity()
        {
            ID = new int();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
    }
}
