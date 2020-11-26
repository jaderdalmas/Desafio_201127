using API.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repository
{
  public class DeParaRepository : IDeParaRepository
  {
    readonly IList<DePara> db = new List<DePara>() {
      new DePara("AFN", 66),
      new DePara("ALL", 49),
      new DePara("ANG", 33),
      new DePara("ARS", 3),
      new DePara("AWG", 6),
      new DePara("BOB", 56),
      new DePara("BYN", 64),
      new DePara("CAD", 25),
      new DePara("CDF", 58),
      new DePara("CLP", 16),
      new DePara("COP", 37),
      new DePara("CRC", 52),
      new DePara("CUP", 8 ),
      new DePara("CVE", 51),
      new DePara("CZK", 29),
      new DePara("DJF", 36),
      new DePara("DZD", 54),
      new DePara("EGP", 12),
      new DePara("EUR", 20),
      new DePara("FJD", 38),
      new DePara("GBP", 22),
      new DePara("GEL", 48),
      new DePara("GIP", 18),
      new DePara("HTG", 63),
      new DePara("ILS", 40),
      new DePara("IRR", 17),
      new DePara("ISK", 11),
      new DePara("JPY", 9 ),
      new DePara("KES", 21),
      new DePara("KMF", 19),
      new DePara("LBP", 42),
      new DePara("LSL", 4 ),
      new DePara("MGA", 35),
      new DePara("MGB", 26),
      new DePara("MMK", 69),
      new DePara("MRO", 53),
      new DePara("MRU", 15),
      new DePara("MUR", 7 ),
      new DePara("MXN", 41),
      new DePara("MZN", 43),
      new DePara("NIO", 23),
      new DePara("NOK", 62),
      new DePara("OMR", 34),
      new DePara("PEN", 45),
      new DePara("PGK", 2 ),
      new DePara("PHP", 24),
      new DePara("RON", 5 ),
      new DePara("SAR", 44),
      new DePara("SBD", 32),
      new DePara("SGD", 70),
      new DePara("SLL", 10),
      new DePara("SOS", 61),
      new DePara("SSP", 47),
      new DePara("SZL", 55),
      new DePara("THB", 39),
      new DePara("TRY", 13),
      new DePara("TTD", 67),
      new DePara("UGX", 59),
      new DePara("USD", 1 ),
      new DePara("UYU", 46),
      new DePara("VES", 68),
      new DePara("VUV", 57),
      new DePara("WST", 28),
      new DePara("XAF", 30),
      new DePara("XAU", 60),
      new DePara("XDR", 27),
      new DePara("XOF", 14),
      new DePara("XPF", 50),
      new DePara("ZAR", 65),
      new DePara("ZWL", 31)
    };

    public Task<IEnumerable<DePara>> GetAll()
    {
      return (Task<IEnumerable<DePara>>)db;
    }
  }
}
