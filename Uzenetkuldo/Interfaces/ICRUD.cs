using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uzenetkuldo.Models;

namespace Uzenetkuldo.Interfaces
{
    public interface ICRUD
    {
        string Create(Tablazat rekord);
        List<Tablazat> Read();
        string Update(Tablazat rekord);
        string Delete(int id);
    }
}
