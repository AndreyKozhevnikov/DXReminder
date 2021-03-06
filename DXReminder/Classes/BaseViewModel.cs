﻿using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Windows.Input;

namespace DXReminder.Classes {
    public class BaseViewModel {
        public BaseViewModel() {
            serializer = new ReminderSerializer();
            Processor = new RemindProcessor(null);
        }
        public ObservableCollection<Reminder> Reminders { get; set; }
        ICommand _addNewReminderCommand;
        ICommand _startProcessCommand;
        ICommand _serializeCommand;
        ReminderSerializer serializer;
        public RemindProcessor Processor { get; set; }
        public string UIDescription { get; set; }
        public List<int> UIDayOfWeekList { get; set; }
        public List<DateTime> UITimeList { get; set; }

        public ICommand AddNewReminderCommand {
            get {
                if (_addNewReminderCommand == null)
                    _addNewReminderCommand = new DelegateCommand(AddNewReminder);
                return _addNewReminderCommand;
            }
        }
        public ICommand StartProcessCommand {
            get {
                if (_startProcessCommand == null)
                    _startProcessCommand = new DelegateCommand(StartProcess);
                return _startProcessCommand;
            }
        }
        public ICommand SerializeCommand {
            get {
                if (_serializeCommand == null)
                    _serializeCommand = new DelegateCommand(Serialize);
                return _serializeCommand;
            }
        }
       
        private void StartProcess() {
            Processor.Reminders = Reminders.ToList();
            Processor.Start();
        }


        private void AddNewReminder() {
            if (string.IsNullOrEmpty(UIDescription) || UIDayOfWeekList == null || UITimeList == null || UIDayOfWeekList.Count == 0 || UITimeList.Count == 0) {
                return;
            }
            Reminder r = new Reminder(UIDescription, UIDayOfWeekList, UITimeList);
            Reminders.Add(r);
        }



        void Serialize() {
            serializer.Serialize(Reminders);
        }

        public void Deserialize() {
            Reminders = serializer.Deserialize();
        }
     

        #region temp
        public void Temp_Serialize() {
            this.Serialize();
        }


        public void Temp_CreateReminder() {
            //  Reminders.Add(new Reminder("test", 1, new DateTime(1, 1, 1, 23, 23, 0)));
            //  Reminders.Add(new Reminder("test2", 1, new DateTime(1, 1, 1, 11, 24, 0)));
        }
        #endregion

        #region For_Test
        public ReminderSerializer Test_Serializer {
            get { return serializer; }
        }

    




        #endregion
    }
}
