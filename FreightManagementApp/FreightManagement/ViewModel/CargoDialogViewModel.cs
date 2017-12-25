using FreightManagement.Domain.Model;
using FreightManagement.Service.Interface;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Windows.Input;

namespace FreightManagement.ViewModel
{
    public class CargoDialogViewModel : ViewModelBase
    {
        public CargoDialogViewModel(IService<Cargo> cargoService,
            IService<Order> orderService)
        {
            _cargoService = cargoService;
            _orderService = orderService;

            MessengerInstance.Register<Cargo>(this,
                "cargoModel", AssignCargo);
            MessengerInstance.Register<Window>(this,
                "cargoView", AssignWindow);
        }

        public ICommand OkCommand => _okCommand ??
            (_okCommand = new RelayCommand(() => OkDialog()));

        public ICommand CancelCommand => _cancelCommand ??
            (_cancelCommand = new RelayCommand(() => CloseDialog()));

        public Cargo CargoModel
        {
            get { return _cargoModel; }
            set
            {
                _cargoModel = value;
                RaisePropertyChanged(nameof(CargoModel));
            }
        }

        private void OkDialog()
        {
            if(CargoModel != null && _cargoService.GetById(CargoModel.Id) != null
                && CargoModel.Id != 0)
            {
                _cargoService.Update(CargoModel);
            }
            else
            {
                CargoModel.Status = StatusCargoEnum.Waiting;
                _cargoService.Create(CargoModel);
            }
            _cargoDialog.Close();
        }

        private void CloseDialog()
        {
            _cargoDialog.Close();
        }

        /// <summary>
        /// Assign cargo from MainViewModel
        /// to CargoModel.
        /// </summary>
        /// <param name="cargo">Cargo.</param>
        private void AssignCargo(Cargo cargo)
        {
            CargoModel = cargo;
        }

        /// <summary>
        /// Assign cargo view
        /// to _cargoDialog.
        /// </summary>
        /// <param name="window">Cargo view.</param>
        private void AssignWindow(Window window)
        {
            _cargoDialog = window;
        }

        private ICommand _okCommand = null;
        private ICommand _cancelCommand = null;
        private IService<Cargo> _cargoService = null;
        private IService<Order> _orderService = null;
        private Window _cargoDialog = null;
        private Cargo _cargoModel = null;
    }
}
