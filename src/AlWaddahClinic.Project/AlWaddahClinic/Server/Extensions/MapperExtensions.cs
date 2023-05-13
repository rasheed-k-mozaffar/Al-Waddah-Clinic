using System;
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
				EmailAddress = patient.EmaillAddress,
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
				EmaillAddress = patient.EmaillAddress,
				DateOfBirth = patient.DateOfBirth,
				Address = patient.Address,
				PhoneNumber = patient.PhoneNumber,
				HealthRecords = patient.HealthRecords.Select(h => h.ToHealthRecordDto()).ToList(),
				Appointments = patient.Appointments.Select(a => a.ToAppointmentDto()).ToList(),
				Gender = patient.Gender
			};
		}

		public static HealthRecordDto ToHealthRecordDto(this HealthRecord healthRecord)
		{
			return new HealthRecordDto
			{
				Id = healthRecord.Id,
				Description = healthRecord.Description,
				Patient = healthRecord.Patient.ToPatientDto(),
				Medications = healthRecord.Medications?.Select(m => m.ToMedicationDto()).ToList()
			};
		}

		public static MedicationDto ToMedicationDto(this Medication medication)
		{
			return new MedicationDto
			{
				Id = medication.Id,
				CommercialName = medication.CommercialName,
				DailyDose = medication.DailyDose,
				FinishAt= medication.FinishAt,
				HealthRecord = medication.HealthRecord?.ToHealthRecordDto(),
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
	}

	public static class ModelMappers
	{
		public static Patient ToPatient(this PatientCreateDto patientDto)
		{
			return new Patient
			{
				FullName = patientDto.FullName,
				EmaillAddress = patientDto.EmailAddress,
				PhoneNumber = patientDto.PhoneNumber,
				Address = patientDto.Address,
				DateOfBirth = patientDto.DateOfBirth,
				Gender = patientDto.Gender,
				Appointments = new List<Appointment>(),
				HealthRecords = new List<HealthRecord>()
			};
		}
	}
}

