using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using Common.Static;
using Common.Structure;
using Queries.ViewModel.Base;

namespace Queries.ViewModel
{
    public class ItemsImportviewModel : BaseViewModel
    {
        private const int Step = 50;
        private readonly BackgroundWorker _backgroundWorker = new BackgroundWorker();
        private string _filePath;
        private double _percent;
	    private int _progress;
        private string _status;
        public RelayCommand CommitCommand { get; set; }

        public ItemsImportviewModel()
        {
            CommitCommand = new RelayCommand(CommitCommandAction);
            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.DoWork += (sender, args) =>
            {
                var client = RunContext.Get<Client.Client>();
	            var items = Csv.Parse(FilePath)
				.Select(item =>
				{
					float discount, origin;
					try
					{
						discount = Convert.ToSingle(item.Discount);
						origin = Convert.ToSingle(item.OriginPrice);
					}
					catch (Exception)
					{
						discount = 0;
						origin = 0;
					}
					return new ItemModel
					{
						Name = item.Name,
						Model = item.Model,
						Spec = item.Spec,
						Brand = item.Brand,
						Supplier = item.Supplier,
						Remark = item.Remark,
						Discount = discount,
						OriginPrice = origin,
						DiscountedPrice = discount * origin,
						CreateDate = DateTime.Now
					};
				}).ToList();

	            var gp = new List<List<ItemModel>>();
	            for (int i = 0; i <= items.Count/Step; i ++)
	            {
		            gp.Add(items.Skip(Step*i).Take(Step).ToList());
	            }

	            gp.ForEach(i =>
				{
					client.Item.AddItem(i);
					Interlocked.Increment(ref _progress);
					this.Status = $"{_progress} / {gp.Count}";
					var c = Convert.ToDouble(_progress)/Convert.ToDouble(gp.Count)*100.0;
					_backgroundWorker.ReportProgress((int)c);
				});
            };
			_backgroundWorker.ProgressChanged += _backgroundWorker_ProgressChanged;
        }

		private void _backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			this.Percent = e.ProgressPercentage;
		}

		private void CommitCommandAction()
        {

            if (!File.Exists(FilePath)) return;
            _backgroundWorker.RunWorkerAsync();
        }

        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                OnPropertyChanged(nameof(FilePath));
            }
        }

        public double Percent
        {
            get { return _percent; }
            set
            {
                _percent = value;
                OnPropertyChanged(nameof(Percent));
            }
        }

        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }


    }
}
