using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersLogic
{
    public enum eMoveFeedback
    {
        Success = 1,
        CanDoubleCapture = 2,
        FailedCouldCapture = 3,
        Failed = 4,
    }
}
