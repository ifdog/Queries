using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
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
        private string _status;
        public RelayCommand CommitCommand { get; set; }

        public ItemsImportviewModel()
        {
            CommitCommand = new RelayCommand(CommitCommandAction);
            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.DoWork += (sender, args) =>
            {
                var client = RunContext.Get<Client.Client>();
                var d = File.ReadAllLines(FilePath);
                var stop = d.Length / Step;
                for (int i = 0; i < d.Length / Step; i++)
                {
                    var x = d.Skip(i * Step)
                        .Take(Step)
                        .Select(Csv.ParseLine)
                        .Where(item => item.Length >= 8)
                        .Where(item => !string.IsNullOrWhiteSpace(item[6]) && !string.IsNullOrEmpty(item[7]))
                        .Select(item =>
                        {
                            float discount, origin;
                            try
                            {
                                discount = Convert.ToSingle(item[6]);
                                origin = Convert.ToSingle(item[7]);
                            }
                            catch (Exception)
                            {
                                discount = 0;
                                origin = 0;
                            }
                            return new ItemModel
                            {
                                Name = item[0],
                                Model = item[1],
                                Spec = item[2],
                                Brand = item[3],
                                Supplier = item[4],
                                Remark = item[5],
                                Discount = discount,
                                OriginPrice = origin,
                                CreateDate = DateTime.Now
                            };
                        });
                    client.Item.AddItem(x.ToList());
                    var p = (i + 1) / (float)stop * 100.0;
                    _backgroundWorker.ReportProgress((int)p);
                }
            };
            _backgroundWorker.ProgressChanged += (sender, args) =>
            {
                Percent = args.ProgressPercentage;
                Status = $"已导入{Percent * Step}个记录";
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
