using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Data.VO
{
    public class SimpleTodoVO
    {
        public SimpleTodoVO()
        {
            this.InputsName = new();
            this.InputsName.Add("Title", "título");
            this.InputsName.Add("Description", "descrição");
            this.InputsName.Add("CategoryId", "categoria");

            this.Nullables = new();
            this.Nullables.Add("CategoryId");
            this.Nullables.Add("InputsName");
            this.Nullables.Add("Nullables");
        }

        public Dictionary<string, string> InputsName { get; set; }
        public List<string> Nullables { get; set; }
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long CategoryId { get; set; }
    }
}
