using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class UserCodeCounter
    {
        [Key]
        public string RoleKey { get; set; }   

        public int LastNumber { get; set; }   

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
