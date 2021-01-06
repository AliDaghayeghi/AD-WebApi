using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADWebApi.Models.Dto
{
    public class ToDoItemDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime insertime { get; set; }
        public List<Links> Links { get; set; }
    }


}
