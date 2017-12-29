using FreightManagement.Domain.Model;
using FreightManagement.Service.Interface;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FreightManagement.ViewModel
{
    public class ChartViewModel : ViewModelBase
    {
        public ChartViewModel(IService<Order> order)
        {
            _service = order;

            OrderList = new ObservableCollection<Order>(_service.GetAll().Result);

            BuildChart();
        }

        #region ObservableCollections properties
        public ObservableCollection<KeyValuePair<string, int>> ChartList { get; set; }
        public ObservableCollection<Order> OrderList { get; set; }
        #endregion

        /// <summary>
        /// Fills chart with data.
        /// </summary>
        private void BuildChart()
        {
            ChartList = new ObservableCollection<KeyValuePair<string, int>>();
            foreach(var item in OrderList)
            {
                Thread.Sleep(1);
                int value = new Random((int)DateTime.Now.Ticks & 0x0000FFFF).Next(5, 30);
                ChartList.Add(new KeyValuePair<string, int>(item.Truck.Name, value));
            }
        }

        #region Fields
        private IService<Order> _service = null;
        #endregion
    }
}
