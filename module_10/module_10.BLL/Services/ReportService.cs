using System.Collections.Generic;
using module_10.BLL.DTO;
using module_10.BLL.Interfaces;
using AutoMapper;
using module_10.DAL.Entities;
using module_10.DAL.Interfaces;
using System.Linq;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace module_10.BLL.Services
{
    public class ReportService : IReportService
    {
        private IUnitOfWork _database { get; set; }
        private IMapper _mapper;

        public ReportService(IUnitOfWork uow, IMapper mapper)
        {
            _database = uow;
            _mapper = mapper;
        }

        public void GetReport(ReportDTO report)
        {
            IEnumerable<Attendance> attendance = new List<Attendance>();
            string reportFileName = @"report.json";
            
            if (report.lectureName != null)
            {
                reportFileName = String.Format(@"reportForLecture{0}", report.lectureName); 

                var lecturesIDs = from l in _database.Lectures.GetAll()
                                  where l.Name == report.lectureName
                                  select l.ID;
                attendance = from att in _database.Attendances.GetAll()
                                 where lecturesIDs.Contains(att.LectureID)
                                 select att;
            }
            else if (report.studentName != null)
            {
                reportFileName = String.Format(@"reportForStudent{0}", report.studentName);

                var studentsIDs = from s in _database.Students.GetAll()
                                  where s.Name == report.studentName
                                  select s.ID;
                attendance = from att in _database.Attendances.GetAll()
                                  where studentsIDs.Contains(att.StudentID)
                                  select att;
            }

            if(report.Format.ToLower() == "json")
            {
                reportFileName += ".json";
                GetJSON(attendance, reportFileName);
            }
            if (report.Format.ToLower() == "xml")
            {
                reportFileName += ".xml";
                GetXML(attendance, reportFileName);
            }
        }

        public void GetJSON(IEnumerable<Attendance> attendance, string fileName)
        {
            List<AttendanceDTO> _attendanceData = new List<AttendanceDTO>();

            foreach (var att in attendance)
            {
                var attDTO = _mapper.Map<Attendance, AttendanceDTO>(att);
                _attendanceData.Add(attDTO);
            }

            string json = JsonSerializer.Serialize(_attendanceData);
            File.WriteAllText(fileName, json);
        }

        public void GetXML(IEnumerable<Attendance> attendance, string fileName)
        {
            XmlDocument doc = new XmlDocument();

            var xml = new XElement("attendances", from att in attendance.ToList()
                                                  select new XElement("attendance",
                                                     new XAttribute("LectureID", att.LectureID.ToString()),
                                                     new XAttribute("StudentID", att.StudentID.ToString()),
                                                     new XAttribute("WasPresent", att.isPresent.ToString()),
                                                     new XAttribute("WasHomeworkDone", att.isHwDone.ToString())));
            System.IO.File.WriteAllText(fileName, xml.ToString());
        }

    }
}
