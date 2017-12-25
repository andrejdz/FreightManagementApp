using FreightManagement.Domain.Model;
using FreightManagement.Service.Interface;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace FreightManagement.ViewModel
{
    public class OrderDialogViewModel : ViewModelBase
    {
        public OrderDialogViewModel(IService<Order> orderService,
            IService<Customer> customerService, IService<Truck> truckService,
            IService<Cargo> cargoService)
        {
            _orderService = orderService;
            _customerService = customerService;
            _truckService = truckService;
            _cargoService = cargoService;

            MessengerInstance.Register<Order>(this,
                "orderModel", AssignOrder);
            MessengerInstance.Register<Window>(this,
                "orderView", AssignWindow);

            CustomerList = new ObservableCollection<Customer>(_customerService.GetAll().Result);
            TruckList = new ObservableCollection<Truck>(_truckService
                .GetAll().Result
                .Where(t => t.Status == AvailabilityEnum.Free));
            CargoList = new ObservableCollection<Cargo>(_cargoService
                .GetAll().Result
                .Where(c => c.Status == StatusCargoEnum.Waiting));
        }

        public ICommand OkCommand => _okCommand ??
            (_okCommand = new RelayCommand(() => OkDialog()));

        public ICommand CancelCommand => _cancelCommand ??
            (_cancelCommand = new RelayCommand(() => CloseDialog()));

        public Order OrderModel
        {
            get { return _orderModel; }
            set
            {
                _orderModel = value;
                RaisePropertyChanged(nameof(OrderModel));
            }
        }

        public Customer CustomerModel
        {
            get { return _customerModel; }
            set
            {
                _customerModel = value;
                RaisePropertyChanged(nameof(CustomerModel));
            }
        }

        public Truck TruckModel
        {
            get { return _truckModel; }
            set
            {
                _truckModel = value;
                RaisePropertyChanged(nameof(TruckModel));
            }
        }

        public Cargo CargoModel
        {
            get { return _cargoModel; }
            set
            {
                _cargoModel = value;
                RaisePropertyChanged(nameof(CargoModel));
            }
        }

        public bool IsEnded
        {
            get { return _isEnded; }
            set
            {
                _isEnded = value;
                RaisePropertyChanged(nameof(IsEnded));
            }
        }


        public ObservableCollection<Cargo> CargoList { get; set; }
        public ObservableCollection<Customer> CustomerList { get; set; }
        public ObservableCollection<Truck> TruckList { get; set; }

        private void OkDialog()
        {
            if(OrderModel != null && _orderService.GetById(OrderModel.Id) != null
                && OrderModel.Id != 0)
            {
                if(TruckModel != null)
                {
                    if(IsEnded)
                    {
                        OrderModel.Status = StatusEnum.Ended;
                    }
                    else
                    {
                        OrderModel.Status = StatusEnum.Processing;
                    }

                    OrderModel.Price = OrderModel.Distance * TruckModel.CostPerKm;

                    Truck freedTruck = _truckService.GetById(OrderModel.TruckId.Value).Result;
                    freedTruck.Status = AvailabilityEnum.Free;
                    _truckService.Update(freedTruck);

                    OrderModel.TruckId = TruckModel.Id;
                    TruckModel.Status = AvailabilityEnum.Busy;

                    _truckService.Update(TruckModel);
                }
                else
                {
                    if(IsEnded)
                    {
                        OrderModel.Status = StatusEnum.Ended;
                    }
                    else if(OrderModel.TruckId != null)
                    {
                        OrderModel.Status = StatusEnum.Processing;
                    }
                    else
                    {
                        OrderModel.Status = StatusEnum.InQueue;
                    }
                }

                if(CargoModel != null)
                {
                    List<Cargo> listCargoes = new List<Cargo>();
                    listCargoes.AddRange(_orderService.GetById(OrderModel.Id).Result.Cargoes);

                    foreach(var freedCargo in listCargoes)
                    {
                        freedCargo.Status = StatusCargoEnum.Waiting;
                        freedCargo.OrderId = null;
                        _cargoService.Update(freedCargo);
                    }

                    CargoModel.Status = StatusCargoEnum.InOrder;
                    CargoModel.OrderId = OrderModel.Id;

                    _cargoService.Update(CargoModel);
                }

                if(CustomerModel != null)
                {
                    OrderModel.CustomerId = CustomerModel.Id;
                }

                _orderService.Update(OrderModel);
            }
            else
            {
                OrderModel.CustomerId = CustomerModel.Id;
                OrderModel.TruckId = TruckModel.Id;

                if(OrderModel.TruckId != null)
                {
                    OrderModel.Price = TruckModel.CostPerKm * OrderModel.Distance;
                    OrderModel.Status = StatusEnum.Processing;

                    TruckModel.Status = AvailabilityEnum.Busy;
                    _truckService.Update(TruckModel);
                }
                else
                {
                    OrderModel.Status = StatusEnum.InQueue;
                }

                _orderService.Create(OrderModel);

                if(CargoModel != null)
                {
                    Cargo cargo = _cargoService.GetById(CargoModel.Id).Result;
                    cargo.OrderId = OrderModel.Id;
                    cargo.Status = StatusCargoEnum.InOrder;
                    _cargoService.Update(cargo);
                }
            }
            _orderDialog.Close();
        }

        private void CloseDialog()
        {
            _orderDialog.Close();
        }

        /// <summary>
        /// Assign order from MainViewModel
        /// to OrderModel.
        /// </summary>
        /// <param name="order">Order.</param>
        private void AssignOrder(Order order)
        {
            OrderModel = order;
            IsEnded = OrderModel.Status == StatusEnum.Ended ? true : false;
        }

        /// <summary>
        /// Assign order view
        /// to _orderDialog.
        /// </summary>
        /// <param name="window">Order view.</param>
        private void AssignWindow(Window window)
        {
            _orderDialog = window;
        }

        private ICommand _okCommand = null;
        private ICommand _cancelCommand = null;
        private IService<Order> _orderService = null;
        private IService<Customer> _customerService = null;
        private IService<Truck> _truckService = null;
        private IService<Cargo> _cargoService = null;
        private Window _orderDialog = null;
        private Order _orderModel = null;
        private Customer _customerModel = null;
        private Truck _truckModel = null;
        private Cargo _cargoModel = null;
        private bool _isEnded = false;
    }
}