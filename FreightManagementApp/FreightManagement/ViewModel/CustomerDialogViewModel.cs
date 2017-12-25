using FreightManagement.Domain.Model;
using FreightManagement.Service.Interface;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using System.Windows;
using System.Windows.Input;

namespace FreightManagement.ViewModel
{
    public class CustomerDialogViewModel : ViewModelBase
    {
        public CustomerDialogViewModel(IService<Customer> service)
        {
            _service = service;

            MessengerInstance.Register<Customer>(this,
                "customerModel", AssignCustomer);
            MessengerInstance.Register<Window>(this,
                "customerView", AssignWindow);
        }

        public ICommand OkCommand => _okCommand ??
            (_okCommand = new RelayCommand(() => OkDialog()));

        public ICommand CancelCommand => _cancelCommand ??
            (_cancelCommand = new RelayCommand(() => CloseDialog()));

        public Customer CustomerModel
        {
            get { return _customerModel; }
            set
            {
                _customerModel = value;
                RaisePropertyChanged(nameof(CustomerModel));
            }
        }

        private void OkDialog()
        {
            if(CustomerModel != null && _service.GetById(CustomerModel.Id) != null
                && CustomerModel.Id != 0)
            {
                _service.Update(CustomerModel);
            }
            else
            {
                _service.Create(CustomerModel);
            }
            _customerDialog.Close();
        }

        private void CloseDialog()
        {
            _customerDialog.Close();
        }

        /// <summary>
        /// Assign customer from MainViewModel
        /// to CustomerModel.
        /// </summary>
        /// <param name="customer">Customer.</param>
        private void AssignCustomer(Customer customer)
        {
            CustomerModel = customer;
        }

        /// <summary>
        /// Assign customer view
        /// to _customerDialog.
        /// </summary>
        /// <param name="window">Customer view.</param>
        private void AssignWindow(Window window)
        {
            _customerDialog = window;
        }

        private ICommand _okCommand = null;
        private ICommand _cancelCommand = null;
        private IService<Customer> _service = null;
        private Window _customerDialog = null;
        private Customer _customerModel = null;
    }
}
