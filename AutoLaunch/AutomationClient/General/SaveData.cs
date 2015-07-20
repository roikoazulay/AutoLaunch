using System.Collections.ObjectModel;
using AutomationClient.Models.Memento;
using AutomationCommon;
using AutomationServer;

namespace AutomationClient
{
    public class SaveData
    {
        public ScriptObj CopyStepEntities { get; set; }

        public TestObj CopyTestEntities { get; set; }

        public bool IsSelectedStepEntity { get; set; }//for updating selected entity

        public StepEntity SelectedStepEntity { get; set; }

        public ScriptObj Script { get; set; }

        public TestEntity SelectedTestEntity { get; set; }

        public TestObj Test { get; set; }

        public TestSuiteEntity SelectedTestSuiteEntity { get; set; }

        public TestSuite TestSuite { get; set; }

        public int SelectedStepEntityIndex { get; set; }

        public ObservableCollection<AutomationCommon.ClientMessage> ClientMessages { get; set; }

        public void AddTestEntity(TestEntity entity)
        {
            entity.Enable = true;
            Test.Entities.Add(entity);
        }

        public void AddStepEntity(StepEntity entity)
        {
            if (SelectedStepEntityIndex == -1)
                SelectedStepEntityIndex = 0;
            int index = SelectedStepEntityIndex;
            entity.Enable = true;
            if (IsSelectedStepEntity)
            {
                entity.Comment = Script.Entities[index].Comment;//save the previous comment
                Script.Entities[index] = entity;
                SelectedStepEntityIndex = index;//SelectedStepEntityIndex turns to -1 after update
            }
            else
                Script.Entities.Add(entity);

            UpdateAutoSave();
        }

        public void UpdateAutoSave()
        {
            Singleton.Instance<DesignerViewModel>().AlternatingRowBackground = "#FFEF9999";
          //  Singleton.Instance<DesignerViewModel>().OnPropertyChanged("AlternatingRowBackground");
            //auto save on update or change
            if (AutoApp.Settings.ScriptAutoSave)
                FileHandler.GenerateFile(Singleton.Instance<SaveData>().Script,
                                         StaticFields.SCRIPT_PATH + "\\AutoSave" + StaticFields.SCRIPT_EXTENTION);

            Singleton.Instance<Originator>().SaveToMemento(Script);
        }

        public void AddTestSuiteEntity(TestSuiteEntity entity)
        {
            entity.Enable = true;
            if (IsSelectedStepEntity)
                SelectedTestSuiteEntity = entity;
            else
                TestSuite.Entities.Add(entity);
        }

        private SaveData()
        {
            CopyStepEntities = new ScriptObj();
            CopyTestEntities = new TestObj();
            Test = new TestObj();
            Script = new ScriptObj();
            TestSuite = new TestSuite();
            ClientMessages = new ObservableCollection<AutomationCommon.ClientMessage>();
        }

        public FileScanner FileScanner { get; set; }
    }
}