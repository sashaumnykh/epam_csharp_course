using module_10.BLL.DTO;
using System.Collections.Generic;

namespace module_10.BLL.Interfaces
{
    public interface ILectureService
    {
        void Create(LectureDTO lecture);
        IEnumerable<LectureDTO> GetAll();
        
        LectureDTO Get(int id);
        void Update(LectureDTO lecture);
        void Delete(int id);
    }
}
