using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using module_10.BLL.Interfaces;
using module_10.BLL.Services;
using module_10.DAL.EF;
using module_10.DAL.Entities;
using module_10.DAL.Interfaces;
using module_10.DAL.Repositories;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog;

namespace module_10.WEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MyContext>(options => options.UseSqlServer(connection));

            services.AddAutoMapper(typeof(MapperProfile));

            services.AddControllers();

            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

            services.AddScoped<IUnitOfWork, EFUnitOfWork>();

            services.AddScoped<IEmailSender, EmailSender>();


            services.AddScoped<AttendanceRepository>();

            services.AddScoped<IRepository<Attendance>, AttendanceRepository>();
            services.AddScoped<IRepository<Homework>, HomeworkRepository>();
            services.AddScoped<IRepository<Lecture>, LectureRepository>();
            services.AddScoped<IRepository<Lecturer>, LecturerRepository>();
            services.AddScoped<IRepository<Student>, StudentRepository>();


            services.AddScoped<IAttendanceService, AttendanceService>();
            services.AddScoped<IHomeworkService, HomeworkService>();
            services.AddScoped<ILectureService, LectureService>();
            services.AddScoped<ILecturerService, LecturerService>();
            services.AddScoped<IStudentService, StudentService>();

            services.AddScoped<IReportService, ReportService>();

            services.AddSwaggerGen(options =>
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "WebAPI", Version = "v1" })
                );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
