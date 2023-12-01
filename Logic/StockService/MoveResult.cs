using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAPI.Logic.StockService
{
    public class MoveResult
    {
        public int NewQuantity { get; set; }
        public int OldQuantity { get; set; }
    }
}
