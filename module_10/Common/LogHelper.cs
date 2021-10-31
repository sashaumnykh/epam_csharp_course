using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class LogHelper
    {
        public const string methodInvoked = "Method was invoked. ";
        public const string returnedOK = "Returned to Controller. OK result.";

        public const string lectureIDNF = "Lecture from LectureID property was not found.";
        public const string lecturerIDNF = "Lecturer from LecturerID property was not found.";
        public const string studentIDNF = "Student from StudentID property was not found.";

        public const string updateNull = "An object to be updated cannot be null.";
        public const string createNull = "An object to be created cannot be null.";
        public const string toGetID = "To get an object its ID must be positive.";
        public const string toDeleteID = "To delete an object its ID must be positive.";
        public const string toCheckID = "To check smth for the Student its ID must be positive.";

        public const string markField = "The field Mark must be between 0 and 5.";

        public const string attAlreadyExists = "An Attendance object with these lectureID and studentID properties already exists. To change properties' values use Update method.";

        public const string reportInvalidFormat = "The report format must be JSON or XML";
        public const string gettingReport = "Start getting the report...";
        public const string reportForLecture = "Start getting the report for the Lecture.";
        public const string reportForStudent = "Start getting the report for the Student";
        public const string reportExtention = "Getting the extention for the report";

        public const string validInput = "Input was valid.";
        public const string invalidInput = "Input was invalid.";
        public const string nullInput = "Input was null.";

        public const string checkAtt = "Start checking attendance for the Student.";
        public const string checkAvScore = "Start checking the average score for the Student.";

        public const string badAvScore = "Student's average score is less than 4.";
        public const string badAttendance = "Student has skipped more than 3 lectures";

        public const string smsSending = "Sending sms..";
        public const string emailSending = "Sending email..";

        public static string GetInfoFor(string person)
        {
            return $"Start getting inforamtion for {person}";
        }

        public static string GetReposrtFor(string person)
        {
            return $"Start getting the reposrt for {person}";
        }

        public static string ExWasThrown(string ex)
        {
            return $"{ex} was thrown";
        }

        public static string Creating(string entityName)
        {
            return $"Start creating the {entityName} object..";
        }

        public static string Getting(string entityName)
        {
            return $"Start getting the {entityName} object..";
        }

        public static string GettingAll(string entityName)
        {
            return $"Start getting all {entityName} objects..";
        }

        public static string Updating(string entityName)
        {
            return $"Start updating the {entityName} object..";
        }

        public static string Deleting(string entityName)
        {
            return $"Start deleting the {entityName} object..";
        }

        public static string ServiceInvoked(string methodName)
        {
            return $"Service{methodName}Method was invoked.";
        }

        public static string ControllerInvoked(string methodName)
        {
            return $"Controller{methodName}Method was invoked.";
        }

        public static string AvScoreMessage(double? avScore)
        {
            return $"Your average score {avScore} is less than 4";
        }

        public static string AttMessageForLecturer(string studentName, int skipped)
        {
            return $"Student {studentName} has skipped {skipped} lectures";
        }

        public static string AttMessageForStudent(int skipped)
        {
            return $"You have skipped {skipped} lectures";
        }
    }
}
