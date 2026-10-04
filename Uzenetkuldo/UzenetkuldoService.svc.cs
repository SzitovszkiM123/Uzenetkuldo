using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Uzenetkuldo.Models;
using Uzenetkuldo.Services;

namespace Uzenetkuldo
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class UzenetkuldoService : IUzenetkuldoService
    {
        public string CreateUzenet(Uzenet bejovoUzenet)
        {
            return new UzenetService().Create(bejovoUzenet);
        }

        public List<Uzenet> GetUzenetek()
        {
            List<Tablazat> rekordok = new UzenetService().Read();
            List<Uzenet> uzenetLista = new List<Uzenet>();
            foreach (var tetel in rekordok)
            {
                uzenetLista.Add(tetel as Uzenet);
            }
            return uzenetLista;
        }

        public string UpdateUzenet(Uzenet bejovoUzenet)
        {
            return new UzenetService().Update(bejovoUzenet);
        }

        public string DeleteUzenet(int id)
        {
            return new UzenetService().Delete(id);
        }
    }
}
