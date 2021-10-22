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

namespace exp
{
    public class Programm
    {
        public static void Main(string[] args)
        {
            
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Attendance, AttendanceDTO>();
                cfg.CreateMap<AttendanceDTO, Attendance>();
            });

            Mapper _mapper = new Mapper(config);

            string reportPath = String.Format(@"reportForStudent{0}.json", "Alex");

            List<AttendanceDTO> _attendanceData = new List<AttendanceDTO>();

            List<Attendance> attendance = new List<Attendance>
            {
                new Attendance
                {
                    StudentID = 1,
                    isPresent = true
                },
                new Attendance
                {
                    StudentID = 2,
                    isPresent = false
                },
                new Attendance
                {
                    StudentID = 3,
                    isPresent = true
                }
            };

            XmlDocument doc = new XmlDocument();

            var xml = new XElement("attendances", from att in attendance.ToList()
                                                select new XElement("attendance",
                                                   new XAttribute("LectureID", att.LectureID.ToString()), 
                                                   new XAttribute("StudentID", att.StudentID.ToString()),
                                                   new XAttribute("WasPresent", att.isPresent.ToString()),
                                                   new XAttribute("WasHomeworkDone", att.isHwDone.ToString())));
            System.IO.File.WriteAllText(@"report.xml", xml.ToString());

            /*
            using (XmlWriter writer = XmlWriter.Create(@"report.xml"))
            {
                writer.WriteStartElement("attendances");
                foreach (var att in attendance)
                {
                    writer.WriteStartElement("attendance");
                    writer.WriteElementString("LectureID", att.LectureID.ToString());
                    writer.WriteElementString("StudentID", att.StudentID.ToString());
                    writer.WriteElementString("WasPresent", att.isPresent.ToString());
                    writer.WriteElementString("WasHomeworkDone", att.isHwDone.ToString());
                }
                writer.WriteEndElement();
                writer.Flush();

            }
            */
        }
    }

}

