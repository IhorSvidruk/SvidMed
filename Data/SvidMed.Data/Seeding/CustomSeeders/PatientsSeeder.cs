﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SvidMed.Common;
using SvidMed.Data.Models;

namespace SvidMed.Data.Seeding.CustomSeeders
{
    public class PatientsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Patients.Any())
            {
                return;
            }

            var patientUser = await dbContext.ApplicationUsers
                .Where(u => u.Email == GlobalConstants.AccountsSeeding.PatientEmail).FirstOrDefaultAsync();

            var patient = new Patient()
            {
                FirstName = "Kostadin",
                LastName = "Minov",
                Age = 24,
                Gender = "Male",
                PhoneNumber = "0885254553",
                TownId = 2,
                User = patientUser,
            };

            patientUser.Patient = patient;

            await dbContext.Patients.AddAsync(patient);
            await dbContext.SaveChangesAsync();
        }
    }
}
