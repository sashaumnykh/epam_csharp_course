using module_10.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace module_10.BLL.Interfaces
{
    public interface IStudentService
    {
        void Create(StudentDTO student);
        IEnumerable<StudentDTO> GetAll();
        StudentDTO Get(int id);
        void Update(StudentDTO student);
        void Delete(int id);
        void CheckAttendance(StudentDTO student);
        void CheckAverageScore(StudentDTO student);

    }
}
