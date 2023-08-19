using System;
using AlWaddahClinic.Client.Components;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AlWaddahClinic.Client.Pages.Patients.Records
{
	public partial class ListRecord : ComponentBase
	{
		[Inject]
		public NavigationManager NavigationManager { get; set; } = default!;

		[Inject]
		public IHealthRecordsService HealthRecordsService { get; set; } = default!;

		[Inject]
		public IPaymentsService PaymentsService { get; set; } = default!;

		[Inject]
		public IDialogService DialogService { get; set; } = default!;

		//Matching the route params
		[Parameter] public Guid Id { get; set; }

		private ApiResponse<HealthRecordDto> result = new();
		private HealthRecordDto record = new();
		private ApiResponse<IEnumerable<PaymentDto>> paymentsResult = new();
		private List<PaymentDto> payments = new();
		private PaymentCreateDto paymentModel = new();

		private string _patientName = string.Empty;
		private string _errorMessage = string.Empty;
		private string _paymentErrorMessage = string.Empty;
		private bool _isBusy = true;
		private bool _wantsToAddPayment = false;
		private bool _isAddingNewPayment = false;
		private bool _refreshingPayments = false;
		private string _addPaymentButtonStauts => _wantsToAddPayment ? "none" : "inline";

        protected override async Task OnInitializedAsync()
        {
			_errorMessage = string.Empty;
			try
			{
                result = await HealthRecordsService.GetHealthRecordByIdAsync(Id);
				paymentsResult = await PaymentsService.GetPaymentsForRecordAsync(Id);
				if(result.IsSuccess)
				{
					record = result.Value;
				}

				if (paymentsResult.IsSuccess)
				{
					payments = paymentsResult.Value.ToList();
				}

				_isBusy = false;
            }
			catch(DomainException ex)
			{
				_errorMessage = ex.Message;
			}
        }

		private async Task OpenRemoveDialog()
		{
			var parameters = new DialogParameters();
			parameters.Add("Header" ,"Confirm Removal");
			parameters.Add("Content", "Do you really want to remove this health record? By confirming the process, all related notes will be removed as well");
			parameters.Add("ButtonText", "Remove");
			parameters.Add("Color", Color.Error);

			var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

			var dialog = DialogService.Show<Dialog>("Delete", parameters, options);
			var result = await dialog.Result;

			if(!result.Canceled)
			{
				await RemoveRecordAsync();
			}
		}

		private async Task RemoveRecordAsync()
		{
			try
			{
				await HealthRecordsService.RemoveRecordAsync(Id);
				NavigationManager.NavigateTo($"/patients/{record.Patient.Id}");
			}
			catch(DomainException ex)
			{
				_errorMessage = ex.Message;
			}
		}

		private void ShowAddPaymentForm()
		{
			_wantsToAddPayment = !_wantsToAddPayment;
		}

		private async Task AddPayment()
		{
			_paymentErrorMessage = string.Empty;

            if (paymentModel.Amount <= 0)
			{
				_paymentErrorMessage = "You can't add a payment without providing an amount!";
				return;
			}

			_isAddingNewPayment = true;

			if((record.TotalAmount - payments.Sum(p => p.Amount)) - paymentModel.Amount < 0)
			{
				_paymentErrorMessage = "The payment amount is greater than the remaning amount!";
				return;
			}

			try
			{
				await PaymentsService.
					CreatePaymentAsync(healthRecordId: Id,model: paymentModel);

				await HandlePaymentsChange();

				if(payments.Sum(p => p.Amount) >= record.TotalAmount)
				{
					record.IsPaymentCompleted = true;
					await HealthRecordsService.MarkPaymentsCompletedAsync(Id);
				}

				paymentModel = new PaymentCreateDto();
				ShowAddPaymentForm();
			}
			catch(DomainException ex)
			{
				_paymentErrorMessage = ex.Message;
			}
        }

		private async Task OpenPaymentRemoveDialog(Guid paymentId)
		{
			var parameters = new DialogParameters();
			parameters.Add("Header", "Confirm Removal");
			parameters.Add("Content", "Do you really want to remove this payment? Once the process is cofirmed, it cannot be inverted!");
			parameters.Add("ButtonText", "Remove");
			parameters.Add("Color", Color.Error);

			var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

			var dialog = DialogService.Show<Dialog>("Delete", parameters, options);
			var result = await dialog.Result;

			if (!result.Canceled)
			{
				await RemovePaymentAsync(paymentId);
			}
		}

		private async Task RemovePaymentAsync(Guid id)
		{
			try
			{
				await PaymentsService.RemovePaymentAsync(paymentId: id);
				await HandlePaymentsChange();
			}
			catch(DomainException ex)
			{
				_paymentErrorMessage = ex.Message;
			}
		}

		private void CancelPaymentCreation()
		{
			paymentModel = new PaymentCreateDto();
			_paymentErrorMessage = string.Empty;
			ShowAddPaymentForm();
		}

		private async Task HandlePaymentsChange()
		{
			if (record.IsPaymentCompleted)
			{
				result = await HealthRecordsService.GetHealthRecordByIdAsync(recordId: Id);
				record = result.Value;
			}

			_refreshingPayments = true;
			paymentsResult = await PaymentsService.GetPaymentsForRecordAsync(Id);
			payments = paymentsResult.Value.ToList();
			_refreshingPayments = false;
		}

		private void OpenPaymentDetailsDialog(Guid id)
		{
			var parameters = new DialogParameters();

			parameters.Add("Header", "Payment Details");
			parameters.Add("PaymentId", id);

			var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Medium, ClassBackground = "dialog-overlay"};

			DialogService.Show<PaymentDetailsDialog>(null, parameters, options);
		}


        private void GoBack()
		{
			NavigationManager.NavigateTo($"/patients/{record.Patient.Id}");
		}
    }
}

