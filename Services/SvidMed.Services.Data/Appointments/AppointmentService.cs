﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SvidMed.Data.Common.Repositories;
using SvidMed.Data.Models;
using SvidMed.Services.Mapping;

namespace SvidMed.Services.Data.Appointments
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IDeletableEntityRepository<Appointment> _appointmentsRepository;
        private readonly IDeletableEntityRepository<Doctor> _doctorRepository;

        public AppointmentService(IDeletableEntityRepository<Appointment> appointmentsRepository, IDeletableEntityRepository<Doctor> doctorRepository)
        {
            _appointmentsRepository = appointmentsRepository;
            _doctorRepository = doctorRepository;
        }

        public int? GetDoctorIdByAppointmentId(int appointmentId) => _doctorRepository.AllAsNoTracking().FirstOrDefault(d => d.Appointments.Any(a => a.Id == appointmentId))?.Id;

        public async Task<IEnumerable<T>> GetPastByPatientAsync<T>(int patientId)
        {
            var appointments =
                await _appointmentsRepository
                    .AllAsNoTracking()
                    .Where(a => a.PatientId == patientId
                                && a.DateTime.Date < DateTime.UtcNow.Date)
                    .OrderBy(a => a.DateTime)
                    .To<T>()
                    .ToListAsync();

            return appointments;
        }

        public async Task<IEnumerable<T>> GetUpcomingByPatientAsync<T>(int patientId)
        {
            var appointments =
                await _appointmentsRepository
                    .AllAsNoTracking()
                    .Where(a => a.PatientId == patientId
                                && a.DateTime.Date > DateTime.UtcNow.Date)
                    .OrderBy(a => a.DateTime)
                    .To<T>()
                    .ToListAsync();

            return appointments;
        }

        public async Task<bool> AddAsync(int doctorId, int patientId, DateTime date)
        {
            var doctorAppointment = _doctorRepository.AllAsNoTracking()
                .FirstOrDefault(d => d.Id == doctorId && d.Appointments.Any(a => a.DateTime == date));

            if (doctorAppointment != null)
            {
                return false;
            }

            await _appointmentsRepository.AddAsync(new Appointment
            {
                DateTime = date,
                DoctorId = doctorId,
                PatientId = patientId
            });

            await _appointmentsRepository.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(int appointmentId)
        {
            var appointment =
                await _appointmentsRepository
                    .AllAsNoTracking()
                    .Where(x => x.Id == appointmentId)
                    .FirstOrDefaultAsync();
            _appointmentsRepository.Delete(appointment);
            await _appointmentsRepository.SaveChangesAsync();
        }

        public async Task ConfirmAsync(int appointmentId)
        {
            var appointment =
                await _appointmentsRepository
                    .All()
                    .Where(x => x.Id == appointmentId)
                    .FirstOrDefaultAsync();
            appointment.Confirmed = true;
            await _appointmentsRepository.SaveChangesAsync();
        }

        public async Task DeclineAsync(int appointmentId)
        {
            var appointment =
                await _appointmentsRepository
                    .All()
                    .Where(x => x.Id == appointmentId)
                    .FirstOrDefaultAsync();
            appointment.Confirmed = false;
            await _appointmentsRepository.SaveChangesAsync();
        }

        public async Task<Appointment> GetByUserIdAsync(string userId, int appointmentId) => await _appointmentsRepository.All().Where(a => a.Patient.User.Id == userId && a.Id == appointmentId).FirstOrDefaultAsync();

        public async Task<T> GetByIdAsync<T>(int id) => await _appointmentsRepository.AllAsNoTracking().Where(x => x.Id == id).To<T>().FirstOrDefaultAsync();
    }
}
