using module_10.BLL.DTO;
using System.Collections.Generic;

namespace module_10.BLL.Interfaces
{
    public interface ILecturerService
    {
        void Create(LecturerDTO lecturer);
        IEnumerable<LecturerDTO> GetAll();
        
        LecturerDTO Get(int id);

        void Update(LecturerDTO lecturer);
        void Delete(int id);

    }
}
