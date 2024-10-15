using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalSystemAPI.Data;
using HospitalSystemAPI.Models;
using HospitalSystemAPI.DTOs;

namespace HospitalSystemAPI.Controllers
{
    public class DoctorController : Controller
    {
        private readonly HospitalDbContext _context;

        public DoctorController(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var doctors = await _context.Doctors.Include(d => d.Speciality).ToListAsync();
            List<GetDoctorDTO> DoctorsObject = getDoctorsVMobject(doctors);
            return View(DoctorsObject);  // Return the view that displays the list of doctors
        }

        
        public async Task<IActionResult> DoctorsBySpeciality(int id)
        {
            var doctors = await _context.Doctors.Include(d => d.Speciality).Where(d => d.SpecialityId == id).ToListAsync();
            List<GetDoctorDTO> DoctorsObject = getDoctorsVMobject(doctors);
            return View("Index", DoctorsObject);  
        }

        public async Task<IActionResult> Details(int id)
        {
            var doctor = await _context.Doctors.Include(d => d.Speciality).SingleOrDefaultAsync(d => d.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            GetDoctorDTO DoctorObject = new GetDoctorDTO()
            {
                Id = doctor.Id,
                Name = doctor.Name,
                PhoneNumber = doctor.PhoneNumber,
                Speciality = doctor.Speciality.Name
            };
            return View(DoctorObject);  // Return the view for doctor details
        }

        // GET: Doctor/Create
        // Displays a form for creating a new doctor
        public IActionResult Create()
        {
            return View();  // Return a view containing a form to add a doctor
        }

        // POST: Doctor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]  // Prevents CSRF attacks
        public async Task<IActionResult> Create(DoctorInsertionDTO doctorVM)
        {
            if (ModelState.IsValid)
            {
                var newDoctor = new Doctor();
                MapVmToDoctor(doctorVM, newDoctor);
                _context.Doctors.Add(newDoctor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));  // Redirect to list after successful creation
            }
            return View(doctorVM);  // If invalid, return the form with validation messages
        }

        // GET: Doctor/Edit/5
        // Shows the form to edit an existing doctor
        public async Task<IActionResult> Edit(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            var doctorVM = new DoctorInsertionDTO()
            {
                Id = doctor.Id,
                Name = doctor.Name,
                PhoneNumber = doctor.PhoneNumber,
                SpecialityId = doctor.SpecialityId,
                Email = doctor.Email,
                Password = doctor.password
            };
            return View(doctorVM);  // Return the view containing the edit form
        }

        // POST: Doctor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DoctorInsertionDTO doctorVM)
        {
            if (id != doctorVM.Id || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingDoctor = await _context.Doctors.FindAsync(id);
            if (existingDoctor == null)
            {
                return NotFound();
            }

            MapVmToDoctor(doctorVM, existingDoctor);
            _context.Entry(existingDoctor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));  // Redirect after editing
        }

        // GET: Doctor/Delete/5
        // Shows confirmation for deleting a doctor
        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            GetDoctorDTO DoctorObject = new GetDoctorDTO()
            {
                Id = doctor.Id,
                Name = doctor.Name,
                PhoneNumber = doctor.PhoneNumber,
                Speciality = doctor.Speciality.Name
            };
            return View(DoctorObject);  // Return a confirmation view for deletion
        }

        // POST: Doctor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));  // Redirect to list after deletion
        }

        // Private methods remain the same
        private bool DoctorExists(int id)
        {
            return _context.Doctors.Any(e => e.Id == id);
        }

        private List<GetDoctorDTO> getDoctorsVMobject(List<Doctor> doctors)
        {
            List<GetDoctorDTO> DoctorsObject = new List<GetDoctorDTO>();
            foreach (var doctor in doctors)
            {
                DoctorsObject.Add(new GetDoctorDTO()
                {
                    Id = doctor.Id,
                    Name = doctor.Name,
                    PhoneNumber = doctor.PhoneNumber,
                    Speciality = doctor.Speciality.Name
                });
            }
            return DoctorsObject;
        }

        private void MapVmToDoctor(DoctorInsertionDTO doctorVM, Doctor doctor)
        {
            doctor.Id = doctorVM.Id;
            doctor.Name = doctorVM.Name;
            doctor.PhoneNumber = doctorVM.PhoneNumber;
            doctor.SpecialityId = doctorVM.SpecialityId;
            doctor.Email = doctorVM.Email;
            doctor.password = doctorVM.Password;
        }
    }

}
