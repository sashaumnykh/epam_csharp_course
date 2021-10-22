using AutoMapper;
using module_10.BLL.DTO;
using module_10.DAL.Entities;
using System.Collections.Generic;
using module_10.WEB.Models;
//using WebApiExample.ViewModels.Hateoas;

namespace module_10.WEB
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Attendance, AttendanceDTO>();
            CreateMap<AttendanceDTO, AttendanceViewModel>();
            CreateMap<AttendanceViewModel, AttendanceDTO>();
            CreateMap<AttendanceDTO, Attendance>();

            CreateMap<Homework, HomeworkDTO>();
            CreateMap<HomeworkDTO, HomeworkViewModel>();
            CreateMap<HomeworkViewModel, HomeworkDTO>();
            CreateMap<HomeworkDTO, Homework>();

            CreateMap<Lecture, LectureDTO>();
            CreateMap<LectureDTO, LectureViewModel>();
            CreateMap<LectureViewModel, LectureDTO>();
            CreateMap<LectureDTO, Lecture>();

            CreateMap<LecturerViewModel, LecturerDTO>();
            CreateMap<LecturerDTO, Lecturer>();
            CreateMap<Lecturer, LecturerDTO>();
            CreateMap<LecturerDTO, LecturerViewModel>();

            CreateMap<ReportViewModel, ReportDTO>();
            CreateMap<ReportDTO, ReportViewModel>();

            CreateMap<StudentViewModel, StudentDTO>();
            CreateMap<StudentDTO, Student>();
            CreateMap<StudentDTO, StudentViewModel>();
            CreateMap<Student, StudentDTO>();
        }
    }
}
