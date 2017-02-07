using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Enums;
using Common.Static;
using Common.Structure;
using Queries.Structure;
using Queries.ViewModel.Base;

namespace Queries.ViewModel
{
    public class ItemsImportviewModel : BaseViewModel
    {
        private const int Step = 50;
	    private const int MaxThreads = 1;
        private readonly BackgroundWorker _backgroundWorker = new BackgroundWorker();
        private string _filePath;
        private double _percent;
	    private int _progress;
	    private int _fail;
        private string _status;
        public RelayCommand CommitCommand { get; set; }
	    private StatusManager _statusManager = RunContext.Get<StatusManager>();
		private Client.Client _client = RunContext.Get<Client.Client>();

	    public ItemsImportviewModel()
	    {
		    CommitCommand = new RelayCommand(CommitCommandAction);

		    _backgroundWorker.DoWork += (sender, args) =>
		    {
			    _progress = 0;
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
						    DiscountedPrice = discount*origin,
						    CreateDate = DateTime.Now
					    };
				    }).ToList();

			    var gp = new List<List<ItemModel>>();
			    for (int i = 0; i <= items.Count/Step; i ++)
			    {
				    gp.Add(items.Skip(Step*i).Take(Step).ToList());
			    }
			    Parallel.ForEach(gp, new ParallelOptions {MaxDegreeOfParallelism = MaxThreads}, i =>
			    {
				    _client.Item.AddItem(i).ContinueWith(j =>
				    {
					    if (j.Result.EqualsResultCode(ResultCode.Ok))
					    {
						    Interlocked.Increment(ref _progress);
					    }
					    else
					    {
						    Interlocked.Increment(ref _fail);
					    }
					    this._statusManager.Status = $"导入列表：成功{_progress*Step},失败{_fail*Step},共{gp.Count*Step}";
					    this._statusManager.Percents = Convert.ToDouble(_progress + _fail)/Convert.ToDouble(gp.Count)*100.0;
				    });
			    });
		    };
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
    }
}
