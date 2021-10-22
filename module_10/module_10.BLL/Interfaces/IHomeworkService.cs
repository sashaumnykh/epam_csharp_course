using module_10.BLL.DTO;
using System.Collections.Generic;

namespace module_10.BLL.Interfaces
{
    public interface IHomeworkService
    {
        void Create(HomeworkDTO hw);
        IEnumerable<HomeworkDTO> GetAll();
        HomeworkDTO Get(int id);
        void Update(HomeworkDTO hw);
        void Delete(int id);
    }
}
