using module_10.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace module_10.BLL.Interfaces
{
    public interface IAttendanceService
    {
        void Create(AttendanceDTO attendance);
        IEnumerable<AttendanceDTO> GetAll();
        AttendanceDTO Get(int id);
        void Update(AttendanceDTO attendance);
        void Delete(int id);


    }
}
