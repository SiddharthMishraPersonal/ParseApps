using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse.Service
{
    public interface IChatParseObject
    {
        string ObjectId { get; set; }

        Task<string> SaveObject();
    }
}
