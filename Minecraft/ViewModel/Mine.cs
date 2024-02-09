using Minecraft.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Minecraft.ViewModel
{
    public class Mine: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Controllers.HtmlController Parser;

        private List<Models.Mob> mobList;

        public List<Models.Mob> MobList
        {
            get => mobList;
            set
            {
                mobList = value;
                OnPropertyChanged("MobList");
            }
        }

        private ICommand _startParsingCommand;
        public ICommand StartParsingCommand
        {
            get
            {
                if (_startParsingCommand == null)
                    _startParsingCommand = new Controllers.Command(async param => await StartParsing());
                return _startParsingCommand;
            }
        }

        private Controllers.Command getMobs;
        public Controllers.Command GetMobs
        {
            get
            {
                getMobs = new Controllers.Command(obj =>
                {
                    using (var db = new MobsDbContext())
                    {
                        var MobsInDb = db.Mobs.OrderBy(x => x.MobId).ToList();

                        MobList = MobsInDb.Select(x => new Models.Mob
                        {
                            MobId = x.MobId,
                            MobName = x.MobName,
                            MobHealth = x.MobHealth,
                            Drop = x.Drop,
                            Location = x.Location
                        }).ToList();
                    }
                });
                return getMobs;
            }
        }

        private async Task StartParsing()
        {
            Parser = new Controllers.HtmlController();
            await Parser.ParseData();
            GetMobs.Execute(null);
            //IsDataLoaded = true;
        }

        private Models.Mob _SelectedMob;
        public Models.Mob SelectedMob
        {
            get { return _SelectedMob; }
            set
            {
                _SelectedMob = value;
                OnPropertyChanged(nameof(SelectedMob));
            }
        }

        //private bool _isDataLoaded = false;
        //public bool IsDataLoaded
        //{
        //    get { return _isDataLoaded; }
        //    set
        //    {
        //        _isDataLoaded = value;
        //        OnPropertyChanged(nameof(IsDataLoaded));
        //    }
        //}

        //private ICommand _deleteSelectedCommand;
        //public ICommand DeleteSelectedCommand
        //{
        //    get
        //    {
        //        if (_deleteSelectedCommand == null)
        //            _deleteSelectedCommand = new Controllers.Command(param => DeleteSelected(), param => CanDeleteSelected());
        //        return _deleteSelectedCommand;
        //    }
        //}

        //private void DeleteSelected()
        //{
        //    using (var db = new MobsDbContext())
        //    {
        //        var mobsToDelete = db.Mobs.FirstOrDefault(c => c.MobId == _SelectedMob.MobId);
        //        if (mobsToDelete != null)
        //        {
        //            db.Mobs.Remove(mobsToDelete);
        //            db.SaveChanges();
        //        }
        //    }
        //    GetMobs.Execute(null);
        //}

        //private bool CanDeleteSelected()
        //{
        //    return SelectedMob != null;
        //}

        //private ICommand _deleteAllCommand;
        //public ICommand DeleteAllCommand


    }

}
