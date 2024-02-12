using Minecraft.Controllers;
using Minecraft.DataBase;
using Minecraft.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Data.Entity;
using System.Collections.ObjectModel;

namespace Minecraft.ViewModel
{
    public class Mine: INotifyPropertyChanged
    {
        DataBaseController DB = new DataBaseController();
        public Mine()
        {
            MobList = new ObservableCollection<Models.Mob>();
            using (var db = new MobsDbContext())
            {
                var MobsInDb = db.Mobs.Include(p => p.Location).Include(p => p.Drop).ToList();

                foreach (var mob in MobsInDb)
                {
                    MobList.Add(new Models.Mob
                    {
                        MobId = mob.MobId,
                        MobName = mob.MobName,
                        MobHealth = mob.MobHealth,
                        Drop = mob.Drop,
                        Location = mob.Location
                    });
                        
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Controllers.HtmlController Parser;

        private ObservableCollection<Models.Mob> mobList;

        public ObservableCollection<Models.Mob> MobList
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

                        var MobsInDb = db.Mobs.Include(p => p.Location).Include(p => p.Drop).ToList();

                        foreach (var mob in MobsInDb)
                        {
                            MobList.Add(new Models.Mob
                            {
                                MobId = mob.MobId,
                                MobName = mob.MobName,
                                MobHealth = mob.MobHealth,
                                Drop = mob.Drop,
                                Location = mob.Location
                            });

                        }
                    }
                });
                return getMobs;
            }
        } 

        private async Task StartParsing()
        {
            //DB.DeleteAll();
            Parser = new Controllers.HtmlController();
            await Parser.Parse();
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

        private ICommand _deleteSelectedCommand;
        public ICommand DeleteSelectedCommand
        {
            get
            {
                if (_deleteSelectedCommand == null)
                {
                    MobList.Remove(_SelectedMob);
                    _deleteSelectedCommand = new Command(param => DB.DeleteElement(_SelectedMob), param => CanDeleteSelected());
                    
                }
                    
                return _deleteSelectedCommand;
            }
        }
        //private void DeleteSelected()
        //{
        //    if (_SelectedMob != null)
        //    {
        //        DB.DeleteElement(_SelectedMob);
        //        MobList.Remove(_SelectedMob);
        //        OnPropertyChanged(nameof(MobList));
        //    }
        //}

        private bool CanDeleteSelected()
        {
            return SelectedMob != null;
        }

        private ICommand _deleteAllCommand;
        public ICommand DeleteAllCommand
        {
            get
            {
                if (_deleteAllCommand == null)
                    _deleteAllCommand = new Command(param => DB.DeleteAll(), param => CanDeleteAll());
                return _deleteAllCommand;
            }
        }

        private bool CanDeleteAll()
        {
            return MobList != null && MobList.Count > 0;
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
