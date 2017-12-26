using GalaSoft.MvvmLight;
using FreightManagement.View;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using FreightManagement.Service.Interface;
using FreightManagement.Domain.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace FreightManagement.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IService<Customer> customerService,
            IService<Truck> truckService,
            IService<Order> orderService,
            IService<Cargo> cargoService)
        {
            #region Assign services
            _customerService = customerService;
            _truckService = truckService;
            _orderService = orderService;
            _cargoService = cargoService;
            #endregion

            #region Create ObservableCollections
            CustomerList = new ObservableCollection<Customer>(_customerService.GetAll().Result);
            TruckList = new ObservableCollection<Truck>(_truckService.GetAll().Result);
            OrderList = new ObservableCollection<Order>(_orderService.GetAll().Result);
            CargoList = new ObservableCollection<Cargo>(_cargoService.GetAll().Result);
            #endregion
        }

        #region RelayCommands
        public ICommand AddCommand => _addCommand ??
            (_addCommand = new RelayCommand<string>((key) => Add(key)));

        public ICommand UpdateCommand => _updateCommand ??
            (_updateCommand = new RelayCommand<string>((key) => Update(key)));

        public ICommand DeleteCommand => _deleteCommand ??
            (_deleteCommand = new RelayCommand<string>((key) => Delete(key)));

        public ICommand ObjectsToXML => _objectsToXML ??
            (_objectsToXML = new RelayCommand<string>((key) => ToXML(key)));
        #endregion

        #region ObservableCollections properties
        public ObservableCollection<Customer> CustomerList { get; set; }
        public ObservableCollection<Truck> TruckList { get; set; }
        public ObservableCollection<Order> OrderList { get; set; }
        public ObservableCollection<Cargo> CargoList { get; set; }
        #endregion

        #region Models
        public Customer CustomerModel { get; set; }
        public Truck TruckModel { get; set; }
        public Order OrderModel { get; set; }
        public Cargo CargoModel { get; set; }
        #endregion

        private void Add(string key)
        {
            switch(key)
            {
                case "customer":
                    CustomerDialog customerDialog = new CustomerDialog();
                    MessengerInstance.Send(new Customer(), "customerModel");
                    MessengerInstance.Send<Window>(customerDialog, "customerView");
                    customerDialog.ShowDialog();

                    UpdateCustomerGridView();
                    break;
                case "truck":
                    TruckDialog truckDialog = new TruckDialog();
                    MessengerInstance.Send(new Truck(), "truckModel");
                    MessengerInstance.Send<Window>(truckDialog, "truckView");
                    truckDialog.ShowDialog();

                    UpdateTruckGridView();
                    break;
                case "order":
                    OrderDialog orderDialog = new OrderDialog();
                    MessengerInstance.Send(new Order(), "orderModel");
                    MessengerInstance.Send<Window>(orderDialog, "orderView");
                    orderDialog.ShowDialog();

                    UpdateOrderGridView();
                    UpdateTruckGridView();
                    UpdateCargoGridView();
                    break;
                case "cargo":
                    CargoDialog cargoDialog = new CargoDialog();
                    MessengerInstance.Send(new Cargo(), "cargoModel");
                    MessengerInstance.Send<Window>(cargoDialog, "cargoView");
                    cargoDialog.ShowDialog();

                    UpdateCargoGridView();
                    break;
                default:
                    break;
            }
        }

        private void Update(string key)
        {
            switch(key)
            {
                case "customer":
                    CustomerDialog customerDialog = new CustomerDialog();
                    MessengerInstance.Send(CustomerModel, "customerModel");
                    MessengerInstance.Send<Window>(customerDialog, "customerView");
                    customerDialog.ShowDialog();
                    break;
                case "truck":
                    TruckDialog truckDialog = new TruckDialog();
                    MessengerInstance.Send(TruckModel, "truckModel");
                    MessengerInstance.Send<Window>(truckDialog, "truckView");
                    truckDialog.ShowDialog();
                    break;
                case "order":
                    OrderDialog orderDialog = new OrderDialog();
                    MessengerInstance.Send(OrderModel, "orderModel");
                    MessengerInstance.Send<Window>(orderDialog, "orderView");
                    orderDialog.ShowDialog();

                    UpdateOrderGridView();
                    UpdateTruckGridView();
                    UpdateCargoGridView();
                    break;
                case "cargo":
                    CargoDialog cargoDialog = new CargoDialog();
                    MessengerInstance.Send(CargoModel, "cargoModel");
                    MessengerInstance.Send<Window>(cargoDialog, "cargoView");
                    cargoDialog.ShowDialog();

                    UpdateCargoGridView();
                    break;
                default:
                    break;
            }
        }

        private void Delete(string key)
        {
            switch(key)
            {
                case "customer":
                    _customerService.Delete(CustomerModel.Id);
                    CustomerList.Remove(CustomerModel);
                    break;
                case "truck":
                    _truckService.Delete(TruckModel.Id);
                    TruckList.Remove(TruckModel);
                    break;
                case "order":
                    UpdateTruckOnDelete();
                    UpdateCargoOnDelete();

                    UpdateCargoGridView();
                    UpdateTruckGridView();

                    _orderService.Delete(OrderModel.Id);
                    OrderList.Remove(OrderModel);
                    break;
                case "cargo":
                    _cargoService.Delete(CargoModel.Id);
                    CargoList.Remove(CargoModel);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Updates customer's grid view.
        /// </summary>
        private void UpdateCustomerGridView()
        {
            CustomerList.Clear();
            foreach(var item in _customerService.GetAll().Result)
            {
                CustomerList.Add(item);
            }
        }

        /// <summary>
        /// Updates truck's grid view.
        /// </summary>
        private void UpdateTruckGridView()
        {
            TruckList.Clear();
            foreach(var item in _truckService.GetAll().Result)
            {
                TruckList.Add(item);
            }
        }

        /// <summary>
        /// Updates cargo's grid view.
        /// </summary>
        private void UpdateCargoGridView()
        {
            CargoList.Clear();
            foreach(var item in _cargoService.GetAll().Result)
            {
                CargoList.Add(item);
            }
        }

        /// <summary>
        /// Updates order's grid view.
        /// </summary>
        private void UpdateOrderGridView()
        {
            OrderList.Clear();
            foreach(var item in _orderService.GetAll().Result)
            {
                OrderList.Add(item);
            }
        }

        /// <summary>
        /// Updates cargo's status when delete order.
        /// </summary>
        private void UpdateCargoOnDelete()
        {
            List<Cargo> listCargoes = new List<Cargo>();
            listCargoes.AddRange(_orderService.GetById(OrderModel.Id).Result.Cargoes);

            foreach(var item in listCargoes)
            {
                Cargo freedCargo = _cargoService.GetById(item.Id).Result;
                freedCargo.OrderId = null;
                freedCargo.Status = StatusCargoEnum.Waiting;
                _cargoService.Update(freedCargo);
            }
        }

        /// <summary>
        /// Updates truck's status when delete order.
        /// </summary>
        private void UpdateTruckOnDelete()
        {
            if(TruckModel != null)
            {
                Truck freedTruck = _truckService.GetById(OrderModel.TruckId.Value).Result;
                freedTruck.Status = AvailabilityEnum.Free;
                _truckService.Update(freedTruck);
            }
        }

        /// <summary>
        /// Saves data to XML.
        /// </summary>
        /// <param name="key">Specify object to XML</param>
        private void ToXML(string key)
        {
            switch(key)
            {
                case "order":
                    XElement orders = new XElement("Orders",
                        from o in OrderList
                        select new XElement("Order", new XAttribute("Id", o.Id),
                            new XElement("Price", o.Price),
                            new XElement("Distance", o.Distance),
                            new XElement("Country", o.Country),
                            new XElement("City", o.City),
                            new XElement("Address", o.Address),
                            new XElement("Term", o.Term),
                            new XElement("Status", o.Status),
                            new XElement("Cargoes",
                                from c in o.Cargoes
                                select new XElement("Cargo", new XAttribute("Id", c.Id),
                                    new XElement("Description", c.Description),
                                    new XElement("Weight", c.Weight),
                                    new XElement("Status", c.Status)))
                                ));
                    orders.Save("Orders.xml");
                    break;
                case "customer":
                    XElement customers = new XElement("Customers",
                        from c in CustomerList
                        select new XElement("Customer", new XAttribute("Id", c.Id),
                            new XElement("Name", c.Name),
                            new XElement("Manager", c.Manager),
                            new XElement("Telephone", c.Telephone),
                            new XElement("Fax", c.Fax),
                            new XElement("Orders",
                                from o in c.Orders
                                select new XElement("Order", new XAttribute("Id", o.Id),
                                    new XElement("Price", o.Price),
                                    new XElement("Distance", o.Distance),
                                    new XElement("Country", o.Country),
                                    new XElement("City", o.City),
                                    new XElement("Address", o.Address),
                                    new XElement("Term", o.Term),
                                    new XElement("Status", o.Status)))
                                ));

                    customers.Save("Customers.xml");
                    break;
                case "truck":
                    XElement trucks = new XElement("Trucks",
                        from t in TruckList
                        select new XElement("Truck", new XAttribute("Id", t.Id),
                            new XElement("Name", t.Name),
                            new XElement("CarryingCapacity", t.CarryingCapacity),
                            new XElement("CostPerKm", t.CostPerKm),
                            new XElement("Status", t.Status),
                            new XElement("Orders",
                                from o in t.Orders
                                select new XElement("Order", new XAttribute("Id", o.Id),
                                    new XElement("Price", o.Price),
                                    new XElement("Distance", o.Distance),
                                    new XElement("Country", o.Country),
                                    new XElement("City", o.City),
                                    new XElement("Address", o.Address),
                                    new XElement("Term", o.Term),
                                    new XElement("Status", o.Status)))
                                ));

                    trucks.Save("Trucks.xml");
                    break;
                case "cargo":
                    XElement cargoes = new XElement("Cargoes",
                        from c in CargoList
                        select new XElement("Cargo", new XAttribute("Id", c.Id),
                            new XElement("Description", c.Description),
                            new XElement("Weight", c.Weight),
                            new XElement("Status", c.Status)));

                    cargoes.Save("Cargoes.xml");
                    break;
                default:
                    break;
            }
        }

        #region Fields
        private ICommand _addCommand = null;
        private ICommand _deleteCommand = null;
        private ICommand _updateCommand = null;
        private ICommand _objectsToXML = null;
        private IService<Customer> _customerService = null;
        private IService<Truck> _truckService = null;
        private IService<Order> _orderService = null;
        private IService<Cargo> _cargoService = null;
        #endregion
    }
}