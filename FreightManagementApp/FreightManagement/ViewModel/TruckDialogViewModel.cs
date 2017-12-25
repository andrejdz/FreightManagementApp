using FreightManagement.Domain.Model;
using FreightManagement.Service.Interface;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Windows.Input;

namespace FreightManagement.ViewModel
{
    public class TruckDialogViewModel : ViewModelBase
    {
        public TruckDialogViewModel(IService<Truck> service)
        {
            _service = service;

            MessengerInstance.Register<Truck>(this,
                "truckModel", AssignTruck);
            MessengerInstance.Register<Window>(this,
                "truckView", AssignWindow);
        }

        public ICommand OkCommand => _okCommand ??
            (_okCommand = new RelayCommand(() => OkDialog()));

        public ICommand CancelCommand => _cancelCommand ??
            (_cancelCommand = new RelayCommand(() => CloseDialog()));

        public Truck TruckModel
        {
            get { return _truckModel; }
            set
            {
                _truckModel = value;
                RaisePropertyChanged(nameof(TruckModel));
            }
        }

        private void OkDialog()
        {
            if(TruckModel != null && _service.GetById(TruckModel.Id) != null
                && TruckModel.Id != 0)
            {
                _service.Update(TruckModel);
            }
            else
            {
                _service.Create(TruckModel);
            }
            _truckDialog.Close();
        }

        private void CloseDialog()
        {
            _truckDialog.Close();
        }

        /// <summary>
        /// Assign truck from MainViewModel
        /// to TruckModel.
        /// </summary>
        /// <param name="truck">Truck.</param>
        private void AssignTruck(Truck truck)
        {
            TruckModel = truck;
        }

        /// <summary>
        /// Assign truck view
        /// to _truckDialog.
        /// </summary>
        /// <param name="window">Truck view.</param>
        private void AssignWindow(Window window)
        {
            _truckDialog = window;
        }

        private ICommand _okCommand = null;
        private ICommand _cancelCommand = null;
        private IService<Truck> _service = null;
        private Window _truckDialog = null;
        private Truck _truckModel = null;
    }
}
