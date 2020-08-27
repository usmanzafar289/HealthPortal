using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarePortal.API.Helpers
{
    public class GreatEnum
    {
        
    }
    public enum eMessageType
    {
        Success = 0,
        Info = 1,
        Warning = 2,
        Confirm = 3,
        Error = 4
    }
    public enum eAttendanceStatus
    {
        Present = 22,
        Absent = 23,
        Leave = 25
    }

    public enum eEvaluationType
    {
        Assignment = 5,
        Quiz = 6,
        MidTerm = 26
    }

}
