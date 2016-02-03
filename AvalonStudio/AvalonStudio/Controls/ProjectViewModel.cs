﻿namespace AvalonStudio.Controls.ViewModels
{
    using MVVM;
    using Perspex.Media;
    using Projects;
    using ReactiveUI;
    using System;
    using System.Collections.ObjectModel;

    public abstract class ProjectViewModel : ViewModel<IProject>
    {
        public ProjectViewModel(IProject model)
            : base(model)
        {
            items = new ObservableCollection<ViewModel>();

            items.Add(new ReferenceFolderViewModel(model));

            foreach(var item in model.Items)
            {
                items.Add(ProjectItemViewModel.Create(item));
            }
        }

        public bool IsExpanded { get; set; }

        public string Title
        {
            get
            {
                return Model.Name;
            }
        }

        private ObservableCollection<ViewModel> items;
        public ObservableCollection<ViewModel> Items
        {
            get { return items; }
            set { this.RaiseAndSetIfChanged(ref items, value); }
        }

        public ReactiveCommand<object> BuildCommand { get; protected set; }
        public ReactiveCommand<object> CleanCommand { get; protected set; }
        public ReactiveCommand<object> DebugCommand { get; protected set; }
        public ReactiveCommand<object> ManageReferencesCommand { get; protected set; }
        public ReactiveCommand<object> RemoveCommand { get; protected set; }


        public static ProjectViewModel Create(IProject model)
        {
            ProjectViewModel result = new StandardProjectViewModel(model);

            return result;
        }

        private bool visibility;
        public bool Visibility
        {
            get { return visibility; }
            set { visibility = value; this.RaisePropertyChanged(); }
        }

        public FontWeight FontWeight
        {
            get
            {
                if (Model == Model.Solution.StartupProject)
                {
                    return FontWeight.Bold;
                }
                else
                {
                    return FontWeight.Normal;
                }
            }
        }

        public ObservableCollection<string> IncludePaths { get; private set; }      
    }
}
