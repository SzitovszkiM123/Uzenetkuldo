using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Uzenetkuldo.Models
{
    [DataContract]
    public class Uzenet: Tablazat
    {
        [DataMember]
        public string Szoveg { get; set; }
        [DataMember]
        public DateTime KuldesiIdo { get; set; }
        [DataMember]
        public string UzenetTipus { get; set; }
        [DataMember]
        public string Telefon { get; set; }
        [DataMember]
        public string Email { get; set; }

        public override string ToString()
        {
            return $"Azonosító: {Id} | Tartalom: {Szoveg} | Elküldve: {KuldesiIdo} | Csatorna: {UzenetTipus} | Elérhetőség: {(UzenetTipus=="SMS"?Telefon:Email)}";
        }
    }
}
