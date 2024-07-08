using CourseNest.Data;
using CourseNest.Models;
using CourseNest.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseNest.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public StudentsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel addStudentViewModel)
        {
            var student = new Student
            {
                Name = addStudentViewModel.Name,
                Email = addStudentViewModel.Email,
                Phone = addStudentViewModel.Phone,
                
            };



            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await dbContext.Students.ToListAsync();
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var stud = await dbContext.Students.FindAsync(id);
            return View(stud);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Student st)
        {
            var stud = await dbContext.Students.FindAsync(st.Id);
            if (stud is not null)
            {
                stud.Name = st.Name;
                stud.Email = st.Email;
                stud.Phone = st.Phone;
                

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Students");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Student st)
        {
            var stud = await dbContext.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == st.Id);
            if (stud is not null)
            {
                dbContext.Students.Remove(stud);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Students");
        }
    }
}
