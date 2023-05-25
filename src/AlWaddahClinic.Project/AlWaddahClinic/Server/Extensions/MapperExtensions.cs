using System;
using AlWaddahClinic.Shared.Dtos;

namespace AlWaddahClinic.Server.Extensions
{

    public static class DtoMappers
    {
        public static PatientSummaryDto ToPatientSummaryDto(this Patient patient)
        {
            return new PatientSummaryDto
            {
                PatientId = patient.Id,
                FullName = patient.FullName,
                EmailAddress = patient.EmailAddress,
                PhoneNumber = patient.PhoneNumber,
                Gender = patient.Gender
            };
        }

        public static PatientDto ToPatientDto(this Patient patient)
        {
            return new PatientDto
            {
                Id = patient.Id,
                FullName = patient.FullName,
                EmailAddress = patient.EmailAddress,
                DateOfBirth = patient.DateOfBirth,
                Address = patient.Address,
                PhoneNumber = patient.PhoneNumber,
                HealthRecords = patient.HealthRecords.Select(h => h.ToHealthRecordSummaryDto()).ToList(),
                Appointments = patient.Appointments.Select(a => a.ToAppointmentDto()).ToList(),
                Gender = patient.Gender
            };
        }

        public static HealthRecordSummaryDto ToHealthRecordSummaryDto(this HealthRecord healthRecord)
        {
            return new HealthRecordSummaryDto
            {
                Id = healthRecord.Id,
                CreatedOn = (DateTime)healthRecord.CreatedOn
            };
        }

        public static HealthRecordDto ToHealthRecordDto(this HealthRecord healthRecord)
        {
            return new HealthRecordDto
            {
                Id = healthRecord.Id,
                Description = healthRecord.Description,
                CreatedOn = (DateTime)healthRecord.CreatedOn,
                Notes = healthRecord.Notes.Select(n => n.ToNoteDto()).ToList(),
                Patient = healthRecord.Patient.ToPatientDto()
            };
        }

        public static MedicationDto ToMedicationDto(this Medication medication)
        {
            return new MedicationDto
            {
                Id = medication.Id,
                CommercialName = medication.CommercialName,
                DailyDose = medication.DailyDose,
                FinishAt = medication.FinishAt,
                Prescription = medication.Prescription.ToPrescriptionDto()
            };
        }

        public static PrescriptionDto ToPrescriptionDto(this Prescription prescription)
        {
            return new PrescriptionDto
            {
                Id = prescription.Id,
                Description = prescription.Description,
                Medications = prescription.Medications.Select(m => m.ToMedicationDto()).ToList(),
                Appointment = prescription.Appointment.ToAppointmentDto()
            };
        }

        public static AppointmentDto ToAppointmentDto(this Appointment appointment)
        {
            return new AppointmentDto
            {
                Id = appointment.Id,
                Description = appointment.Description,
                Patient = appointment.Patient.ToPatientDto(),
                StartAt = appointment.StartAt,
                FinishAt = appointment.FinishAt,
                Prescription = appointment.Prescription?.ToPrescriptionDto()
            };
        }

        public static NoteDto ToNoteDto(this Note note)
        {
            return new NoteDto
            {
                Id = note.Id,
                Title = note.Title
            };
        }
    }

    public static class ModelMappers
    {
        public static Patient ToPatientCreate(this PatientCreateDto patientDto)
        {
            return new Patient
            {
                FullName = patientDto.FullName,
                EmailAddress = patientDto.EmailAddress,
                PhoneNumber = patientDto.PhoneNumber,
                Address = patientDto.Address,
                DateOfBirth = patientDto.DateOfBirth,
                Gender = patientDto.Gender,
                Appointments = new List<Appointment>(),
                HealthRecords = new List<HealthRecord>()
            };
        }

        public static Patient ToPatientUpdate(this PatientUpdateDto patientUpdateDto)
        {
            return new Patient
            {
                FullName = patientUpdateDto.FullName,
                EmailAddress = patientUpdateDto.EmailAddress,
                PhoneNumber = patientUpdateDto.PhoneNumber,
                Address = patientUpdateDto.Address,
                DateOfBirth = patientUpdateDto.DateOfBirth,
                Gender = patientUpdateDto.Gender
            };
        }

        public static Patient ToPatient(this PatientDto patientDto)
        {
            return new Patient
            {
                Id = patientDto.Id,
                FullName = patientDto.FullName,
                EmailAddress = patientDto.EmailAddress,
                PhoneNumber = patientDto.PhoneNumber,
                Address = patientDto.Address,
                DateOfBirth = patientDto.DateOfBirth,
                Gender = patientDto.Gender,
                Appointments = new List<Appointment>(),
                HealthRecords = new List<HealthRecord>()
            };
        }

        public static HealthRecord ToHealthRecordCreate(this HealthRecordCreateDto healthRecordDto)
        {
            return new HealthRecord
            {
                Description = healthRecordDto.Description,
                Notes = healthRecordDto.Notes?.Select(n => n.ToNoteCreate()).ToList()
            };
        }

        public static HealthRecord ToHealthRecord(this HealthRecordDto healthRecordDto)
        {
            return new HealthRecord
            {
                Id = healthRecordDto.Id,
                PatientId = healthRecordDto.Patient.Id,
                Description = healthRecordDto.Description,
                Patient = healthRecordDto.Patient.ToPatient(),
                Notes = healthRecordDto.Notes?.Select(n => n.ToNote()).ToList()
            };

        }

        public static Medication ToMedication(this MedicationDto medicationDto)
        {
            return new Medication
            {
                Id = medicationDto.Id,
                CommercialName = medicationDto.CommercialName,
                DailyDose = medicationDto.DailyDose,
                FinishAt = medicationDto.FinishAt,
                Prescription = medicationDto.Prescription.ToPrescription(),
            };
        }

        public static Prescription ToPrescription(this PrescriptionDto prescriptionDto)
        {
            return new Prescription
            {
                Id = prescriptionDto.Id,
                Description = prescriptionDto.Description,
                Appointment = prescriptionDto.Appointment.ToAppointment(),
                Medications = prescriptionDto.Medications.Select(m => m.ToMedication()).ToList()
            };
        }

        public static Appointment ToAppointment(this AppointmentDto appointmentDto)
        {
            return new Appointment
            {
                Id = appointmentDto.Id,
                PatientId = appointmentDto.Patient.Id,
                Description = appointmentDto.Description,
                Patient = appointmentDto.Patient.ToPatient(),
                StartAt = appointmentDto.StartAt,
                FinishAt = appointmentDto.FinishAt,
                Prescription = appointmentDto.Prescription.ToPrescription()
            };
        }

        public static Note ToNoteCreate(this NoteCreateDto noteCreateDto)
        {
            return new Note
            {
                Title = noteCreateDto.Title
            };
        }

        public static Note ToNote(this NoteDto noteDto)
        {
            return new Note
            {
                Id = noteDto.Id,
                Title = noteDto.Title,
                HealthRecord = noteDto.HealthRecord.ToHealthRecord()
            };
        }

    }
}

