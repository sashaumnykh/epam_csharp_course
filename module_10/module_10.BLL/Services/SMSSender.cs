using System.Collections.Generic;
using module_10.BLL.DTO;
using module_10.BLL.Interfaces;
using AutoMapper;
using module_10.DAL.Entities;
using module_10.DAL.Interfaces;
using System.Linq;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Logging;
using Common;

namespace module_10.BLL.Services
{
    public class SMSSender : ISMSSender
    {
        private readonly ILogger<SMSSender> _logger;

        public SMSSender(ILogger<SMSSender> logger)
        {
            _logger = logger;
        }

        public void SendSMS(string toEmail, string text)
        {
            _logger.LogInformation(LogHelper.smsSending);
        }
    }
}